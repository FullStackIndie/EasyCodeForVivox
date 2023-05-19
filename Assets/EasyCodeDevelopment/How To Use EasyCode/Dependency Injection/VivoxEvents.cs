using EasyCodeForVivox.Events;
using UnityEngine;
using VivoxUnity;
using Zenject;

public class VivoxEvents : MonoBehaviour
{
    EasyEvents _events;

    [Inject]
    private void Initialize(EasyEvents events)
    {
        _events = events;
    }

    public void SubscribeToLoginEvents()
    {
        _events.LoggingIn += OnLoggingIn;
        _events.LoggedIn += OnLoggedIn;
        _events.LoggingOut += OnLoggingOut;
        _events.LoggedOut += OnLoggedOut;

        _events.LoginAdded += OnLoginAdded;
        _events.LoginRemoved += OnLoginRemoved;
        _events.LoginUpdated += OnLoginUpdated;
    }

    public void UnsubscribeFromLoginEvents()
    {
        _events.LoggingIn -= OnLoggingIn;
        _events.LoggedIn -= OnLoggedIn;
        _events.LoggingOut -= OnLoggingOut;
        _events.LoggedOut -= OnLoggedOut;

        _events.LoginAdded -= OnLoginAdded;
        _events.LoginRemoved -= OnLoginRemoved;
        _events.LoginUpdated -= OnLoginUpdated;
    }


    public void SubscribeToChannelEvents()
    {
        _events.ChannelConnecting += OnChannelConnecting;
        _events.ChannelConnected += OnChannelConnected;
        _events.ChannelDisconnecting += OnChannelDisconnecting;
        _events.ChannelDisconnected += OnChannelDisconnected;
    }

    public void UnsubscribeFromChannelEvents()
    {
        _events.ChannelConnecting -= OnChannelConnecting;
        _events.ChannelConnected -= OnChannelConnected;
        _events.ChannelDisconnecting -= OnChannelDisconnecting;
        _events.ChannelDisconnected -= OnChannelDisconnected;
    }

    public void SubscribeToAudioChannelEvents()
    {
        _events.AudioChannelConnecting += OnAudioChannelConnecting;
        _events.AudioChannelConnected += OnAudioChannelConnected;
        _events.AudioChannelDisconnecting += OnAudioChannelDisconnecting;
        _events.AudioChannelDisconnected += OnAudioChannelDisconnected;
    }

    public void UnsubscribeFromAudioChannelEvents()
    {
        _events.AudioChannelConnecting -= OnAudioChannelConnecting;
        _events.AudioChannelConnected -= OnAudioChannelConnected;
        _events.AudioChannelDisconnecting -= OnAudioChannelDisconnecting;
        _events.AudioChannelDisconnected -= OnAudioChannelDisconnected;
    }

    public void SubscribeToTextChannelEvents()
    {
        _events.TextChannelConnecting += OnTextChannelConnecting;
        _events.TextChannelConnected += OnTextChannelConnected;
        _events.TextChannelDisconnecting += OnTextChannelDisconnecting;
        _events.TextChannelDisconnected += OnTextChannelDisconnected;
    }

    public void UnsubscribeFromTextChannelEvents()
    {
        _events.TextChannelConnecting -= OnTextChannelConnecting;
        _events.TextChannelConnected -= OnTextChannelConnected;
        _events.TextChannelDisconnecting -= OnTextChannelDisconnecting;
        _events.TextChannelDisconnected -= OnTextChannelDisconnected;
    }

    public void SubscribeToMessageEvents()
    {
        _events.ChannelMessageRecieved += OnChannelMessageRecieved;

        _events.DirectMessageRecieved += OnDirectMessageRecieved;
        _events.DirectMessageFailed += OnDirectMessageFailed;
    }

