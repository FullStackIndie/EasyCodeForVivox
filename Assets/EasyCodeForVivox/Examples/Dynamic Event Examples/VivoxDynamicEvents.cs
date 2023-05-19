using EasyCodeForVivox;
using EasyCodeForVivox.Events;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox.Examples
{
    public class VivoxDynamicEvents : MonoBehaviour
    {
        [LoginEvent(LoginStatus.LoggingIn)]
        private void OnPlayerLoggingIn(ILoginSession loginSession)
        {
            Debug.Log($"Logging In : {loginSession.LoginSessionId.DisplayName}");
        }

        [LoginEvent(LoginStatus.LoggedIn)]
        private void OnPlayerLoggedIn(ILoginSession loginSession)
        {
            Debug.Log($"Logged in : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
        }

        [LoginEvent(LoginStatus.LoggingOut)]
        private void OnPlayerLoggingOut(ILoginSession loginSession)
        {
            Debug.Log($"Logging out : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
        }

        [LoginEvent(LoginStatus.LoggedOut)]
        private void OnPlayerLoggedOut(ILoginSession loginSession)
        {
            Debug.Log($"Logged out : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
        }

        [LoginEvent(LoginStatus.LoginAdded)]
        private void OnLoginSessionAdded(AccountId accountId)
        {
            Debug.Log($"A new Login was added for player {accountId.DisplayName}");
        }

        [LoginEvent(LoginStatus.LoginRemoved)]
        private void OnLoginSessionRemoved(AccountId accountId)
        {
            Debug.Log($"A new Login was removed for player {accountId.DisplayName}");
        }

        [LoginEvent(LoginStatus.LoginValuesUpdated)]
        private void OnLoginSessionValuesUpdated(ILoginSession loginSession)
        {
            Debug.Log($"LoginSession has been updated for player {loginSession.LoginSessionId.DisplayName}");
        }



        [ChannelEvent(ChannelStatus.ChannelConnecting)]
        private void OnChannelConnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Is Connecting");
        }

        [ChannelEvent(ChannelStatus.ChannelConnected)]
        private void OnChannelConnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Has Connected : Channel Type == {channelSession.Channel.Type}");
        }

        [ChannelEvent(ChannelStatus.ChannelDisconnecting)]
        private void OnChannelDisconnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Is Disconnecting");
        }

        [ChannelEvent(ChannelStatus.ChannelDisconnected)]
        private void OnChannelDisconnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Has Disconnected");
        }



        [AudioChannelEvent(AudioChannelStatus.AudioChannelConnecting)]
        private void OnAudioChannelConnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Is Connecting");
        }

        [AudioChannelEvent(AudioChannelStatus.AudioChannelConnected)]
        private void OnAudioChannelConnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Has Connected : Channel Type == {channelSession.Channel.Type}");
        }

        [AudioChannelEvent(AudioChannelStatus.AudioChannelDisconnecting)]
        private void OnAudioChannelDisconnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Is Disconnecting");
        }

        [AudioChannelEvent(AudioChannelStatus.AudioChannelDisconnected)]
        private void OnAudioChannelDisconnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Has Disconnected");
        }



        [TextChannelEvent(TextChannelStatus.TextChannelConnecting)]
        private void OnTextChannelConnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Is Connecting");
        }

        [TextChannelEvent(TextChannelStatus.TextChannelConnected)]
        private void OnTextChannelConnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Has Connected : Channel Type == {channelSession.Channel.Type}");
        }
        [TextChannelEvent(TextChannelStatus.TextChannelDisconnecting)]
        private void OnTextChannelDisconnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Is Disconnecting");
        }

        [TextChannelEvent(TextChannelStatus.TextChannelDisconnected)]
        private void OnTextChannelDisconnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Has Disconnected");
        }




        [ChannelMessageEvent(ChannelMessageStatus.ChannelMessageRecieved)]
        private void OnChannelMessageRecieved(IChannelTextMessage textMessage)
        {
            Debug.Log($"From {textMessage.Sender.DisplayName} : {textMessage.ReceivedTime} : {textMessage.Message}");
        }

        [ChannelMessageEvent(ChannelMessageStatus.EventMessageRecieved)]
        private void OnEventMessageRecieved(IChannelTextMessage textMessage)
        {
            Debug.Log($"Event Message From {textMessage.Sender.DisplayName} : {textMessage.ReceivedTime} : {textMessage.ApplicationStanzaNamespace} : {textMessage.ApplicationStanzaBody} : {textMessage.Message}");
        }

        [DirectMessageEvent(DirectMessageStatus.DirectMessageRecieved)]
        private void OnDirectMessageRecieved(IDirectedTextMessage directedTextMessage)
        {
            Debug.Log($"Recived Message From : {directedTextMessage.Sender.DisplayName} : {directedTextMessage.ReceivedTime} : {directedTextMessage.Message}");
        }

        [DirectMessageEvent(DirectMessageStatus.DirectMessageFailed)]
        private void OnDirectMessageFailed(IFailedDirectedTextMessage failedMessage)
        {
            Debug.Log($"Failed To Send Message From : {failedMessage.Sender}");
        }





        [UserEvents(UserStatus.LocalUserMuted)]
        private void OnLocalUserMuted()
        {
            Debug.Log("Local User is Muted");
        }

        [UserEvents(UserStatus.LocalUserUnmuted)]
        private void OnLocalUserUnmuted()
        {
            Debug.Log("Local User is Unmuted");
        }

        [UserEvents(UserStatus.UserCrossMuted)]
        private void OnCrossMuted(AccountId accountId)
        {
            Debug.Log($"Player {accountId.DisplayName} has been Cross Muted");
        }

        [UserEvents(UserStatus.UserCrossUnmuted)]
        private void OnCrossUnmuted(AccountId accountId)
        {
            Debug.Log($"Player {accountId.DisplayName} has been Cross Unmuted");
        }

        [UserEvents(UserStatus.UserMuted)]
        private void OnUserMuted(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Is Muted : (Muted For All : {participant.IsMutedForAll})");
        }

        [UserEvents(UserStatus.UserUnmuted)]
        private void OnUserUnmuted(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Is Unmuted : (Muted For All : {participant.IsMutedForAll})");
        }

        [UserEvents(UserStatus.UserSpeaking)]
        private void OnUserSpeaking(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Is Speaking : Audio Energy {participant.AudioEnergy}");
        }

        [UserEvents(UserStatus.UserNotSpeaking)]
        private void OnUserNotSpeaking(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Is Not Speaking");
        }



        [UserEvents(UserStatus.UserJoinedChannel)]
        private void OnUserJoinedChannel(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Has Joined The Channel");
        }

        [UserEvents(UserStatus.UserLeftChannel)]
        private void OnUserLeftChannel(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Has Left The Channel");
        }

        [UserEvents(UserStatus.UserValuesUpdated)]
        private void OnUserValuesUpdated(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Has updated itself in the channel");
        }


        [AudioDeviceEvent(AudioDeviceStatus.AudioInputDeviceAdded)]
        private void OnAudioInputDeviceAdded(IAudioDevice audioDevice)
        {
            Debug.Log($"Audio Input device has been added {audioDevice?.Name}");
        }

        [AudioDeviceEvent(AudioDeviceStatus.AudioInputDeviceRemoved)]
        private void OnAudioInputDeviceRemoved(IAudioDevice audioDevice)
        {
            Debug.Log($"Audio Input device has been removed {audioDevice?.Name}");
        }

        [AudioDeviceEvent(AudioDeviceStatus.AudioInputDeviceUpdated)]
        private void OnAudioInputDeviceUpdated(IAudioDevice audioDevice)
        {
            Debug.Log($"Audio Input Device has been changed to {audioDevice?.Name}");
        }

        [AudioDeviceEvent(AudioDeviceStatus.AudioOutputDeviceAdded)]
        private void OnAudioOutputDeviceAdded(IAudioDevice audioDevice)
        {
            Debug.Log($"Audio Output device has been added {audioDevice?.Name}");
        }

        [AudioDeviceEvent(AudioDeviceStatus.AudioOutputDeviceRemoved)]
        private void OnAudioOutputDeviceRemoved(IAudioDevice audioDevice)
        {
            Debug.Log($"Audio Output device has been removed {audioDevice?.Name}");
        }

        [AudioDeviceEvent(AudioDeviceStatus.AudioOutputDeviceUpdated)]
        private void OnAudioOutputDeviceUpdated(IAudioDevice audioDevice)
        {
            Debug.Log($"Audio Output Device has been changed to {audioDevice?.Name}");
        }



        [TextToSpeechEvent(TextToSpeechStatus.TTSMessageAdded)]
        private void OnTTSMessageAdded(ITTSMessageQueueEventArgs ttsArgs)
        {
            Debug.Log($"TTS Message Has Been Added : {ttsArgs.Message.Text}");
        }

        [TextToSpeechEvent(TextToSpeechStatus.TTSMessageRemoved)]
        private void OnTTSMessageRemoved(ITTSMessageQueueEventArgs ttsArgs)
        {
            Debug.Log($"TTS Message Has Been Removed : {ttsArgs.Message.Text}");
        }

        [TextToSpeechEvent(TextToSpeechStatus.TTSMessageUpdated)]
        private void OnTTSMessageUpdated(ITTSMessageQueueEventArgs ttsArgs)
        {
            Debug.Log($"TTS Message Has Been Updated : {ttsArgs.Message.Text}");
        }

    }
}