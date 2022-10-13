using EasyCodeForVivox.Events;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;
using Zenject;

namespace EasyCodeForVivox
{
    public class EasyManager : MonoBehaviour
    {
        private EasyLogin _login;
        private EasyChannel _channel;
        private EasyAudioChannel _voiceChannel;
        private EasyTextChannel _textChannel;
        private EasyUsers _users;
        private EasyMessages _messages;
        private EasyAudio _audioSettings;
        private EasyMute _mute;
        private EasyTextToSpeech _textToSpeech;
        private EasyAudio _audio;


        [Inject]
        public void Initialize(EasyLogin login, EasyChannel channel, EasyAudioChannel voiceChannel, EasyTextChannel textChannel,
            EasyUsers users, EasyMessages messages, EasyAudio audioSettings, EasyMute mute, EasyTextToSpeech textToSpeech, EasyAudio audio)
        {
            _login = login ?? throw new ArgumentNullException(nameof(login));
            _channel = channel ?? throw new ArgumentNullException(nameof(channel));
            _voiceChannel = voiceChannel ?? throw new ArgumentNullException(nameof(voiceChannel));
            _textChannel = textChannel ?? throw new ArgumentNullException(nameof(textChannel));
            _users = users ?? throw new ArgumentNullException(nameof(users));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
            _audioSettings = audioSettings ?? throw new ArgumentNullException(nameof(audioSettings));
            _mute = mute ?? throw new ArgumentNullException(nameof(mute));
            _textToSpeech = textToSpeech ?? throw new ArgumentNullException(nameof(textToSpeech));
            _audio = audio ?? throw new ArgumentNullException(nameof(audio));
        }


        // guarantees to only Initialize client once
        public async Task InitializeClient(VivoxConfig vivoxConfig = default, bool useDynamicEvents = true, bool logAssemblySearches = true, bool logAllDynamicMethods = false)
        {
            // disable Debug.Log Statements in the build for better performance
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
             Debug.unityLogger.logEnabled = false;
#endif

            EasySession.UseDynamicEvents = useDynamicEvents;

            if (EasySession.IsClientInitialized)
            {
                Debug.Log($"{nameof(EasyManager)} : Vivox Client is already initialized, skipping...".Color(EasyDebug.Yellow));
                return;
            }
            else
            {
                if (!EasySession.Client.Initialized)
                {
                    EasySession.Client.Uninitialize();
                    EasySession.Client.Initialize(vivoxConfig);
                    EasySession.IsClientInitialized = true;
                    SubscribeToVivoxEvents();
                    Debug.Log($"Vivox Client Initialized".Color(EasyDebug.Green));
                    if (useDynamicEvents)
                    {
                        // may seem redundant to use Task.Run because multiple Tasks are created in RuntimeEvents.RegisterEvents()
                        // but I tested running RuntimeEvents.RegisterEvents() without using Task.Run and it was way slower
                        // difference was approximately .900 milliseconds vs .090 milliseconds(results will vary based on how many assemblies are in your project)
                        await Task.Run(async () => { await RuntimeEvents.RegisterEvents(logAssemblySearches, logAllDynamicMethods); });
                    }
                }
            }
        }


        public void UnitializeClient()
        {
            UnsubscribeToVivoxEvents();
            EasySession.Client.Uninitialize();
        }


