using EasyCodeForVivox;
using EasyCodeForVivox.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using VivoxUnity;

public class VivoxManager : EasyManager
{

    private async void Awake()
    {
        VivoxConfig vivoxConfig = new VivoxConfig();
        vivoxConfig.MaxLoginsPerUser = 3;

        await InitializeClient(vivoxConfig);
    }

    private void OnApplicationQuit()
    {
        UnitializeClient();
    }


    public void Login()
    {
        LoginToVivox("username", displayName: "gamerTag");
    }

    public void LoginAsMuted()
    {
        LoginToVivox("username", displayName: "gamerTag", joinMuted: true);
    }

    public void Logout()
    {
        LogoutOfVivox("username");
    }

    public void UpdateLoginProperties()
    {
        UpdateLoginProperties("username", VivoxUnity.ParticipantPropertyUpdateFrequency.StateChange);
    }

    public void SetPlayerTransmissionModeAll()
    {
        SetPlayerTransmissionMode("username", TransmissionMode.All);
    }

    public void SetPlayerTransmissionModeSingle()
    {
        var channelId = GetChannelId("username", "channelName");
        SetPlayerTransmissionMode("username", TransmissionMode.Single, channelId);
    }

    public void JoinEchoChannel()
    {
        JoinChannel("username", "echo", true, false, true, ChannelType.Echo, joinMuted: false);
    }

    public void JoinChannel()
    {
        JoinChannel("username", "chat", true, true, false, ChannelType.NonPositional, joinMuted: false);
    }

    public void Join3DChannel()
    {
        var channelProperties = new Channel3DProperties(32, 1, 1.0f, AudioFadeModel.InverseByDistance);

        JoinChannel("username", "3D", true, false, false, ChannelType.Positional, joinMuted: false, channel3DProperties: channelProperties);
    }


    public void Join3DRegionChannel()
    {
        var channelProperties = new Channel3DProperties(32, 1, 1.0f, AudioFadeModel.InverseByDistance);

        // using match hash
        var matchHash = "lobby name".GetMD5Hash();
        JoinChannelRegion("userName", "3D", "NA", matchHash, true, false, false, ChannelType.Positional, joinMuted: false, channel3DProperties: channelProperties);
    }

    public void JoinSquadRegionChannel()
    {
        var channelProperties = new Channel3DProperties(32, 1, 1.0f, AudioFadeModel.InverseByDistance);

        // using match hash
        var matchHash = "lobby name".GetMD5Hash();
        JoinChannelRegion("userName", "sqaud1", "NA", matchHash, true, false, false, ChannelType.Positional, joinMuted: false, channel3DProperties: channelProperties);
    }

    public void ToggleAudioInChannel()
    {
        ToggleAudioInChannel("3d", true);
    }

    public void ToggleTextInChannel()
    {
        ToggleTextInChannel("chat", true);
    }

    public void SendChannelMessage()
    {
        SendChannelMessage("userName", "chat", "my message to everyone");
        SendChannelMessage("userName", "chat", "my message to everyone", header: "squad", body: "1");
    }


    public void SendDirectMessage()
    {
        SendDirectMessage("userName", "myFriendsName", "my message to everyone");
        SendDirectMessage("userName", "myFriendsName", "my message to everyone", header: "squad", body: "1");
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
        LocalMuteRemoteUser("remotePlayer", "chat");
    }

    public void UnmuteRemotePlayer()
    {
        LocalUnmuteRemoteUser("remotePlayer", "chat");
    }

    public void MuteAllPlayers()
    {
        LocalMuteAllPlayers("chat");
    }

    public void UnmuteAllPlayers()
    {
        LocalUnmuteAllPlayers("chat");
    }

    public void CrossMuteUser()
    {
        CrossMuteUser("userName", "channelName", "usernameToMute", true);
    }

    public void CrossUnmuteUser()
    {
        CrossMuteUser("userName", "channelName", "usernameToMute", false);
    }

    public void CrossMuteUsers()
    {
        List<string> usersToMute = new List<string>() { "player1", "player2" };
        CrossMuteUsers("userName", "channelName", usersToMute, true);
    }

    public void CrossUnmuteUsers()
    {
        List<string> usersToMute = new List<string>() { "player1", "player2" };
        CrossMuteUsers("userName", "channelName", usersToMute, false);
    }