    public void UnsubscribeFromMessageEvents()
    {
        _events.ChannelMessageRecieved -= OnChannelMessageRecieved;

        _events.DirectMessageRecieved -= OnDirectMessageRecieved;
        _events.DirectMessageFailed -= OnDirectMessageFailed;
    }

    public void SubscribeToMuteEvents()
    {
        _events.UserMuted += OnUserMuted;
        _events.UserUnmuted += OnUserUnmuted;
        _events.UserSpeaking += OnUserSpeaking;
        _events.UserNotSpeaking += OnUserNotSpeaking;
        _events.LocalUserMuted += OnLocalUserMuted;
        _events.LocalUserUnmuted += OnLocalUserUnmuted;
        _events.UserCrossMuted += OnCrossMuted;
        _events.UserCrossUnmuted += OnCrossUnmuted;
    }

    public void UnsubscribeFromMuteEvents()
    {
        _events.UserMuted -= OnUserMuted;
        _events.UserUnmuted -= OnUserUnmuted;
        _events.UserSpeaking -= OnUserSpeaking;
        _events.UserNotSpeaking -= OnUserNotSpeaking;
        _events.LocalUserMuted -= OnLocalUserMuted;
        _events.LocalUserUnmuted -= OnLocalUserUnmuted;
        _events.UserCrossMuted -= OnCrossMuted;
        _events.UserCrossUnmuted -= OnCrossUnmuted;
    }

    public void SubscribeToUserEvents()
    {
        _events.UserJoinedChannel += OnUserJoinedChannel;
        _events.UserLeftChannel += OnUserLeftChannel;
        _events.UserValuesUpdated += OnUserValuesUpdated;
    }

    public void UnsubscribeFromUserEvents()
    {
        _events.UserJoinedChannel -= OnUserJoinedChannel;
        _events.UserLeftChannel -= OnUserLeftChannel;
        _events.UserValuesUpdated -= OnUserValuesUpdated;
    }


    public void SubscribeToAudioDeviceEvents()
    {
        _events.AudioInputDeviceAdded += OnAudioInputDeviceAdded;
        _events.AudioInputDeviceRemoved += OnAudioInputDeviceRemoved;
        _events.AudioInputDeviceUpdated += OnAudioInputDeviceUpdated;

        _events.AudioOutputDeviceAdded += OnAudioOutputDeviceAdded;
        _events.AudioOutputDeviceRemoved += OnAudioOutputDeviceRemoved;
        _events.AudioOutputDeviceUpdated += OnAudioOutputDeviceUpdated;
    }

    public void UnsubscribeFromAudioDeviceEvents()
    {
        _events.AudioInputDeviceAdded -= OnAudioInputDeviceAdded;
        _events.AudioInputDeviceRemoved -= OnAudioInputDeviceRemoved;
        _events.AudioInputDeviceUpdated -= OnAudioInputDeviceUpdated;

        _events.AudioOutputDeviceAdded -= OnAudioOutputDeviceAdded;
        _events.AudioOutputDeviceRemoved -= OnAudioOutputDeviceRemoved;
        _events.AudioOutputDeviceUpdated -= OnAudioOutputDeviceUpdated;
    }


    public void SubscribeToTTAEvents()
    {
        _events.TTSMessageAdded += OnTTSMessageAdded;
        _events.TTSMessageRemoved += OnTTSMessageRemoved;
        _events.TTSMessageUpdated += OnTTSMessageUpdated;
    }

    public void UnsubscribeFromTTSEvents()
    {
        _events.TTSMessageAdded -= OnTTSMessageAdded;
        _events.TTSMessageRemoved -= OnTTSMessageRemoved;
        _events.TTSMessageUpdated -= OnTTSMessageUpdated;
    }


    protected virtual void OnLoggingIn(ILoginSession loginSession)
    {
        Debug.Log($"Logging In : {loginSession.LoginSessionId.DisplayName}");
    }

