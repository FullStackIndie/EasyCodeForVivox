using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyManager : MonoBehaviour
    {

        // guarantees to only Initialize client once
        public void InitializeClient()
        {
            // disable Debug.Log Statements in the build for better performance
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
             Debug.unityLogger.logEnabled = false;
#endif

            if (EasySession.isClientInitialized)
            {
                Debug.Log($"{nameof(EasyManager)} : Vivox Client is already initialized, skipping...");
                return;
            }
            else
            {
                if (!EasySession.mainClient.Initialized)
                {
                    EasySession.mainClient.Uninitialize();
                    EasySession.mainClient.Initialize();
                    EasySession.isClientInitialized = true;
                    SubscribeToVivoxEvents();
                    Debug.Log("Vivox Client Initialized");

                    RuntimeEvents.RegisterEvents();
                }
            }
        }


        public void UnitializeClient()
        {
            UnsubscribeToVivoxEvents();
            EasySession.mainClient.Uninitialize();
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

            EasyEvents.VoiceChannelConnecting += OnVoiceConnecting;
            EasyEvents.VoiceChannelConnected += OnVoiceConnected;
            EasyEvents.VoiceChannelDisconnecting += OnVoiceDisconnecting;
            EasyEvents.VoiceChannelDisconnected += OnVoiceDisconnected;

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

            EasyEvents.SubscriptionAddAllowed += OnAddAllowedSubscription;
            EasyEvents.SubscriptionAddBlocked += OnAddBlockedSubscription;
            EasyEvents.SubscriptionAddPresence += OnAddPresenceSubscription;

            EasyEvents.SubscriptionRemoveAllowed += OnRemoveAllowedSubscription;
            EasyEvents.SubscriptionRemoveBlocked += OnRemoveBlockedSubscription;
            EasyEvents.SubscriptionRemovePresence += OnRemovePresenceSubscription;

            EasyEvents.SubscriptionUpdatePresence += OnUpdatePresenceSubscription;
            EasyEvents.SubscriptionIncomingRequest += OnIncomingSubscription;

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

            EasyEvents.VoiceChannelConnecting -= OnVoiceConnecting;
            EasyEvents.VoiceChannelConnected -= OnVoiceConnected;
            EasyEvents.VoiceChannelDisconnecting -= OnVoiceDisconnecting;
            EasyEvents.VoiceChannelDisconnected -= OnVoiceDisconnected;

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

            EasyEvents.SubscriptionAddAllowed -= OnAddAllowedSubscription;
            EasyEvents.SubscriptionAddBlocked -= OnAddBlockedSubscription;
            EasyEvents.SubscriptionAddPresence -= OnAddPresenceSubscription;

            EasyEvents.SubscriptionRemoveAllowed -= OnRemoveAllowedSubscription;
            EasyEvents.SubscriptionRemoveBlocked -= OnRemoveBlockedSubscription;
            EasyEvents.SubscriptionRemovePresence -= OnRemovePresenceSubscription;

            EasyEvents.SubscriptionUpdatePresence -= OnUpdatePresenceSubscription;
            EasyEvents.SubscriptionIncomingRequest -= OnIncomingSubscription;

        }


        #region Vivox Backend Functionality Classes, Enums


        public enum VoiceGender { male, female }

        private readonly EasyLogin _login = new EasyLogin();
        private readonly EasyChannel _channel = new EasyChannel();
        private readonly EasyVoiceChannel _voiceChannel = new EasyVoiceChannel();
        private readonly EasyTextChannel _textChannel = new EasyTextChannel();
        private readonly EasyUsers _users = new EasyUsers();
        private readonly EasyMessages _messages = new EasyMessages();
        private readonly EasyAudioSettings _audioSettings = new EasyAudioSettings();
        private readonly EasyMute _mute = new EasyMute();
        private readonly EasySubscriptions _subscriptions = new EasySubscriptions();
        private readonly EasyTextToSpeech _textToSpeech = new EasyTextToSpeech();


        #endregion



        #region Main Methods





        public void LoginToVivox(string userName, bool joinMuted = false)
        {
            try
            {
                if (!FilterChannelAndUserName(userName)) { return; }

                EasySession.mainLoginSession = EasySession.mainClient.GetLoginSession(new AccountId(EasySession.Issuer, userName, EasySession.Domain));
                _messages.SubscribeToDirectMessages(EasySession.mainLoginSession);
                _textToSpeech.Subscribe(EasySession.mainLoginSession);
                _subscriptions.Subscribe(EasySession.mainLoginSession);

                _login.LoginToVivox(EasySession.mainLoginSession, EasySession.APIEndpoint, userName, joinMuted);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
                _subscriptions.Unsubscribe(EasySession.mainLoginSession);
                _messages.UnsubscribeFromDirectMessages(EasySession.mainLoginSession);
                _textToSpeech.Unsubscribe(EasySession.mainLoginSession);
            }
        }


        public void LogoutOfVivox()
        {
            if (EasySession.mainLoginSession.State == LoginState.LoggedIn)
            {
                _subscriptions.Unsubscribe(EasySession.mainLoginSession);
                _messages.UnsubscribeFromDirectMessages(EasySession.mainLoginSession);
                _textToSpeech.Unsubscribe(EasySession.mainLoginSession);
                _login.Logout(EasySession.mainLoginSession);
            }
            else
            {
                Debug.Log($"Not logged in");
            }
        }


        public void JoinChannel(string channelName, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel, ChannelType channelType,
            bool joinMuted = false, Channel3DProperties channel3DProperties = null)
        {
            if (!FilterChannelAndUserName(channelName)) { return; }
            IChannelSession channelSession = CreateNewChannel(channelName, channelType, channel3DProperties);

            try
            {
                _textChannel.Subscribe(channelSession);
                _voiceChannel.Subscribe(channelSession);
                _users.SubscribeToParticipantEvents(channelSession);
                _messages.SubscribeToChannelMessages(channelSession);

                _channel.JoinChannel(includeVoice, includeText, switchTransmissionToThisChannel, channelSession, joinMuted);
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

        public void LeaveChannel(string channelName)
        {
            if (EasySession.mainChannelSessions.ContainsKey(channelName))
            {
                _users.UnsubscribeFromParticipantEvents(EasySession.mainChannelSessions[channelName]);
                _messages.UnsubscribeFromChannelMessages(EasySession.mainChannelSessions[channelName]);
                _channel.LeaveChannel(EasySession.mainLoginSession, EasySession.mainChannelSessions[channelName]);
            }
        }

        public void SetVoiceActiveInChannel(string channelName, bool connect)
        {
            // todo fix error where channel disconects if both text and voice are disconnected and when you try and toggle 
            // you get an object null refenrce because channel name exists but channelsession doesnt exist
            IChannelSession channelSession = EasySession.mainLoginSession.GetChannelSession(new ChannelId(GetChannelSIP(channelName)));
            _voiceChannel.ToggleAudioChannelActive(channelSession, connect);
        }

        public void SetTextActiveInChannel(string channelName, bool connect)
        {
            // todo fix error where channel disconects if both text and voice are disconnected and when you try and toggle 
            // you get an object null refenrce because channel name exists but channelsession doesnt exist
            IChannelSession channelSession = EasySession.mainLoginSession.GetChannelSession(new ChannelId(GetChannelSIP(channelName)));
            _textChannel.ToggleTextChannelActive(channelSession, connect);
        }

        public void SendChannelMessage(string channelName, string msg, string title = "", string body = "")
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(body))
            {
                _messages.SendChannelMessage(GetExistingChannelSession(channelName),
                msg);
            }
            else
            {
                _messages.SendChannelMessage(GetExistingChannelSession(channelName),
                    msg, title, body);
            }
        }

        public void SendEventMessage(string channelName, string msg, string title = "", string body = "")
        {
            _messages.SendChannelMessage(GetExistingChannelSession(channelName),
                msg, title, body);
        }

        public void SendDirectMessage(string userToMsg, string msg, string title = "", string body = "")
        {
            // todo check if user is blocked and alert front end users
            _messages.SendDirectMessage(EasySession.mainLoginSession, userToMsg, msg, title, body);
        }

        public void MuteSelf()
        {
            _mute.LocalMuteSelf(EasySession.mainClient);
        }

        public void UnmuteSelf()
        {
            _mute.LocalUnmuteSelf(EasySession.mainClient);
        }

        public void ToggleMuteRemoteUser(string userName, string channelName)
        {
            _mute.LocalToggleMuteRemoteUser(userName, GetExistingChannelSession(channelName));
        }

        public void MuteAllPlayers(string channelName)
        {
            if (EasySession.mainChannelSessions.ContainsKey(channelName))
            {
                _mute.MuteAllUsers(EasySession.mainChannelSessions[channelName]);
            }
            else
            {
                Debug.Log("Channel Does not exist");
            }
        }

        public void UnmuteAllPlayers(string channelName)
        {
            if (EasySession.mainChannelSessions.ContainsKey(channelName))
            {
                _mute.UnmuteAllUsers(EasySession.mainChannelSessions[channelName]);
            }
            else
            {
                Debug.Log("Channel Does not exist");
            }
        }


        public void AdjustLocalUserVolume(int volume)
        {
            _audioSettings.AdjustLocalPlayerAudioVolume(volume, EasySession.mainClient);
        }

        public void AdjustRemoteUserVolume(string userName, string channelName, float volume)
        {
            IChannelSession channelSession = GetExistingChannelSession(channelName);
            _audioSettings.AdjustRemotePlayerAudioVolume(userName, channelSession, volume);
        }

        public void AddFriend(string userName)
        {
            _subscriptions.AddAllowPresence(EasySIP.GetUserSIP(EasySession.Issuer, userName, EasySession.Domain), EasySession.mainLoginSession);
        }

        public void RemoveFriend(string userName)
        {
            _subscriptions.RemoveAllowedPresence(EasySIP.GetUserSIP(EasySession.Issuer, userName, EasySession.Domain), EasySession.mainLoginSession);
        }

        public void AddAllowedUser(string userName)
        {
            _subscriptions.AddAllowedSubscription(EasySIP.GetUserSIP(EasySession.Issuer, userName, EasySession.Domain), EasySession.mainLoginSession);
        }

        public void RemoveAllowedUser(string userName)
        {
            _subscriptions.RemoveAllowedSubscription(EasySIP.GetUserSIP(EasySession.Issuer, userName, EasySession.Domain), EasySession.mainLoginSession);
        }

        public void BlockUser(string userName)
        {
            _subscriptions.AddBlockedSubscription(EasySIP.GetUserSIP(EasySession.Issuer, userName, EasySession.Domain), EasySession.mainLoginSession);
        }

        public void RemoveBlockedUser(string userName)
        {
            _subscriptions.RemoveBlockedSubscription(EasySIP.GetUserSIP(EasySession.Issuer, userName, EasySession.Domain), EasySession.mainLoginSession);
        }

        public void SpeakTTS(string msg)
        {
            _textToSpeech.TTSSpeak(msg, TTSDestination.QueuedLocalPlayback, EasySession.mainLoginSession);
        }

        public void SpeakTTS(string msg, TTSDestination playMode)
        {
            _textToSpeech.TTSSpeak(msg, playMode, EasySession.mainLoginSession);
        }

        public void ChooseVoiceGender(VoiceGender voiceGender)
        {
            switch (voiceGender)
            {
                case VoiceGender.male:
                    _textToSpeech.TTSChooseVoice(_textToSpeech.MaleVoice, EasySession.mainLoginSession);
                    break;

                case VoiceGender.female:
                    _textToSpeech.TTSChooseVoice(_textToSpeech.FemaleVoice, EasySession.mainLoginSession);
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

        protected IChannelSession GetExistingChannelSession(string channelName)
        {
            if (EasySession.mainChannelSessions[channelName].ChannelState == ConnectionState.Disconnected || EasySession.mainChannelSessions[channelName] == null)
            {
                EasySession.mainChannelSessions[channelName] = EasySession.mainLoginSession.GetChannelSession(new ChannelId(GetChannelSIP(channelName)));
            }

            return EasySession.mainChannelSessions[channelName];
        }

        protected IChannelSession CreateNewChannel(string channelName, ChannelType channelType, Channel3DProperties channel3DProperties = null)
        {
            if (channelType == ChannelType.Positional)
            {
                foreach (KeyValuePair<string, IChannelSession> channel in EasySession.mainChannelSessions)
                {
                    if (channel.Value.Channel.Type == ChannelType.Positional)
                    {
                        Debug.Log($"{channel.Value.Channel.Name} Is already a 3D Positional Channel. Can Only Have One 3D Positional Channel. Refer To Vivox Documentation :: Returning Exisiting 3D Channel : {channel.Value.Channel.Name}");
                        return channel.Value;
                    }
                }

                EasySession.mainChannelSessions.Add(channelName, EasySession.mainLoginSession.GetChannelSession(new ChannelId(GetChannelSIP(channelType, channelName, channel3DProperties))));
            }
            else
            {
                EasySession.mainChannelSessions.Add(channelName, EasySession.mainLoginSession.GetChannelSession(new ChannelId(GetChannelSIP(channelType, channelName))));
            }

            return EasySession.mainChannelSessions[channelName];
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
            foreach (var session in EasySession.mainChannelSessions)
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
            if (EasySession.mainChannelSessions.ContainsKey(channelName))
            {
                EasySession.mainChannelSessions.Remove(channelName);
            }
        }







        #region Event Callbacks



        #region Login / Logout Callbacks


        public virtual void OnLoggingIn(ILoginSession loginSession)
        {
            Debug.Log($"Logging In : {loginSession.LoginSessionId.DisplayName}");
        }

        public virtual void OnLoggedIn(ILoginSession loginSession)
        {
            Debug.Log($"Logged in : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
        }

        public virtual void OnLoggingOut(ILoginSession loginSession)
        {
            Debug.Log($"Logging out : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
        }

        public virtual void OnLoggedOut(ILoginSession loginSession)
        {
            Debug.Log($"Logged out : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
        }



        private void OnLoggedInSetup(ILoginSession loginSession)
        {
            RequestAndroidMicPermission();
            ChooseVoiceGender(VoiceGender.female);
        }


        // todo add Extra/Multiple Login callbacks to EasyLogin

        public virtual void OnLoginAdded(ILoginSession loginSession)
        {
            Debug.Log($"Login Added : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
        }

        public virtual void OnLoginRemoved(ILoginSession loginSession)
        {
            Debug.Log($"Login Removed : {loginSession.LoginSessionId.DisplayName} : Presence = {loginSession.Presence.Status}");
        }

        public virtual void OnLoginUpdated(ILoginSession loginSession)
        {
            Debug.Log($"Login Updated : Login Updated : {loginSession.LoginSessionId.DisplayName} : Presence = {loginSession.Presence.Status}");
        }


        #endregion


        #region Audio / Text / Channel Callbacks


        public virtual void OnChannelConnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Is Connecting");
        }

        public virtual void OnChannelConnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Has Connected");
            Debug.Log($"Channel Type == {channelSession.Channel.Type}");
        }

        public virtual void OnChannelDisconnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Is Disconnecting");
        }

        public virtual void OnChannelDisconnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Has Disconnected");
            RemoveChannelSession(channelSession.Channel.Name);
        }




        public virtual void OnVoiceConnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Audio Is Connecting In Channel");
        }

        public virtual void OnVoiceConnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Audio Has Connected In Channel");
        }

        public virtual void OnVoiceDisconnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Audio Is Disconnecting In Channel");
        }

        public virtual void OnVoiceDisconnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Audio Has Disconnected In Channel");
        }




        public virtual void OnTextChannelConnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Text Is Connecting In Channel");
        }

        public virtual void OnTextChannelConnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Text Has Connected In Channel");
        }

        public virtual void OnTextChannelDisconnecting(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Text Is Disconnecting In Channel");
        }

        public virtual void OnTextChannelDisconnected(IChannelSession channelSession)
        {
            Debug.Log($"{channelSession.Channel.Name} Text Has Disconnected In Channel");
        }



        #endregion


        #region Local User Mute Callbacks

        public virtual void OnLocalUserMuted(bool isMuted)
        {
            Debug.Log("Local User is Muted");
        }

        public virtual void OnLocalUserUnmuted(bool isMuted)
        {
            Debug.Log("Local User is Unmuted");
        }

        #endregion

        #region User Callbacks


        public virtual void OnUserJoinedChannel(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Has Joined The Channel");
        }

        public virtual void OnUserLeftChannel(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Has Left The Channel");

        }

        public virtual void OnUserValuesUpdated(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Has updated itself in the channel");

        }

        public virtual void OnUserMuted(IParticipant participant)
        {
            // todo add option if statement to display debug messages
            Debug.Log($"{participant.Account.DisplayName} Is Muted : (Muted For All : {participant.IsMutedForAll})");

        }

        public virtual void OnUserUnmuted(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Is Unmuted : (Muted For All : {participant.IsMutedForAll})");

        }

        public virtual void OnUserSpeaking(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Is Speaking : Audio Energy {participant.AudioEnergy}");

        }

        public virtual void OnUserNotSpeaking(IParticipant participant)
        {
            Debug.Log($"{participant.Account.DisplayName} Is Not Speaking");
        }


        #endregion


        #region Message Callbacks


        public virtual void OnChannelMessageRecieved(IChannelTextMessage textMessage)
        {
            Debug.Log($"From {textMessage.Sender.DisplayName} : {textMessage.ReceivedTime} : {textMessage.Message}");
        }

        public virtual void OnEventMessageRecieved(IChannelTextMessage textMessage)
        {
            Debug.Log($"Event Message From {textMessage.Sender.DisplayName} : {textMessage.ReceivedTime} : {textMessage.ApplicationStanzaNamespace} : {textMessage.ApplicationStanzaBody} : {textMessage.Message}");
        }

        public virtual void OnDirectMessageRecieved(IDirectedTextMessage directedTextMessage)
        {
            Debug.Log($"Recived Message From : {directedTextMessage.Sender.DisplayName} : {directedTextMessage.ReceivedTime} : {directedTextMessage.Message}");
        }

        public virtual void OnDirectMessageFailed(IFailedDirectedTextMessage failedMessage)
        {
            Debug.Log($"Failed To Send Message From : {failedMessage.Sender}");
        }




        #endregion


        #region Text-to-Speech Callbacks

        public virtual void OnTTSMessageAdded(ITTSMessageQueueEventArgs ttsArgs)
        {
            Debug.Log($"TTS Message Has Been Added : {ttsArgs.Message.Text}");
        }

        public virtual void OnTTSMessageRemoved(ITTSMessageQueueEventArgs ttsArgs)
        {
            Debug.Log($"TTS Message Has Been Removed : {ttsArgs.Message.Text}");
        }

        public virtual void OnTTSMessageUpdated(ITTSMessageQueueEventArgs ttsArgs)
        {
            Debug.Log($"TTS Message Has Been Updated : {ttsArgs.Message.Text}");
        }


        #endregion




        #region Work In Progress


        #region Subscription / Presence Callbacks

        public virtual void OnAddAllowedSubscription(AccountId accountId)
        {
            Debug.Log($"{accountId.DisplayName} User Has Been Allowed Has Been Added");
        }

        public virtual void OnRemoveAllowedSubscription(AccountId accountId)
        {
            Debug.Log($"{accountId.DisplayName} User Has Been Allowed Has Been Removed");
        }

        public virtual void OnAddBlockedSubscription(AccountId accountId)
        {
            Debug.Log($"{accountId.DisplayName} Block On User Has Been Added");
        }

        public virtual void OnRemoveBlockedSubscription(AccountId accountId)
        {
            Debug.Log($"{accountId.DisplayName} Block On User Has Been Removed");
        }

        public virtual void OnAddPresenceSubscription(AccountId accountId)
        {
            Debug.Log($"{accountId.DisplayName} Presence Has Been Added");
        }

        public virtual void OnRemovePresenceSubscription(AccountId accountId)
        {
            Debug.Log($"{accountId.DisplayName} Presence Has Been Removed");
        }

        public virtual void OnUpdatePresenceSubscription(ValueEventArg<AccountId, IPresenceSubscription> presence)
        {
            Debug.Log($"{presence.Value.Key.DisplayName} Presence Has Been Updated");
        }

        public virtual void OnIncomingSubscription(AccountId request)
        {
            Debug.Log($"Incoming subscription request from - {request.Name}");
            Debug.Log($"Incoming subscription request from - {request.DisplayName}");
        }

        #endregion



        #endregion




        #endregion






    }
}