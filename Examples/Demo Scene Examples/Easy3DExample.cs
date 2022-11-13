﻿using EasyCodeForVivox;
using EasyCodeForVivox.Extensions;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VivoxUnity;

public class Easy3DExample : EasyManager
{
    [SerializeField] string apiEndpoint;
    [SerializeField] string domain;
    [SerializeField] string issuer;
    [SerializeField] string secretKey;


    [SerializeField] InputField userNameInput;
    [SerializeField] InputField directMessageRemotePlayerNameInput;
    [SerializeField] InputField channelNameInput;
    [SerializeField] InputField messageInput;
    [SerializeField] Toggle voiceToggle;
    [SerializeField] Toggle textToggle;
    [SerializeField] Toggle textToSpeechToggle;
    [SerializeField] Slider localUserAudioSlider;
    [SerializeField] Slider remotePlayerAudioSlider;

    [SerializeField] TextMeshProUGUI newMessage;
    [SerializeField] Scrollbar scrollbarVertical;
    [SerializeField] TMP_Dropdown loginSessionsDropdown;
    [SerializeField] TMP_Dropdown channelSessionsDropdown;
    [SerializeField] TMP_Dropdown remotePlayerVolumeDropdown;
    [SerializeField] TMP_Dropdown remotePlayerinChannelDropdown;
    [SerializeField] TMP_Dropdown mutePlayerInChannelDropdown;
    [SerializeField] TMP_Dropdown audioCaptureDevicesDropdown;
    [SerializeField] TMP_Dropdown audioRenderDevicesDropdown;
    [SerializeField] TMP_Dropdown textToSpeechOptionsDropdown;

    private PanelSwitcher panelSwitcher;

    private void OnApplicationQuit()
    {
        UnitializeClient();
    }

    private void Awake()
    {
        // todo Implement Unity Remote Config to store sensitive information in the cloud. It's free
        // if users dont want to use Remote Config then advise users to use environment variables instead of hardcoding secrets/api keys
        // inside of the Unity Editor because hackers can decompile there game and steal the secrets/keys
        // Unity decompiler https://devxdevelopment.com/
        EasySession.APIEndpoint = new Uri(apiEndpoint);
        EasySession.Domain = domain;
        EasySession.Issuer = issuer;
        EasySession.SecretKey = secretKey;
        panelSwitcher = FindObjectOfType<PanelSwitcher>();
    }

    async void Start()
    {
        VivoxConfig vivoxConfig = new VivoxConfig();
        vivoxConfig.MaxLoginsPerUser = 3;

        await InitializeClient(vivoxConfig);

        LoadAudioDevices();
        LoadTextToSpeechOptions();
    }


    // Clears Text messages where event logs show up in demo scene
    // hooked up to ClearMessages Button in demo scene
    public void ClearMessages()
    {
        newMessage.text = "";
        scrollbarVertical.value = 1;
    }

    public void LoadAudioDevices()
    {
        foreach (var device in EasySession.Client.AudioInputDevices.AvailableDevices)
        {
            audioCaptureDevicesDropdown.AddValue(device.Name);
        }
        foreach (var device in EasySession.Client.AudioOutputDevices.AvailableDevices)
        {
            audioRenderDevicesDropdown.AddValue(device.Name);
        }
    }

    public void LoadTextToSpeechOptions()
    {
        foreach (var tts in Enum.GetValues(typeof(TTSDestination)))
        {
            textToSpeechOptionsDropdown.AddValue(tts.ToString());
        }
    }

    public void LoadExistingData()
    {
        var userName = EasySession.LoginSessions?.FirstOrDefault().Value.LoginSessionId.DisplayName;
        var channelName = EasySession.ChannelSessions?.FirstOrDefault().Value.Channel.Name;
        userNameInput.text = userName;
        channelNameInput.text = channelName;
        // todo finsish loading data for when you leave network 
        // todo finish doing if checks for Log settings

    }

    public void Login()
    {
        LoginToVivox(userNameInput.text);
    }

    public void Logout()
    {
        LogoutOfVivox(loginSessionsDropdown.GetSelected());
    }

    public void JoinChannel()
    {
        JoinChannel(userNameInput.text, channelNameInput.text, true, true, false, ChannelType.NonPositional);
    }