    protected virtual void OnLoggedIn(ILoginSession loginSession)
    {
        Debug.Log($"Logged in : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
    }

    protected virtual void OnLoggingOut(ILoginSession loginSession)
    {
        Debug.Log($"Logging out : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
    }

    protected virtual void OnLoggedOut(ILoginSession loginSession)
    {
        Debug.Log($"Logged out : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
    }

    protected virtual void OnLoginAdded(AccountId accountId)
    {
        Debug.Log($"LoginSession Added : For user {accountId.Name}");
    }

    protected virtual void OnLoginRemoved(AccountId accountId)
    {
        Debug.Log($"Login Removed : For user {accountId}");
    }

    protected virtual void OnLoginUpdated(ILoginSession loginSession)
    {
        Debug.Log($"LoginSession has been Updated : {loginSession.LoginSessionId.DisplayName} : Presence = {loginSession.Presence.Status}");
    }



    protected virtual void OnChannelConnecting(IChannelSession channelSession)
    {
        Debug.Log($"{channelSession.Channel.Name} Is Connecting");
    }

    protected virtual void OnChannelConnected(IChannelSession channelSession)
    {
        Debug.Log($"{channelSession.Channel.Name} Has Connected : Channel Type == {channelSession.Channel.Type}");
    }

    protected virtual void OnChannelDisconnecting(IChannelSession channelSession)
    {
        Debug.Log($"{channelSession.Channel.Name} Is Disconnecting");
    }

    protected virtual void OnChannelDisconnected(IChannelSession channelSession)
    {
        Debug.Log($"{channelSession.Channel.Name} Has Disconnected");
    }


    protected virtual void OnAudioChannelConnecting(IChannelSession channelSession)
    {
        Debug.Log($"{channelSession.Channel.Name} Audio Is Connecting In Channel");
    }

    protected virtual void OnAudioChannelConnected(IChannelSession channelSession)
    {
        Debug.Log($"{channelSession.Channel.Name} Audio Has Connected In Channel");
    }

    protected virtual void OnAudioChannelDisconnecting(IChannelSession channelSession)
    {
        Debug.Log($"{channelSession.Channel.Name} Audio Is Disconnecting In Channel");
    }

    protected virtual void OnAudioChannelDisconnected(IChannelSession channelSession)
    {
        Debug.Log($"{channelSession.Channel.Name} Audio Has Disconnected In Channel");
    }


    protected virtual void OnTextChannelConnecting(IChannelSession channelSession)
    {
        Debug.Log($"{channelSession.Channel.Name} Text Is Connecting In Channel");
    }

    protected virtual void OnTextChannelConnected(IChannelSession channelSession)
    {
        Debug.Log($"{channelSession.Channel.Name} Text Has Connected In Channel");
    }

    protected virtual void OnTextChannelDisconnecting(IChannelSession channelSession)
    {
        Debug.Log($"{channelSession.Channel.Name} Text Is Disconnecting In Channel");
    }

    protected virtual void OnTextChannelDisconnected(IChannelSession channelSession)
    {
        Debug.Log($"{channelSession.Channel.Name} Text Has Disconnected In Channel");
    }


    protected virtual void OnChannelMessageRecieved(IChannelTextMessage textMessage)
    {
        Debug.Log($"From {textMessage.Sender.DisplayName} : {textMessage.ReceivedTime} : {textMessage.Message}");
    }

    protected virtual void OnEventMessageRecieved(IChannelTextMessage textMessage)
    {
        Debug.Log($"Event Message From {textMessage.Sender.DisplayName} : {textMessage.ReceivedTime} : {textMessage.ApplicationStanzaNamespace} : {textMessage.ApplicationStanzaBody} : {textMessage.Message}");
    }

    protected virtual void OnDirectMessageRecieved(IDirectedTextMessage directedTextMessage)
    {
        Debug.Log($"Recived Message From : {directedTextMessage.Sender.DisplayName} : {directedTextMessage.ReceivedTime} : {directedTextMessage.Message}");
    }

    protected virtual void OnDirectMessageFailed(IFailedDirectedTextMessage failedMessage)
    {
        Debug.Log($"Failed To Send Message From : {failedMessage.Sender}");
    }



    protected virtual void OnLocalUserMuted()
    {
        Debug.Log("Local User is Muted");
    }

    protected virtual void OnLocalUserUnmuted()
    {
        Debug.Log("Local User is Unmuted");
    }

    protected virtual void OnCrossMuted(AccountId accountId)
    {
        Debug.Log($"Player {accountId.DisplayName} has been Cross Muted");
    }

    protected virtual void OnCrossUnmuted(AccountId accountId)
    {
        Debug.Log($"Player {accountId.DisplayName} has been Cross Unmuted");
    }

    protected virtual void OnUserMuted(IParticipant participant)
    {
        Debug.Log($"{participant.Account.DisplayName} Is Muted : (Muted For All : {participant.IsMutedForAll})");
    }

    protected virtual void OnUserUnmuted(IParticipant participant)
    {
        Debug.Log($"{participant.Account.DisplayName} Is Unmuted : (Muted For All : {participant.IsMutedForAll})");
    }

    protected virtual void OnUserSpeaking(IParticipant participant)
    {
        Debug.Log($"{participant.Account.DisplayName} Is Speaking : Audio Energy {participant.AudioEnergy}");
    }

    protected virtual void OnUserNotSpeaking(IParticipant participant)
    {
        Debug.Log($"{participant.Account.DisplayName} Is Not Speaking");
    }




    protected virtual void OnUserJoinedChannel(IParticipant participant)
    {
        Debug.Log($"{participant.Account.DisplayName} Has Joined The Channel");
    }

    protected virtual void OnUserLeftChannel(IParticipant participant)
    {
        Debug.Log($"{participant.Account.DisplayName} Has Left The Channel");
    }

    protected virtual void OnUserValuesUpdated(IParticipant participant)
    {
        Debug.Log($"{participant.Account.DisplayName} Has updated itself in the channel");
    }


    #region Audio Device Events

    protected virtual void OnAudioInputDeviceAdded(IAudioDevice audioDevice)
    {
        Debug.Log($"Audio Input device has been added {audioDevice?.Name}");
    }

    protected virtual void OnAudioInputDeviceRemoved(IAudioDevice audioDevice)
    {
        Debug.Log($"Audio Input device has been removed {audioDevice?.Name}");
    }

    protected virtual void OnAudioInputDeviceUpdated(IAudioDevice audioDevice)
    {
        Debug.Log($"Audio Input Device has been changed to {audioDevice?.Name}");
    }

    protected virtual void OnAudioOutputDeviceAdded(IAudioDevice audioDevice)
    {
        Debug.Log($"Audio Output device has been added {audioDevice?.Name}");
    }

    protected virtual void OnAudioOutputDeviceRemoved(IAudioDevice audioDevice)
    {
        Debug.Log($"Audio Output device has been removed {audioDevice?.Name}");
    }

    protected virtual void OnAudioOutputDeviceUpdated(IAudioDevice audioDevice)
    {
        Debug.Log($"Audio Output Device has been changed to {audioDevice?.Name}");
    }


    #endregion


    protected virtual void OnTTSMessageAdded(ITTSMessageQueueEventArgs ttsArgs)
    {
        Debug.Log($"TTS Message Has Been Added : {ttsArgs.Message.Text}");
    }

    protected virtual void OnTTSMessageRemoved(ITTSMessageQueueEventArgs ttsArgs)
    {
        Debug.Log($"TTS Message Has Been Removed : {ttsArgs.Message.Text}");
    }

    protected virtual void OnTTSMessageUpdated(ITTSMessageQueueEventArgs ttsArgs)
    {
        Debug.Log($"TTS Message Has Been Updated : {ttsArgs.Message.Text}");
    }

}