        public void SubscribeToVivoxEvents()
        {
            EasyEvents.LoggingIn += OnLoggingIn;
            EasyEvents.LoggedIn += OnLoggedIn;
            EasyEvents.LoggedIn += OnLoggedInSetup;
            EasyEvents.LoggingOut += OnLoggingOut;
            EasyEvents.LoggedOut += OnLoggedOut;

            EasyEvents.ChannelConnecting += OnChannelConnecting;
            EasyEvents.ChannelConnected += OnChannelConnected;
            EasyEvents.ChannelDisconnecting += OnChannelDisconnecting;
            EasyEvents.ChannelDisconnected += OnChannelDisconnected;

            EasyEvents.AudioChannelConnecting += OnVoiceConnecting;
            EasyEvents.AudioChannelConnected += OnVoiceConnected;
            EasyEvents.AudioChannelDisconnecting += OnVoiceDisconnecting;
            EasyEvents.AudioChannelDisconnected += OnVoiceDisconnected;

            EasyEvents.TextChannelConnecting += OnTextChannelConnecting;
            EasyEvents.TextChannelConnected += OnTextChannelConnected;
            EasyEvents.TextChannelDisconnecting += OnTextChannelDisconnecting;
            EasyEvents.TextChannelDisconnected += OnTextChannelDisconnected;

            EasyEvents.ChannelMessageRecieved += OnChannelMessageRecieved;
            EasyEvents.EventMessageRecieved += OnEventMessageRecieved;

            EasyEvents.DirectMessageRecieved += OnDirectMessageRecieved;
            EasyEvents.DirectMessageFailed += OnDirectMessageFailed;

            EasyEvents.LocalUserMuted += OnLocalUserMuted;
            EasyEvents.LocalUserUnmuted += OnLocalUserUnmuted;

            EasyEvents.UserJoinedChannel += OnUserJoinedChannel;
            EasyEvents.UserLeftChannel += OnUserLeftChannel;
            EasyEvents.UserValuesUpdated += OnUserValuesUpdated;

            EasyEvents.UserMuted += OnUserMuted;
            EasyEvents.UserUnmuted += OnUserUnmuted;
            EasyEvents.UserSpeaking += OnUserSpeaking;
            EasyEvents.UserNotSpeaking += OnUserNotSpeaking;

            EasyEvents.TTSMessageAdded += OnTTSMessageAdded;
            EasyEvents.TTSMessageRemoved += OnTTSMessageRemoved;
            EasyEvents.TTSMessageUpdated += OnTTSMessageUpdated;

        }

        public void UnsubscribeToVivoxEvents()
        {
            EasyEvents.LoggingIn -= OnLoggingIn;
            EasyEvents.LoggedIn -= OnLoggedIn;
            EasyEvents.LoggedIn -= OnLoggedInSetup;
            EasyEvents.LoggingOut -= OnLoggingOut;
            EasyEvents.LoggedOut -= OnLoggedOut;

            EasyEvents.ChannelConnecting -= OnChannelConnecting;
            EasyEvents.ChannelConnected -= OnChannelConnected;
            EasyEvents.ChannelDisconnecting -= OnChannelDisconnecting;
            EasyEvents.ChannelDisconnected -= OnChannelDisconnected;

            EasyEvents.AudioChannelConnecting -= OnVoiceConnecting;
            EasyEvents.AudioChannelConnected -= OnVoiceConnected;
            EasyEvents.AudioChannelDisconnecting -= OnVoiceDisconnecting;
            EasyEvents.AudioChannelDisconnected -= OnVoiceDisconnected;

            EasyEvents.TextChannelConnecting -= OnTextChannelConnecting;
            EasyEvents.TextChannelConnected -= OnTextChannelConnected;
            EasyEvents.TextChannelDisconnecting -= OnTextChannelDisconnecting;
            EasyEvents.TextChannelDisconnected -= OnTextChannelDisconnected;

            EasyEvents.ChannelMessageRecieved -= OnChannelMessageRecieved;
            EasyEvents.EventMessageRecieved -= OnEventMessageRecieved;

            EasyEvents.DirectMessageRecieved -= OnDirectMessageRecieved;
            EasyEvents.DirectMessageFailed -= OnDirectMessageFailed;

            EasyEvents.LocalUserMuted -= OnLocalUserMuted;
            EasyEvents.LocalUserUnmuted -= OnLocalUserUnmuted;

            EasyEvents.UserJoinedChannel -= OnUserJoinedChannel;
            EasyEvents.UserLeftChannel -= OnUserLeftChannel;
            EasyEvents.UserValuesUpdated -= OnUserValuesUpdated;

            EasyEvents.UserMuted -= OnUserMuted;
            EasyEvents.UserUnmuted -= OnUserUnmuted;
            EasyEvents.UserSpeaking -= OnUserSpeaking;
            EasyEvents.UserNotSpeaking -= OnUserNotSpeaking;

            EasyEvents.TTSMessageAdded -= OnTTSMessageAdded;
            EasyEvents.TTSMessageRemoved -= OnTTSMessageRemoved;
            EasyEvents.TTSMessageUpdated -= OnTTSMessageUpdated;

        }