    public void Join3DPositionalChannel()
    {
        JoinChannel(userNameInput.text, "3D", true, false, false, ChannelType.Positional);
    }

    public void JoinEchoChannel()
    {
        JoinChannel(userNameInput.text, channelNameInput.text, true, true, true, ChannelType.Echo);
    }

    public void SwitchChannel()
    {
        //JoinChannel(userName.text, "3D", true, false, true, ChannelType.Positional);
        // figure ot how settransmission works
        JoinChannel(loginSessionsDropdown.GetSelected(), channelNameInput.text, true, true, true, ChannelType.NonPositional);
    }

    public void SendMessage()
    {
        if (string.IsNullOrEmpty(channelNameInput.text) || string.IsNullOrEmpty(messageInput.text))
        {
            Debug.Log("Channel name or message is empty");
            return;
        }
        SendChannelMessage(loginSessionsDropdown.GetSelected(), channelSessionsDropdown.GetSelected(), messageInput.text);
    }

    public void SendDirectMessageToPlayer()
    {
        if (!string.IsNullOrEmpty(directMessageRemotePlayerNameInput.text))
        {
            SendDirectMessage(loginSessionsDropdown.GetSelected(), directMessageRemotePlayerNameInput.text, messageInput.text);
        }
        else
        {
            Debug.Log("Remote Player Name is Empty or Invalid, Cannot send Message");
        }
    }

    public void LeaveChannel()
    {
        LeaveChannel(channelNameInput.text, loginSessionsDropdown.GetSelected());
    }

    public void ToggleAudioInChannel()
    {
        ToggleAudioInChannel(channelNameInput.text, voiceToggle.isOn);
    }

    public void ToggleTextInChannel()
    {
        ToggleTextInChannel(channelNameInput.text, textToggle.isOn);
    }

    public void MuteLocalPlayer()
    {
        MuteSelf();
    }

    public void UnmuteLocalPlayer()
    {
        UnmuteSelf();
    }

    public void MuteRemotePlayer()
    {
        var selectedUser = mutePlayerInChannelDropdown.GetSelected();
        if (selectedUser != null)
        {
            LocalMuteRemoteUser(selectedUser, channelNameInput.text);
        }
        else
        {
            Debug.Log("Remote Player Name is Empty or Invalid, Cannot mute player");
        }
    }

    public void UnmuteRemotePlayer()
    {
        var selectedUser = mutePlayerInChannelDropdown.GetSelected();
        if (selectedUser != null)
        {
            LocalUnmuteRemoteUser(selectedUser, channelNameInput.text);
        }
        else
        {
            Debug.Log("Remote Player Name is Empty or Invalid, Cannot mute player");
        }
    }

    public void MuteAllPlayers()
    {
        LocalMuteAllPlayers(channelNameInput.text);
    }

    public void UnmuteAllPlayers()
    {
        LocalUnmuteAllPlayers(channelNameInput.text);
    }

    public void AdjustLocalSelfVolume()
    {
        AdjustLocalUserVolume(Mathf.RoundToInt(localUserAudioSlider.value));
    }

    public void AdjustRemotePlayerVolume()
    {
        var selectedUser = remotePlayerVolumeDropdown.GetSelected();
        if (selectedUser != null)
        {
            AdjustRemoteUserVolume(selectedUser, channelNameInput.text, Mathf.RoundToInt(remotePlayerAudioSlider.value));
        }
        else
        {
            Debug.Log("Remote Player Name is Empty or Invalid, Cannot adjust player volume");
        }
    }

    public void CrossMute()
    {
        CrossMuteUser(loginSessionsDropdown.GetSelected(), channelSessionsDropdown.GetSelected(), remotePlayerVolumeDropdown.GetSelected(), true);
    }

    public void CrossUnmute()
    {
        CrossMuteUser(loginSessionsDropdown.GetSelected(), channelSessionsDropdown.GetSelected(), remotePlayerVolumeDropdown.GetSelected(), false);
    }

    public void ClearCrossUnmute()
    {
        ClearCrossMutedUsersForLoginSession(loginSessionsDropdown.GetSelected());
    }

    public void InjectAudio()
    {
        InjectAudio(loginSessionsDropdown.GetSelected(), @"Assets\EasyCodeForVivox\Resources\Over_the_Horizon.wav");
    }

