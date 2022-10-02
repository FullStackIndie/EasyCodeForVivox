using EasyCodeForVivox;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VivoxUnity;

public class EasyExample : EasyManager
{
    [SerializeField] string apiEndpoint;
    [SerializeField] string domain;
    [SerializeField] string issuer;
    [SerializeField] string secretKey;


    [SerializeField] InputField userName;
    [SerializeField] InputField remotePlayerName;
    [SerializeField] InputField channelName;
    [SerializeField] InputField message;
    [SerializeField] Toggle voiceToggle;
    [SerializeField] Toggle textToggle;
    [SerializeField] Slider selfSlider;
    [SerializeField] Slider remotePlayerSlider;

    [SerializeField] Text newMessage;
    [SerializeField] Image container;
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] TMP_Dropdown dropdown;


    private void OnApplicationQuit()
    {
        UnitializeClient();
    }

    private async void Awake()
    {
        // todo Implement Unity Remote Config to store sensitive information in the cloud. It's free
        // if users dont want to use Remote Config then advise users to use environment variables instead of hardcoding secrets/api keys
        // inside of the Unity Editor because hackers can decompile there game and steal the secrets/keys
        EasySession.APIEndpoint = new Uri(apiEndpoint);
        EasySession.Domain = domain;
        EasySession.Issuer = issuer;
        EasySession.SecretKey = secretKey;

        await InitializeClient();
        DontDestroyOnLoad(this);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [LoginEvent(LoginStatus.LoggedIn)]
    public  void CustomLoginEvent(ILoginSession loginSession)
    {
        Debug.Log($"Invoking Event Dynamically from {nameof(CustomLoginEvent)}");
    }

    [LoginEvent(LoginStatus.LoggedIn)]
    public static void CustomStaticLoginEvent(ILoginSession loginSession)
    {
        Debug.Log($"Invoking Event Dynamically from {nameof(CustomStaticLoginEvent)}");
    }

    // Clears Text messages where event logs show up in demo scene
    // hooked up to ClearMessages Button in demo scene
    public void ClearMessages()
    {
        newMessage.text = "";
        scrollbar.value = 1;
    }

    public void Login()
    {
        ClearMessages();
        LoginToVivox(userName.text);
    }

    public void Logout()
    {
        LogoutOfVivox();
    }

    public void JoinChannel()
    {
        JoinChannel("3D", true, false, true, ChannelType.Positional);
        JoinChannel(channelName.text, true, true, true, ChannelType.NonPositional);
    }

    public void SendMessage()
    {
        if (string.IsNullOrEmpty(channelName.text) || string.IsNullOrEmpty(message.text))
        {
            Debug.Log("Channel name or message is empty");
            return;
        }
        SendChannelMessage(channelName.text, message.text);
    }

    public void SendDirectMessageToPlayer()
    {
        var selectedUser = dropdown.GetSelected();
        if (selectedUser != null)
        {
            SendDirectMessage(selectedUser, message.text);
        }
        else
        {
            Debug.Log("Remote Player Name is Empty or Invalid, Cannot send Message");
        }
    }

    public void LeaveChannel()
    {
        LeaveChannel(channelName.text);
    }

    public void ToggleAudioInChannel()
    {
        SetVoiceActiveInChannel(channelName.text, voiceToggle.isOn);
    }

    public void ToggleTextInChannel()
    {
        SetTextActiveInChannel(channelName.text, textToggle.isOn);
    }

    public void MuteLocalPlayer()
    {
        MuteSelf();
    }

    public void UnmuteLocalPlayer()
    {
        UnmuteSelf();
    }

    public void ToggleMuteRemotePlayer()
    {
        var selectedUser = dropdown.GetSelected();
        if (selectedUser != null)
        {
            ToggleMuteRemoteUser(selectedUser, channelName.text);
        }
        else
        {
            Debug.Log("Remote Player Name is Empty or Invalid, Cannot mute player");
        }
    }

    public void MuteAllPlayers()
    {
        MuteAllPlayers(channelName.text);
    }

    public void UnmuteAllPlayers()
    {
        UnmuteAllPlayers(channelName.text);
    }

    public void AdjustLocalSelfVolume()
    {
        AdjustLocalUserVolume(Mathf.RoundToInt(selfSlider.value));
    }

    public void AdjustRemotePlayerVolume()
    {
        var selectedUser = dropdown.GetSelected();
        if (selectedUser != null)
        {
            AdjustRemoteUserVolume(selectedUser, channelName.text, Mathf.RoundToInt(remotePlayerSlider.value));
        }
        else
        {
            Debug.Log("Remote Player Name is Empty or Invalid, Cannot adjust player volume");
        }
    }

    public void EnablePushToTalk()
    {
        PushToTalk(true, KeyCode.Space);
    }

    public void DisablePushToTalk()
    {
        PushToTalk(false, KeyCode.Space);
    }

    public void TextToSpeech()
    {
        SpeakTTS(message.text, TTSDestination.QueuedRemoteTransmissionWithLocalPlayback);
    }




    #region  Demo Scene Events for Raise Hand, Mute Player, and Unmute Player


    // Event Messages for Buttons in Demo Scene to RaiseHand, Mute globally or Unmute globally in channel
    // For teaching and Admin settings. Like discord or zoom call without the video
    // This is example is if you are not using a Networking Stack like Mirror, Photon, MLAPI, 
    // or NetCode for GameObjects by Unity. You should handle this type of logic on the server
    // and not use Vivox's event/hidden messages for this type of feature in my opinion.


    public void SendRaiseHandEventMessage()
    {
        SendEventMessage(channelName.text, "event", "Event:RaiseHand", EasySession.mainLoginSession.LoginSessionId.Name);
    }

    public void SendMuteEventMessage()
    {
        SendEventMessage(channelName.text, "event", "Event:Mute", dropdown.GetSelected());
    }

    public void SendUnmuteEventMessage()
    {
        SendEventMessage(channelName.text, "event", "Event:Unmute", dropdown.GetSelected());
    }


    public override void OnEventMessageRecieved(IChannelTextMessage textMessage)
    {
        base.OnEventMessageRecieved(textMessage);
        if (textMessage.ApplicationStanzaNamespace.Contains("RaiseHand"))
        {
            HandleRaiseHandEvent(textMessage);
        }
        else if (textMessage.ApplicationStanzaNamespace.Contains("Mute"))
        {
            HandleMuteEvent(textMessage);
        }
        else if (textMessage.ApplicationStanzaNamespace.Contains("Unmute"))
        {
            HandleUnmuteEvent(textMessage);
        }
    }

    public void HandleRaiseHandEvent(IChannelTextMessage textMessage)
    {
        newMessage.text += $"\n{textMessage.ApplicationStanzaBody} has a question!";
    }

    public void HandleMuteEvent(IChannelTextMessage textMessage)
    {
        if (EasySession.mainLoginSession.LoginSessionId.Name == textMessage.ApplicationStanzaBody)
        {
            MuteLocalPlayer();
        }
    }

    public void HandleUnmuteEvent(IChannelTextMessage textMessage)
    {
        if (EasySession.mainLoginSession.LoginSessionId.Name == textMessage.ApplicationStanzaBody)
        {
            UnmuteLocalPlayer();
        }
    }




    #endregion



    #region  Initial Setup Events for when Players Login and Join Channels

    // These events will be subscribed to the common events that happen in Vivox and are inherited from the base class EasyManager.cs
    // This is an example of inheriting from EasyManager and the methods you can override
    // these events call the base(EasyManager) class. All that is in the base class is Debug.Logs()
    // You can choose not to call base in your own version of a VivoxManager/EasyManager and the Debug.Logs() will not be called
    // I will provide more explanation and alternatives to overriding base class methods in the documentation




    // Login Event Callbacks

    public override void OnLoggingIn(ILoginSession loginSession)
    {
        base.OnLoggingIn(loginSession);
        newMessage.text += $"\nLogging In {loginSession.LoginSessionId.DisplayName}";
    }

    public override void OnLoggedIn(ILoginSession loginSession)
    {
        base.OnLoggedIn(loginSession);
        newMessage.text += $"\nLogged In {loginSession.LoginSessionId.DisplayName}";
    }

    public override void OnLoggingOut(ILoginSession loginSession)
    {
        base.OnLoggingOut(loginSession);
        newMessage.text += $"\nLogging Out {loginSession.LoginSessionId.DisplayName}";
    }

    public override void OnLoggedOut(ILoginSession loginSession)
    {
        base.OnLoggedOut(loginSession);
        newMessage.text += $"\nLogged Out {loginSession.LoginSessionId.DisplayName}";
    }


    // Channel Event Callbacks

    public override void OnChannelConnecting(IChannelSession channelSession)
    {
        base.OnChannelConnecting(channelSession);
        newMessage.text += $"\nChannel Connecting in : {channelSession.Channel.Name}";
    }

    public override void OnChannelConnected(IChannelSession channelSession)
    {
        base.OnChannelConnected(channelSession);
        newMessage.text += $"\nChannel Connected in : {channelSession.Channel.Name}";
    }

    public override void OnChannelDisconnecting(IChannelSession channelSession)
    {
        base.OnChannelDisconnecting(channelSession);
        newMessage.text += $"\nChannel Disconnecting in : {channelSession.Channel.Name}";
    }

    public override void OnChannelDisconnected(IChannelSession channelSession)
    {
        base.OnChannelDisconnected(channelSession);
        newMessage.text += $"\nChannel Disconnected in : {channelSession.Channel.Name}";
    }


    // Voice Channel Event Callbacks

    public override void OnVoiceConnecting(IChannelSession channelSession)
    {
        base.OnVoiceConnecting(channelSession);
        newMessage.text += $"\nVoice Connecting in Channel : {channelSession.Channel.Name}";
    }

    public override void OnVoiceConnected(IChannelSession channelSession)
    {
        base.OnVoiceConnected(channelSession);
        newMessage.text += $"\nVoice Connected in Channel : {channelSession.Channel.Name}";
    }

    public override void OnVoiceDisconnecting(IChannelSession channelSession)
    {
        base.OnVoiceDisconnecting(channelSession);
        newMessage.text += $"\nVoice Disconnecting in Channel : {channelSession.Channel.Name}";
    }

    public override void OnVoiceDisconnected(IChannelSession channelSession)
    {
        base.OnVoiceDisconnected(channelSession);
        newMessage.text += $"\nVoice Disconnected in Channel : {channelSession.Channel.Name}";
    }



    // Text Channels Event Callbacks

    public override void OnTextChannelConnected(IChannelSession channelSession)
    {
        base.OnTextChannelConnected(channelSession);
        newMessage.text += $"\nText Connected in Channel : {channelSession.Channel.Name}";
    }

    public override void OnTextChannelConnecting(IChannelSession channelSession)
    {
        base.OnTextChannelConnecting(channelSession);
        newMessage.text += $"\nText Connecting in Channel : {channelSession.Channel.Name}";
    }

    public override void OnTextChannelDisconnecting(IChannelSession channelSession)
    {
        base.OnTextChannelDisconnecting(channelSession);
        newMessage.text += $"\nText Disconnecting in Channel : {channelSession.Channel.Name}";
    }

    public override void OnTextChannelDisconnected(IChannelSession channelSession)
    {
        base.OnTextChannelDisconnected(channelSession);
        newMessage.text += $"\nText Disconnected in Channel : {channelSession.Channel.Name}";
    }



    // Message Event Callbacks

    public override void OnChannelMessageRecieved(IChannelTextMessage textMessage)
    {
        base.OnChannelMessageRecieved(textMessage);
        newMessage.text += $"\nFrom {textMessage.Sender.DisplayName} : {textMessage.Message}";
    }

    public override void OnDirectMessageRecieved(IDirectedTextMessage directedTextMessage)
    {
        base.OnDirectMessageRecieved(directedTextMessage);
        newMessage.text += $"\nFrom {directedTextMessage.Sender.DisplayName} : {directedTextMessage.Message}";
    }

    public override void OnDirectMessageFailed(IFailedDirectedTextMessage failedMessage)
    {
        base.OnDirectMessageFailed(failedMessage);
        newMessage.text += $"\nMessage failed from {failedMessage.Sender.DisplayName} : Status Code : {failedMessage.StatusCode}";
    }


    // User Event Callbacks

    public override void OnUserJoinedChannel(IParticipant participant)
    {
        base.OnUserJoinedChannel(participant);
        newMessage.text += $"\n {participant.Account.DisplayName} has joined the channel";
        if (!participant.IsSelf)
        {
            dropdown.AddValue(participant.Account.DisplayName);
        }
    }

    public override void OnUserLeftChannel(IParticipant participant)
    {
        base.OnUserLeftChannel(participant);
        newMessage.text += $"\n {participant.Account.DisplayName} has left the channel";
        if (!participant.IsSelf)
        {
            dropdown.RemoveValue(participant.Account.DisplayName);
        }
    }

    public override void OnUserValuesUpdated(IParticipant participant)
    {
        base.OnUserValuesUpdated(participant);
        // fires everytime user value is updated. Fires way to much but left it here if you need access to it
    }



    // User Audio Event Callbacks

    public override void OnUserMuted(IParticipant participant)
    {
        base.OnUserMuted(participant);
        newMessage.text += $"\n {participant.Account.DisplayName} has been muted";
    }

    public override void OnUserUnmuted(IParticipant participant)
    {
        base.OnUserUnmuted(participant);
        newMessage.text += $"\n {participant.Account.DisplayName} has been unmuted";
    }

    public override void OnUserNotSpeaking(IParticipant participant)
    {
        base.OnUserNotSpeaking(participant);
        // use to toggle speaking icons
    }

    public override void OnUserSpeaking(IParticipant participant)
    {
        base.OnUserSpeaking(participant);
        // use to toggle speaking icons
    }




    // Text-To-Speech Event Callbacks

    public override void OnTTSMessageAdded(ITTSMessageQueueEventArgs ttsArgs)
    {
        base.OnTTSMessageAdded(ttsArgs);
        newMessage.text += $"\n Text-To-Speech Message Added : {ttsArgs.Message.Text}";
    }

    public override void OnTTSMessageRemoved(ITTSMessageQueueEventArgs ttsArgs)
    {
        base.OnTTSMessageRemoved(ttsArgs);
        newMessage.text += $"\n Text-To-Speech Message Removed : {ttsArgs.Message.Text}";
    }

    public override void OnTTSMessageUpdated(ITTSMessageQueueEventArgs ttsArgs)
    {
        base.OnTTSMessageUpdated(ttsArgs);
        newMessage.text += $"\n Text-To-Speech Message Updated : {ttsArgs.Message.Text}";
    }



    #endregion



}