    public void ClearCrossMutedUsersForLoginSession()
    {
        ClearCrossMutedUsersForLoginSession("userName");
    }

    public void AdjustLocalPlayerVolume()
    {
        AdjustLocalUserVolume(15);
    }

    public void AdjustRemotePlayerVolume()
    {
        AdjustRemoteUserVolume("userName", "channelName", 15);
    }

    public void SetAutoVoiceActivity()
    {
        SetAutoVoiceActivityDetection("userName");
    }

    public void SetVoiceActivity()
    {
        SetVoiceActivityDetection("userName", hangover: 2000, sensitivity: 43, noiseFloor: 576);
    }

    public void GetAllAudioInputDevices()
    {
        var audioDevices = GetAudioInputDevices();
    }

    public void GetAllAudioOutputDevices()
    {
        var audioDevices = GetAudioOutputDevices();
    }

    public void SetAudioInputDevice()
    {
        var audioDevices = GetAudioInputDevices().ToList();
        foreach (var device in audioDevices)
        {
            Debug.Log(device.Name);
        }
        SetAudioInputDevice(audioDevices.First().Name);
    }

    public void SetAudioOutputDevice()
    {
        var audioDevices = GetAudioOutputDevices().ToList();
        foreach (var device in audioDevices)
        {
            Debug.Log(device.Name);
        }
        SetAudioOutputDevice(audioDevices.First().Name);
    }

    public void RefreshAllAudioDevices()
    {
        RefreshAudioInputDevices();
        RefreshAudioOutputDevices();
    }


    public void StartInjectingAudio()
    {
        StartAudioInjection("userName", $"{Directory.GetCurrentDirectory()}/pathToFile");
    }

    public void StopInjectingAudio()
    {
        StopAudioInjection("userName");
    }


    public void SpeakTTS()
    {

    }


    public void ChooseVivoxVoice()
    {
        ChooseVoiceGender(VoiceGender.female, "userName");
    }


    public void TTSMsgLocalPlayOverCurrent()
    {
        PlayTTSMessage("my message to play", "userName", TTSDestination.LocalPlayback);
    }

    public void TTSMsgRemotePlayOverCurrent()
    {
        PlayTTSMessage("my message to play", "userName", TTSDestination.RemoteTransmission);
    }

    public void TTSMsgLocalRemotePlayOverCurrent()
    {
        PlayTTSMessage("my message to play", "userName", TTSDestination.RemoteTransmissionWithLocalPlayback);
    }

    public void TTSMsgQueueLocal()
    {
        PlayTTSMessage("my message to play", "userName", TTSDestination.QueuedLocalPlayback);
    }

    public void TTSMsgQueueRemote()
    {
        PlayTTSMessage("my message to play", "userName", TTSDestination.QueuedRemoteTransmission);
    }

    public void TTSMsgQueueRemoteLocal()
    {
        PlayTTSMessage("my message to play", "userName", TTSDestination.QueuedRemoteTransmissionWithLocalPlayback);
    }

    public void TTSMsgLocalReplaceCurrentMessagePlaying()
    {
        PlayTTSMessage("my message to play", "userName", TTSDestination.ScreenReader);
    }





    // Login Event Callbacks

    protected override void OnLoggingIn(ILoginSession loginSession)
    {
        base.OnLoggingIn(loginSession);
    }

    protected override void OnLoggedIn(ILoginSession loginSession)
    {
        base.OnLoggedIn(loginSession);
    }

    protected override void OnLoggingOut(ILoginSession loginSession)
    {
        base.OnLoggingOut(loginSession);
    }

    protected override void OnLoggedOut(ILoginSession loginSession)
    {
        base.OnLoggedOut(loginSession);
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
    }

    protected override void OnChannelConnected(IChannelSession channelSession)
    {
        base.OnChannelConnected(channelSession);
    }

    protected override void OnChannelDisconnecting(IChannelSession channelSession)
    {
        base.OnChannelDisconnecting(channelSession);
    }

    protected override void OnChannelDisconnected(IChannelSession channelSession)
    {
        base.OnChannelDisconnected(channelSession);
    }


    // Voice Channel Event Callbacks

    protected override void OnAudioChannelConnecting(IChannelSession channelSession)
    {
        base.OnAudioChannelConnecting(channelSession);
    }

    protected override void OnAudioChannelConnected(IChannelSession channelSession)
    {
        base.OnAudioChannelConnected(channelSession);
    }