        #region Main Methods





        public void LoginToVivox(string userName, bool joinMuted = false)
        {
            _login.LoginToVivox(userName, joinMuted);
        }


        public void LogoutOfVivox(string userName)
        {
            _login.Logout(userName);
        }

        public void JoinChannel(string userName, string channelName, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel, ChannelType channelType,
            bool joinMuted = false, Channel3DProperties channel3DProperties = default)
        {
            _channel.JoinChannel(userName, channelName, includeVoice, includeText, switchTransmissionToThisChannel, channelType, joinMuted, channel3DProperties);
        }

        public void LeaveChannel(string channelName, string userName)
        {
            _channel.LeaveChannel(channelName, userName);
        }

        public void SetVoiceActiveInChannel(string userName, string channelName, bool connect)
        {
            // todo fix error where channel disconects if both text and voice are disconnected and when you try and toggle 
            // you get an object null reference because channel name exists but channelsession doesnt exist
            IChannelSession channelSession = EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(_channel.GetChannelSIP(channelName)));
            _voiceChannel.ToggleAudioChannelActive(channelSession, connect);
        }

        public void SetTextActiveInChannel(string userName, string channelName, bool connect)
        {
            // todo fix error where channel disconects if both text and voice are disconnected and when you try and toggle 
            // you get an object null reference because channel name exists but channelsession doesnt exist
            IChannelSession channelSession = EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(_channel.GetChannelSIP(channelName)));
            _textChannel.ToggleTextChannelActive(channelSession, connect);
        }

        public void SendChannelMessage(string userName, string channelName, string msg, string title = "", string body = "")
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(body))
            {
                _messages.SendChannelMessage(_channel.GetExistingChannelSession(userName, channelName),
                msg);
            }
            else
            {
                _messages.SendChannelMessage(_channel.GetExistingChannelSession(userName, channelName),
                    msg, title, body);
            }
        }

        public void SendEventMessage(string userName, string channelName, string msg, string title = "", string body = "")
        {
            _messages.SendChannelMessage(_channel.GetExistingChannelSession(userName, channelName),
                msg, title, body);
        }

        public void SendDirectMessage(string userName, string userToMsg, string msg, string title = "", string body = "")
        {
            // todo check if user is blocked and alert front end users
            // not supporting presence so not neccessary - leaving todo in case things change
            _messages.SendDirectMessage(EasySession.LoginSessions[userName], userToMsg, msg, title, body);
        }

        public void MuteSelf()
        {
            _mute.LocalMuteSelf(EasySession.Client);
        }

        public void UnmuteSelf()
        {
            _mute.LocalUnmuteSelf(EasySession.Client);
        }

        public void ToggleMuteRemoteUser(string userName, string channelName)
        {
            _mute.LocalToggleMuteRemoteUser(userName, _channel.GetExistingChannelSession(userName, channelName));
        }

        public void MuteAllPlayers(string channelName)
        {
            if (EasySession.ChannelSessions.ContainsKey(channelName))
            {
                _mute.MuteAllUsers(EasySession.ChannelSessions[channelName]);
            }
            else
            {
                Debug.Log("Channel Does not exist");
            }
        }

        public void UnmuteAllPlayers(string channelName)
        {
            if (EasySession.ChannelSessions.ContainsKey(channelName))
            {
                _mute.UnmuteAllUsers(EasySession.ChannelSessions[channelName]);
            }
            else
            {
                Debug.Log("Channel Does not exist");
            }
        }


        public void AdjustLocalUserVolume(int volume)
        {
            _audioSettings.AdjustLocalPlayerAudioVolume(volume, EasySession.Client);
        }

        public void AdjustRemoteUserVolume(string userName, string channelName, float volume)
        {
            IChannelSession channelSession = _channel.GetExistingChannelSession(userName, channelName);
            _audioSettings.AdjustRemotePlayerAudioVolume(userName, channelSession, volume);
        }

        public void InjectAudio(string username, string audioPath)
        {
            _audio.StartAudioInjection(audioPath, EasySession.LoginSessions[username]);
        }
        
        public void StopInjectedAudio(string username)
        {
            _audio.StopAudioInjection(EasySession.LoginSessions[username]);
        }


        public void SpeakTTS(string msg, string userName)
        {
            _textToSpeech.TTSSpeak(msg, TTSDestination.QueuedLocalPlayback, EasySession.LoginSessions[userName]);
        }

        public void SpeakTTS(string msg, string userName, TTSDestination playMode)
        {
            _textToSpeech.TTSSpeak(msg, playMode, EasySession.LoginSessions[userName]);
        }

        public void ChooseVoiceGender(VoiceGender voiceGender, ILoginSession loginSession)
        {
            switch (voiceGender)
            {
                case VoiceGender.male:
                    _textToSpeech.TTSChooseVoice(_textToSpeech.MaleVoice, loginSession);
                    break;

                case VoiceGender.female:
                    _textToSpeech.TTSChooseVoice(_textToSpeech.FemaleVoice, loginSession);
                    break;
            }
        }


        public void PushToTalk(bool enable, KeyCode keyCode)
        {
            if (enable)
            {
                MuteSelf();
                StartCoroutine(PushToTalk(keyCode));
            }
            else
            {
                StopCoroutine(nameof(PushToTalk));
                UnmuteSelf();
            }
        }

        IEnumerator PushToTalk(KeyCode key)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(key));
            Debug.Log($"{key} is down, Local Player Is Unmuted");
            UnmuteSelf();
            yield return new WaitUntil(() => Input.GetKeyUp(key));
            Debug.Log($"{key} is up, Local Player Muted");
            MuteSelf();
            yield return PushToTalk(key);
        }


        #endregion









        #region Event Callbacks



        #region Login / Logout Callbacks


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



        private void OnLoggedInSetup(ILoginSession loginSession)
        {
            EasyVivoxHelpers.RequestAndroidMicPermission();
            ChooseVoiceGender(VoiceGender.female, loginSession);
        }


        // todo add Extra/Multiple Login callbacks to EasyLogin

        protected virtual void OnLoginAdded(ILoginSession loginSession)
        {
            Debug.Log($"Login Added : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
        }

        protected virtual void OnLoginRemoved(ILoginSession loginSession)
        {
            Debug.Log($"Login Removed : {loginSession.LoginSessionId.DisplayName} : Presence = {loginSession.Presence.Status}");
        }

        protected virtual void OnLoginUpdated(ILoginSession loginSession)
        {
            Debug.Log($"Login Updated : Login Updated : {loginSession.LoginSessionId.DisplayName} : Presence = {loginSession.Presence.Status}");
        }


        #endregion


        #region Audio / Text / Channel Callbacks


        protected virtual void OnChannelConnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Is Connecting");
        }

        protected virtual void OnChannelConnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Has Connected");
            Debug.Log($"Channel Type == {channelSession.Channel.Type}");
        }

        protected virtual void OnChannelDisconnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Is Disconnecting");
        }

        protected virtual void OnChannelDisconnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Has Disconnected");
            _channel.RemoveChannelSession(channelSession.Channel.Name);
        }




        protected virtual void OnVoiceConnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Audio Is Connecting In Channel");
        }

        protected virtual void OnVoiceConnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Audio Has Connected In Channel");
        }

        protected virtual void OnVoiceDisconnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Audio Is Disconnecting In Channel");
        }

        protected virtual void OnVoiceDisconnected(IChannelSession channelSession)
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



        #endregion


        #region Local User Mute Callbacks

        protected virtual void OnLocalUserMuted()
        {
            Debug.Log("Local User is Muted");
        }

        protected virtual void OnLocalUserUnmuted()
        {
            Debug.Log("Local User is Unmuted");
        }

        #endregion

        #region User Callbacks


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

        protected virtual void OnUserMuted(IParticipant participant)
        {
            // todo add option if statement to display debug messages
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


        #endregion


        #region Message Callbacks


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




        #endregion


        #region Text-to-Speech Callbacks

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


        #endregion



        #endregion






    }
}