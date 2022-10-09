using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyManager : MonoBehaviour
    {

        // guarantees to only Initialize client once
        public async Task InitializeClient(VivoxConfig vivoxConfig = default, bool useDynamicEvents = true)
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
                Debug.Log($"{nameof(EasyManager)} : Vivox Client is already initialized, skipping...");
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
                    Debug.Log("Vivox Client Initialized");
                    if (useDynamicEvents)
                    {
                        await Task.Run(async () => { await RuntimeEvents.RegisterEvents(); });
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


        #region Vivox Backend Functionality Classes, Enums


        public enum VoiceGender { male, female }

        private readonly EasyLogin _login = new EasyLogin();
        private readonly EasyChannel _channel = new EasyChannel();
        private readonly EasyAudioChannel _voiceChannel = new EasyAudioChannel();
        private readonly EasyTextChannel _textChannel = new EasyTextChannel();
        private readonly EasyUsers _users = new EasyUsers();
        private readonly EasyMessages _messages = new EasyMessages();
        private readonly EasyAudioSettings _audioSettings = new EasyAudioSettings();
        private readonly EasyMute _mute = new EasyMute();
        private readonly EasyTextToSpeech _textToSpeech = new EasyTextToSpeech();


        #endregion



        #region Main Methods





        public void LoginToVivox(string userName, bool joinMuted = false)
        {
            try
            {
                if (!FilterChannelAndUserName(userName)) { return; }

                EasySession.LoginSessions.Add(userName, EasySession.Client.GetLoginSession(new AccountId(EasySession.Issuer, userName, EasySession.Domain)));
                _messages.SubscribeToDirectMessages(EasySession.LoginSessions[userName]);
                _textToSpeech.Subscribe(EasySession.LoginSessions[userName]);

                _login.LoginToVivox(EasySession.LoginSessions[userName], EasySession.APIEndpoint, userName, joinMuted);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
                _messages.UnsubscribeFromDirectMessages(EasySession.LoginSessions[userName]);
                _textToSpeech.Unsubscribe(EasySession.LoginSessions[userName]);
            }
        }


        public void LogoutOfVivox(string userName)
        {
            if (EasySession.LoginSessions[userName].State == LoginState.LoggedIn)
            {
                _messages.UnsubscribeFromDirectMessages(EasySession.LoginSessions[userName]);
                _textToSpeech.Unsubscribe(EasySession.LoginSessions[userName]);
                _login.Logout(EasySession.LoginSessions[userName]);
            }
            else
            {
                Debug.Log($"Not logged in");
            }
        }

        public void JoinChannel(string userName, string channelName, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel, ChannelType channelType,
            bool joinMuted = false, Channel3DProperties channel3DProperties = null)
        {
            if (!FilterChannelAndUserName(channelName)) { return; }
            IChannelSession channelSession = CreateNewChannel(userName, channelName, channelType, channel3DProperties);

            try
            {
                _textChannel.Subscribe(channelSession);
                _voiceChannel.Subscribe(channelSession);
                _users.SubscribeToParticipantEvents(channelSession);
                _messages.SubscribeToChannelMessages(channelSession);

                _channel.JoinChannel(userName, includeVoice, includeText, switchTransmissionToThisChannel, channelSession, joinMuted);
            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
                _textChannel.Unsubscribe(channelSession);
                _voiceChannel.Unsubscribe(channelSession);
                _users.UnsubscribeFromParticipantEvents(channelSession);
                _messages.UnsubscribeFromChannelMessages(channelSession);
            }
        }

        public void LeaveChannel(string channelName, string userName)
        {
            if (EasySession.ChannelSessions.ContainsKey(channelName))
            {
                _users.UnsubscribeFromParticipantEvents(EasySession.ChannelSessions[channelName]);
                _messages.UnsubscribeFromChannelMessages(EasySession.ChannelSessions[channelName]);
                _channel.LeaveChannel(EasySession.LoginSessions[userName], EasySession.ChannelSessions[channelName]);
            }
        }

        public void SetVoiceActiveInChannel( string userName, string channelName, bool connect)
        {
            // todo fix error where channel disconects if both text and voice are disconnected and when you try and toggle 
            // you get an object null refenrce because channel name exists but channelsession doesnt exist
            IChannelSession channelSession = EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelName)));
            _voiceChannel.ToggleAudioChannelActive(channelSession, connect);
        }

        public void SetTextActiveInChannel(string userName, string channelName, bool connect)
        {
            // todo fix error where channel disconects if both text and voice are disconnected and when you try and toggle 
            // you get an object null refenrce because channel name exists but channelsession doesnt exist
            IChannelSession channelSession = EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelName)));
            _textChannel.ToggleTextChannelActive(channelSession, connect);
        }

        public void SendChannelMessage(string userName, string channelName, string msg, string title = "", string body = "")
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(body))
            {
                _messages.SendChannelMessage(GetExistingChannelSession(userName, channelName),
                msg);
            }
            else
            {
                _messages.SendChannelMessage(GetExistingChannelSession(userName, channelName),
                    msg, title, body);
            }
        }

        public void SendEventMessage(string userName, string channelName, string msg, string title = "", string body = "")
        {
            _messages.SendChannelMessage(GetExistingChannelSession(userName, channelName),
                msg, title, body);
        }

        public void SendDirectMessage(string userName, string userToMsg, string msg, string title = "", string body = "")
        {
            // todo check if user is blocked and alert front end users
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
            _mute.LocalToggleMuteRemoteUser(userName, GetExistingChannelSession(userName, channelName));
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
            IChannelSession channelSession = GetExistingChannelSession(userName, channelName);
            _audioSettings.AdjustRemotePlayerAudioVolume(userName, channelSession, volume);
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




        public void RequestAndroidMicPermission()
        {
#if PLATFORM_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }
#endif
        }


        //public void RequestIOSMicrophoneAccess()
        //{
        // todo update and research docs / have someone without IOS test it
        //    // Refer to Vivox Documentation on how to implement this method. Currently a work in progress.NOT SURE IF IT WORKS
        //    // make sure you change the info.plist refer to Vivox documentation for this to work
        //    // Make sure NSCameraUsageDescription and NSMicrophoneUsageDescription
        //    // are in the Info.plist.
        //    Application.RequestUserAuthorization(UserAuthorization.Microphone);
        //}

        protected bool FilterChannelAndUserName(string nameToFilter)
        {
            char[] allowedChars = new char[] { '0','1','2','3', '4', '5', '6', '7', '8', '9',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n','o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I','J', 'K', 'L', 'M', 'N', 'O', 'P','Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        '!', '(', ')', '+','-', '.', '=', '_', '~'};

            List<char> allowed = new List<char>(allowedChars);
            foreach (char c in nameToFilter)
            {
                if (!allowed.Contains(c))
                {
                    if (c == ' ')
                    {
                        Debug.Log($"Can't join channel, Channel name has space in it '{c}'");
                    }
                    else
                    {
                        Debug.Log($"Can't join channel, Channel name has invalid character '{c}'");
                    }
                    return false;
                }
            }
            return true;
        }

        protected IChannelSession GetExistingChannelSession(string userName, string channelName)
        {
            if (EasySession.ChannelSessions[channelName].ChannelState == ConnectionState.Disconnected || EasySession.ChannelSessions[channelName] == null)
            {
                EasySession.ChannelSessions[channelName] = EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelName)));
            }

            return EasySession.ChannelSessions[channelName];
        }

        protected IChannelSession CreateNewChannel(string userName, string channelName, ChannelType channelType, Channel3DProperties channel3DProperties = null)
        {
            if (channelType == ChannelType.Positional)
            {
                foreach (KeyValuePair<string, IChannelSession> channel in EasySession.ChannelSessions)
                {
                    if (channel.Value.Channel.Type == ChannelType.Positional)
                    {
                        Debug.Log($"{channel.Value.Channel.Name} Is already a 3D Positional Channel. Can Only Have One 3D Positional Channel. Refer To Vivox Documentation :: Returning Exisiting 3D Channel : {channel.Value.Channel.Name}");
                        return channel.Value;
                    }
                }

                EasySession.ChannelSessions.Add(userName, EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelType, channelName, channel3DProperties))));
            }
            else
            {
                if (EasySession.ChannelSessions.ContainsKey(userName)) { return EasySession.ChannelSessions[userName]; }
                EasySession.ChannelSessions.Add(userName, EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelType, channelName))));
            }

            return EasySession.ChannelSessions[userName];
        }

        protected string GetChannelSIP(ChannelType channelType, string channelName, Channel3DProperties channel3DProperties = null)
        {
            switch (channelType)
            {
                case ChannelType.NonPositional:
                    return EasySIP.GetChannelSIP(ChannelType.NonPositional, EasySession.Issuer, channelName, EasySession.Domain);

                case ChannelType.Echo:
                    return EasySIP.GetChannelSIP(ChannelType.NonPositional, EasySession.Issuer, channelName, EasySession.Domain);

                case ChannelType.Positional:
                    return EasySIP.GetChannelSIP(ChannelType.Positional, EasySession.Issuer, channelName, EasySession.Domain, channel3DProperties);

            }
            return EasySIP.GetChannelSIP(ChannelType.NonPositional, EasySession.Issuer, channelName, EasySession.Domain);
        }

        protected string GetChannelSIP(string channelName)
        {
            string result = "";
            foreach (var session in EasySession.ChannelSessions)
            {
                if (session.Value.Channel.Name == channelName)
                {
                    result = GetChannelSIP(session.Value.Channel.Type, channelName);
                    return result;
                }
            }
            return result;
        }

        public void RemoveChannelSession(string channelName)
        {
            if (EasySession.ChannelSessions.ContainsKey(channelName))
            {
                EasySession.ChannelSessions.Remove(channelName);
            }
        }







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
            RequestAndroidMicPermission();
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
            RemoveChannelSession(channelSession.Channel.Name);
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

        protected virtual void OnLocalUserMuted(bool isMuted)
        {
            Debug.Log("Local User is Muted");
        }

        protected virtual void OnLocalUserUnmuted(bool isMuted)
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