    public void StopInjectedAudio()
    {
        StopInjectedAudio(loginSessionsDropdown.GetSelected());
    }

    public void SetAudioInputDevice()
    {
        SetAudioInputDevice(audioCaptureDevicesDropdown.GetSelected());
    }

    public void SetAudioOutputDevice()
    {
        SetAudioOutputDevice(audioRenderDevicesDropdown.GetSelected());
    }


    public void EnablePushToTalk()
    {
        EnablePushToTalk(true, KeyCode.Space);
    }

    public void DisablePushToTalk()
    {
        EnablePushToTalk(false, KeyCode.Space);
    }





    #region  Demo Scene Events for Admin Panel - Raise Hand, Mute Player, and Unmute Player


    // Event Messages for Buttons in Demo Scene to RaiseHand, Mute globally or Unmute globally in channel
    // For teaching and Admin settings. Like discord or zoom call without the video
    // This is example is if you are not using a Networking Stack like Mirror, Photon, MLAPI, 
    // or NetCode for GameObjects by Unity. You should handle this type of logic on the server
    // and not use Vivox's event/hidden messages for this type of feature in my opinion.


    public void SendRaiseHandEventMessage()
    {
        SendEventMessage(userNameInput.text, channelNameInput.text, "event", "Event:RaiseHand", EasySession.LoginSessions[userNameInput.text].LoginSessionId.Name);
    }

    public void SendMuteEventMessage()
    {
        SendEventMessage(userNameInput.text, channelNameInput.text, "event", "Event:Mute", remotePlayerinChannelDropdown.GetSelected());
    }

    public void SendUnmuteEventMessage()
    {
        SendEventMessage(userNameInput.text, channelNameInput.text, "event", "Event:Unmute", remotePlayerinChannelDropdown.GetSelected());
    }


    protected override void OnEventMessageRecieved(IChannelTextMessage textMessage)
    {
        base.OnEventMessageRecieved(textMessage);
        if (textMessage.ApplicationStanzaNamespace.Contains("RaiseHand"))
        {
            HandleRaiseHandEvent(textMessage);
        }
        else if (textMessage.ApplicationStanzaNamespace.Contains("Mute"))
        {
            HandleMuteEvent(textMessage, userNameInput.text);
        }
        else if (textMessage.ApplicationStanzaNamespace.Contains("Unmute"))
        {
            HandleUnmuteEvent(textMessage, userNameInput.text);
        }
    }

    public void HandleRaiseHandEvent(IChannelTextMessage textMessage)
    {
        newMessage.text += $"\n{textMessage.ApplicationStanzaBody} has a question!";
    }

    public void HandleMuteEvent(IChannelTextMessage textMessage, string userName)
    {
        if (EasySession.LoginSessions[userName].LoginSessionId.Name == textMessage.ApplicationStanzaBody)
        {
            MuteLocalPlayer();
        }
    }