    protected override void OnAudioChannelDisconnecting(IChannelSession channelSession)
    {
        base.OnAudioChannelDisconnecting(channelSession);
    }

    protected override void OnAudioChannelDisconnected(IChannelSession channelSession)
    {
        base.OnAudioChannelDisconnected(channelSession);
    }


    // Text Channels Event Callbacks

    protected override void OnTextChannelConnecting(IChannelSession channelSession)
    {
        base.OnTextChannelConnecting(channelSession);
    }

    protected override void OnTextChannelConnected(IChannelSession channelSession)
    {
        base.OnTextChannelConnected(channelSession);
    }

    protected override void OnTextChannelDisconnecting(IChannelSession channelSession)
    {
        base.OnTextChannelDisconnecting(channelSession);
    }

    protected override void OnTextChannelDisconnected(IChannelSession channelSession)
    {
        base.OnTextChannelDisconnected(channelSession);
    }



    // Message Event Callbacks

    protected override void OnChannelMessageRecieved(IChannelTextMessage textMessage)
    {
        base.OnChannelMessageRecieved(textMessage);
    }

    protected override void OnDirectMessageRecieved(IDirectedTextMessage directedTextMessage)
    {
        base.OnDirectMessageRecieved(directedTextMessage);
    }

    protected override void OnDirectMessageFailed(IFailedDirectedTextMessage failedMessage)
    {
        base.OnDirectMessageFailed(failedMessage);
    }








    // User Audio Event Callbacks

    protected override void OnUserMuted(IParticipant participant)
    {
        base.OnUserMuted(participant);
    }

    protected override void OnUserUnmuted(IParticipant participant)
    {
        base.OnUserUnmuted(participant);
    }

    protected override void OnUserNotSpeaking(IParticipant participant)
    {
        base.OnUserNotSpeaking(participant);
    }

    protected override void OnUserSpeaking(IParticipant participant)
    {
        base.OnUserSpeaking(participant);
    }

    protected override void OnLocalUserMuted()
    {
        base.OnLocalUserMuted();
    }

    protected override void OnLocalUserUnmuted()
    {
        base.OnLocalUserUnmuted();
    }

    protected override void OnCrossMuted(AccountId accountId)
    {
        base.OnCrossMuted(accountId);
    }

    protected override void OnCrossUnmuted(AccountId accountId)
    {
        base.OnCrossUnmuted(accountId);
    }



    protected override void OnUserJoinedChannel(IParticipant participant)
    {
        base.OnUserJoinedChannel(participant);
    }

    protected override void OnUserLeftChannel(IParticipant participant)
    {
        base.OnUserLeftChannel(participant);
    }

    protected override void OnUserValuesUpdated(IParticipant participant)
    {
        base.OnUserValuesUpdated(participant);
    }



    // Audio Device Events

    protected override void OnAudioInputDeviceAdded(IAudioDevice audioDevice)
    {
        base.OnAudioInputDeviceAdded(audioDevice);
    }

    protected override void OnAudioInputDeviceRemoved(IAudioDevice audioDevice)
    {
        base.OnAudioInputDeviceRemoved(audioDevice);
    }

    protected override void OnAudioInputDeviceUpdated(IAudioDevice audioDevice)
    {
        base.OnAudioInputDeviceUpdated(audioDevice);
    }

    protected override void OnAudioOutputDeviceAdded(IAudioDevice audioDevice)
    {
        base.OnAudioOutputDeviceAdded(audioDevice);
    }

    protected override void OnAudioOutputDeviceRemoved(IAudioDevice audioDevice)
    {
        base.OnAudioOutputDeviceRemoved(audioDevice);
    }

    protected override void OnAudioOutputDeviceUpdated(IAudioDevice audioDevice)
    {
        base.OnAudioOutputDeviceUpdated(audioDevice);
    }


    // Text-To-Speech Event Callbacks

    protected override void OnTTSMessageAdded(ITTSMessageQueueEventArgs ttsArgs)
    {
        base.OnTTSMessageAdded(ttsArgs);
    }

    protected override void OnTTSMessageRemoved(ITTSMessageQueueEventArgs ttsArgs)
    {
        base.OnTTSMessageRemoved(ttsArgs);
    }

    protected override void OnTTSMessageUpdated(ITTSMessageQueueEventArgs ttsArgs)
    {
        base.OnTTSMessageUpdated(ttsArgs);
    }


}