    public void HandleUnmuteEvent(IChannelTextMessage textMessage, string userName)
    {
        if (EasySession.LoginSessions[userName].LoginSessionId.Name == textMessage.ApplicationStanzaBody)
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

    protected override void OnLoggingIn(ILoginSession loginSession)
    {
        base.OnLoggingIn(loginSession);
        newMessage.text += $"\nLogging In {loginSession.LoginSessionId.DisplayName}";

    }

    protected override void OnLoggedIn(ILoginSession loginSession)
    {
        base.OnLoggedIn(loginSession);
        newMessage.text += $"\nLogged In {loginSession.LoginSessionId.DisplayName}";
        loginSessionsDropdown.AddValue(loginSession.LoginSessionId.Name);
        panelSwitcher.EnablePanel("Channel");

        // set Auto VAD for loggedin user
        SetAutoVoiceActivityDetection(loginSession.LoginSessionId.Name);
    }

    protected override void OnLoggingOut(ILoginSession loginSession)
    {
        base.OnLoggingOut(loginSession);
        newMessage.text += $"\nLogging Out {loginSession.LoginSessionId.DisplayName}";
    }

    protected override void OnLoggedOut(ILoginSession loginSession)
    {
        base.OnLoggedOut(loginSession);
        newMessage.text += $"\nLogged Out {loginSession.LoginSessionId.DisplayName}";
        loginSessionsDropdown.RemoveValue(loginSession.LoginSessionId.Name);
    }

    protected override void OnLoginAdded(AccountId accountId)
    {
        base.OnLoginAdded(accountId);
    }

    protected override void OnLoginRemoved(AccountId accountId)
    {
        base.OnLoginRemoved(accountId);
    }

    protected override void OnLoginUpdated(ILoginSession loginSession)
    {
        base.OnLoginUpdated(loginSession);
    }

    // Channel Event Callbacks

    protected override void OnChannelConnecting(IChannelSession channelSession)
    {
        base.OnChannelConnecting(channelSession);
        newMessage.text += $"\nChannel Connecting in : {channelSession.Channel.Name}";
    }

    protected override void OnChannelConnected(IChannelSession channelSession)
    {
        base.OnChannelConnected(channelSession);
        newMessage.text += $"\nChannel Connected in : {channelSession.Channel.Name}";
        channelSessionsDropdown.AddValue(channelSession.Channel.Name);
        panelSwitcher.EnablePanel("Message");
    }

    protected override void OnChannelDisconnecting(IChannelSession channelSession)
    {
        base.OnChannelDisconnecting(channelSession);
        newMessage.text += $"\nChannel Disconnecting in : {channelSession.Channel.Name}";
    }

    protected override void OnChannelDisconnected(IChannelSession channelSession)
    {
        base.OnChannelDisconnected(channelSession);
        newMessage.text += $"\nChannel Disconnected in : {channelSession.Channel.Name}";
        channelSessionsDropdown.RemoveValue(channelSession.Channel.Name);
    }


    // Voice Channel Event Callbacks

    protected override void OnAudioChannelConnecting(IChannelSession channelSession)
    {
        base.OnAudioChannelConnecting(channelSession);
        newMessage.text += $"\nVoice Connecting in Channel : {channelSession.Channel.Name}";
    }

    protected override void OnAudioChannelConnected(IChannelSession channelSession)
    {
        base.OnAudioChannelConnected(channelSession);
        newMessage.text += $"\nVoice Connected in Channel : {channelSession.Channel.Name}";
    }

    protected override void OnAudioChannelDisconnecting(IChannelSession channelSession)
    {
        base.OnAudioChannelDisconnecting(channelSession);
        newMessage.text += $"\nVoice Disconnecting in Channel : {channelSession.Channel.Name}";
    }

    protected override void OnAudioChannelDisconnected(IChannelSession channelSession)
    {
        base.OnAudioChannelDisconnected(channelSession);
        newMessage.text += $"\nVoice Disconnected in Channel : {channelSession.Channel.Name}";
    }



    // Text Channels Event Callbacks

    protected override void OnTextChannelConnecting(IChannelSession channelSession)
    {
        base.OnTextChannelConnecting(channelSession);
        newMessage.text += $"\nText Connecting in Channel : {channelSession.Channel.Name}";
    }

    protected override void OnTextChannelConnected(IChannelSession channelSession)
    {
        base.OnTextChannelConnected(channelSession);
        newMessage.text += $"\nText Connected in Channel : {channelSession.Channel.Name}";
        panelSwitcher.EnablePanel("Message");
    }

    protected override void OnTextChannelDisconnecting(IChannelSession channelSession)
    {
        base.OnTextChannelDisconnecting(channelSession);
        newMessage.text += $"\nText Disconnecting in Channel : {channelSession.Channel.Name}";
    }

    protected override void OnTextChannelDisconnected(IChannelSession channelSession)
    {
        base.OnTextChannelDisconnected(channelSession);
        newMessage.text += $"\nText Disconnected in Channel : {channelSession.Channel.Name}";
    }



    // Message Event Callbacks

    protected override void OnChannelMessageRecieved(IChannelTextMessage textMessage)
    {
        base.OnChannelMessageRecieved(textMessage);
        newMessage.text += $"\nFrom {textMessage.Sender.DisplayName} : {textMessage.Message}";
        if (textToSpeechToggle.isOn)
        {
            if (Enum.TryParse(textToSpeechOptionsDropdown.GetSelected(), out TTSDestination destination))
            {
                SpeakTTS(messageInput.text, userNameInput.text, destination);
            }
        }
    }

    protected override void OnDirectMessageRecieved(IDirectedTextMessage directedTextMessage)
    {
        base.OnDirectMessageRecieved(directedTextMessage);
        newMessage.text += $"\nFrom {directedTextMessage.Sender.DisplayName} : {directedTextMessage.Message}";
        if (textToSpeechToggle.isOn)
        {
            if (Enum.TryParse(textToSpeechOptionsDropdown.GetSelected(), out TTSDestination destination))
            {
                SpeakTTS(messageInput.text, userNameInput.text, destination);
            }
        }
    }

    protected override void OnDirectMessageFailed(IFailedDirectedTextMessage failedMessage)
    {
        base.OnDirectMessageFailed(failedMessage);
        newMessage.text += $"\nMessage failed from {failedMessage.Sender.DisplayName} : Status Code : {failedMessage.StatusCode}";
        if (textToSpeechToggle.isOn)
        {
            if (Enum.TryParse(textToSpeechOptionsDropdown.GetSelected(), out TTSDestination destination))
            {
                SpeakTTS("Failed to send message", userNameInput.text, destination);
            }
        }
    }


    // User Event Callbacks

    protected override void OnUserJoinedChannel(IParticipant participant)
    {
        base.OnUserJoinedChannel(participant);
        newMessage.text += $"\n {participant.Account.DisplayName} has joined the channel";
        if (!participant.IsSelf)
        {
            remotePlayerinChannelDropdown.AddValue(participant.Account.DisplayName);
            mutePlayerInChannelDropdown.AddValue(participant.Account.DisplayName);
            remotePlayerVolumeDropdown.AddValue(participant.Account.DisplayName);
        }

    }

    protected override void OnUserLeftChannel(IParticipant participant)
    {
        base.OnUserLeftChannel(participant);
        newMessage.text += $"\n {participant.Account.DisplayName} has left the channel";
        if (!participant.IsSelf)
        {
            remotePlayerinChannelDropdown.RemoveValue(participant.Account.DisplayName);
            mutePlayerInChannelDropdown.RemoveValue(participant.Account.DisplayName);
            remotePlayerVolumeDropdown.RemoveValue(participant.Account.DisplayName);
        }
    }

    protected override void OnUserValuesUpdated(IParticipant participant)
    {
        base.OnUserValuesUpdated(participant);
        // fires everytime user value is updated. Fires way to much but left it here if you need access to it
    }



    // User Audio Event Callbacks

    protected override void OnUserMuted(IParticipant participant)
    {
        base.OnUserMuted(participant);
        newMessage.text += $"\n {participant.Account.DisplayName} has been muted";
    }

    protected override void OnUserUnmuted(IParticipant participant)
    {
        base.OnUserUnmuted(participant);
        newMessage.text += $"\n {participant.Account.DisplayName} has been unmuted";
    }

    protected override void OnUserNotSpeaking(IParticipant participant)
    {
        base.OnUserNotSpeaking(participant);
        // use to toggle speaking icons
    }

    protected override void OnUserSpeaking(IParticipant participant)
    {
        base.OnUserSpeaking(participant);
        // use to toggle speaking icons
    }

    protected override void OnLocalUserMuted()
    {
        base.OnLocalUserMuted();
    }

    protected override void OnLocalUserUnmuted()
    {
        base.OnLocalUserUnmuted();
    }


    // Text-To-Speech Event Callbacks

    protected override void OnTTSMessageAdded(ITTSMessageQueueEventArgs ttsArgs)
    {
        base.OnTTSMessageAdded(ttsArgs);
        newMessage.text += $"\n Text-To-Speech Message Added : {ttsArgs.Message.Text}";
    }

    protected override void OnTTSMessageRemoved(ITTSMessageQueueEventArgs ttsArgs)
    {
        base.OnTTSMessageRemoved(ttsArgs);
        newMessage.text += $"\n Text-To-Speech Message Removed : {ttsArgs.Message.Text}";
    }

    protected override void OnTTSMessageUpdated(ITTSMessageQueueEventArgs ttsArgs)
    {
        base.OnTTSMessageUpdated(ttsArgs);
        newMessage.text += $"\n Text-To-Speech Message Updated : {ttsArgs.Message.Text}";
    }



    #endregion



}
