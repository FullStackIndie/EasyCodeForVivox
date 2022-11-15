using EasyCodeForVivox.Events;
using EasyCodeForVivox.Extensions;
using EasyCodeForVivox.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;
using Zenject;

namespace EasyCodeForVivox
{
    [RequireComponent(typeof(SceneContext))]
    public class EasyManager : MonoBehaviour
    {
        [SerializeField] private bool dontDestroyOnLoad = true;
        private EasyLogin _login;
        private EasyChannel _channel;
        private EasyAudioChannel _voiceChannel;
        private EasyTextChannel _textChannel;
        private EasyUsers _users;
        private EasyMessages _messages;
        private EasyMute _mute;
        private EasyTextToSpeech _textToSpeech;
        private EasyAudio _audio;
        private EasySettingsSO _settings;
        private EasyEvents _events;


        [Inject]
        public void Initialize(EasyLogin login, EasyChannel channel, EasyAudioChannel voiceChannel, EasyTextChannel textChannel,
            EasyUsers users, EasyMessages messages, EasyMute mute, EasyTextToSpeech textToSpeech, EasyAudio audio,
            EasySettingsSO settings, EasyEvents events)
        {
            _login = login ?? throw new ArgumentNullException(nameof(login));
            _channel = channel ?? throw new ArgumentNullException(nameof(channel));
            _voiceChannel = voiceChannel ?? throw new ArgumentNullException(nameof(voiceChannel));
            _textChannel = textChannel ?? throw new ArgumentNullException(nameof(textChannel));
            _users = users ?? throw new ArgumentNullException(nameof(users));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
            _mute = mute ?? throw new ArgumentNullException(nameof(mute));
            _textToSpeech = textToSpeech ?? throw new ArgumentNullException(nameof(textToSpeech));
            _audio = audio ?? throw new ArgumentNullException(nameof(audio));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _events = events ?? throw new ArgumentNullException(nameof(events));
        }


        // guarantees to only Initialize client once
        public async Task InitializeClient(VivoxConfig vivoxConfig = default)
        {
            // disable Debug.Log Statements in the build for better performance
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
             Debug.unityLogger.logEnabled = false;
#endif
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }
            if (EasySession.Client.Initialized)
            {
                if (_settings.LogEasyManager)
                    Debug.Log($"{nameof(EasyManager)} : Vivox Client is already initialized, skipping...".Color(EasyDebug.Yellow));
                return;
            }
            else
            {
                if (!EasySession.Client.Initialized)
                {
                    EasySession.Client.Uninitialize();
                    EasySession.Client.Initialize(vivoxConfig);
                    _audio.Subscribe(EasySession.Client);
                    SubscribeToVivoxEvents();
                    if (_settings.LogEasyManager)
                        Debug.Log($"Vivox Client Initialized".Color(EasyDebug.Green));
                    if (_settings.UseDynamicEvents)
                    {
                        // may seem redundant to use Task.Run because multiple Tasks are created in DynamicEvents.RegisterEvents()
                        // but I tested running DynamicEvents.RegisterEvents() without using Task.Run and it was way slower
                        // difference was approximately .900 milliseconds vs .090 milliseconds(results will vary based on how many assemblies are in your project
                        // and how many cpu's you have)
                        await Task.Run(async () =>
                        {
                            await HandleDynamicEvents.RegisterEvents(_settings.OnlySearchAssemblyCSharp, _settings.LogAssemblySearches, _settings.LogAllDynamicMethods);
                            if (HandleDynamicEvents.Methods.Count == 0)
                            {
                                _settings.UseDynamicEvents = false;
                            }
                        });
                    }
                }
            }
        }


        public void UnitializeClient()
        {
            _audio.Unsubscribe(EasySession.Client);
            UnsubscribeToVivoxEvents();
            EasySession.Client.Uninitialize();
        }


        public void SubscribeToVivoxEvents()
        {
            EasySession.Client.AudioInputDevices.AvailableDevices.AfterKeyAdded += OnAudioInputDeviceAdded;
            EasySession.Client.AudioInputDevices.AvailableDevices.BeforeKeyRemoved += OnAudioInputDeviceRemoved;
            EasySession.Client.AudioInputDevices.AvailableDevices.AfterValueUpdated += OnAudioInputDeviceUpdated;

            EasySession.Client.AudioOutputDevices.AvailableDevices.AfterKeyAdded += OnAudioOutputDeviceAdded;
            EasySession.Client.AudioOutputDevices.AvailableDevices.BeforeKeyRemoved += OnAudioOutputDeviceRemoved;
            EasySession.Client.AudioOutputDevices.AvailableDevices.AfterValueUpdated += OnAudioOutputDeviceUpdated;

            _events.LoggingIn += OnLoggingIn;
            _events.LoggedIn += OnLoggedIn;
            _events.LoggedIn += OnLoggedInSetup;
            _events.LoggingOut += OnLoggingOut;
            _events.LoggedOut += OnLoggedOut;

            _events.LoginAdded += OnLoginAdded;
            _events.LoginRemoved += OnLoginRemoved;
            _events.LoginUpdated += OnLoginUpdated;

            _events.ChannelConnecting += OnChannelConnecting;
            _events.ChannelConnected += OnChannelConnected;
            _events.ChannelDisconnecting += OnChannelDisconnecting;
            _events.ChannelDisconnected += OnChannelDisconnected;

            _events.AudioChannelConnecting += OnAudioChannelConnecting;
            _events.AudioChannelConnected += OnAudioChannelConnected;
            _events.AudioChannelDisconnecting += OnAudioChannelDisconnecting;
            _events.AudioChannelDisconnected += OnAudioChannelDisconnected;

            _events.TextChannelConnecting += OnTextChannelConnecting;
            _events.TextChannelConnected += OnTextChannelConnected;
            _events.TextChannelDisconnecting += OnTextChannelDisconnecting;
            _events.TextChannelDisconnected += OnTextChannelDisconnected;

            _events.ChannelMessageRecieved += OnChannelMessageRecieved;
            _events.EventMessageRecieved += OnEventMessageRecieved;

            _events.DirectMessageRecieved += OnDirectMessageRecieved;
            _events.DirectMessageFailed += OnDirectMessageFailed;

            _events.UserMuted += OnUserMuted;
            _events.UserUnmuted += OnUserUnmuted;
            _events.UserSpeaking += OnUserSpeaking;
            _events.UserNotSpeaking += OnUserNotSpeaking;
            _events.LocalUserMuted += OnLocalUserMuted;
            _events.LocalUserUnmuted += OnLocalUserUnmuted;
            _events.UserCrossMuted += OnCrossMuted;
            _events.UserCrossUnmuted += OnCrossUnmuted;

            _events.UserJoinedChannel += OnUserJoinedChannel;
            _events.UserLeftChannel += OnUserLeftChannel;
            _events.UserValuesUpdated += OnUserValuesUpdated;

            _events.TTSMessageAdded += OnTTSMessageAdded;
            _events.TTSMessageRemoved += OnTTSMessageRemoved;
            _events.TTSMessageUpdated += OnTTSMessageUpdated;

        }

        public void UnsubscribeToVivoxEvents()
        {
            EasySession.Client.AudioInputDevices.AvailableDevices.AfterKeyAdded -= OnAudioInputDeviceAdded;
            EasySession.Client.AudioInputDevices.AvailableDevices.BeforeKeyRemoved -= OnAudioInputDeviceRemoved;
            EasySession.Client.AudioInputDevices.AvailableDevices.AfterValueUpdated -= OnAudioInputDeviceUpdated;

            EasySession.Client.AudioOutputDevices.AvailableDevices.AfterKeyAdded -= OnAudioOutputDeviceAdded;
            EasySession.Client.AudioOutputDevices.AvailableDevices.BeforeKeyRemoved -= OnAudioOutputDeviceRemoved;
            EasySession.Client.AudioOutputDevices.AvailableDevices.AfterValueUpdated -= OnAudioOutputDeviceUpdated;

            _events.LoggingIn -= OnLoggingIn;
            _events.LoggedIn -= OnLoggedIn;
            _events.LoggedIn -= OnLoggedInSetup;
            _events.LoggingOut -= OnLoggingOut;
            _events.LoggedOut -= OnLoggedOut;

            _events.LoginAdded -= OnLoginAdded;
            _events.LoginRemoved -= OnLoginRemoved;
            _events.LoginUpdated -= OnLoginUpdated;

            _events.ChannelConnecting -= OnChannelConnecting;
            _events.ChannelConnected -= OnChannelConnected;
            _events.ChannelDisconnecting -= OnChannelDisconnecting;
            _events.ChannelDisconnected -= OnChannelDisconnected;

            _events.AudioChannelConnecting -= OnAudioChannelConnecting;
            _events.AudioChannelConnected -= OnAudioChannelConnected;
            _events.AudioChannelDisconnecting -= OnAudioChannelDisconnecting;
            _events.AudioChannelDisconnected -= OnAudioChannelDisconnected;

            _events.TextChannelConnecting -= OnTextChannelConnecting;
            _events.TextChannelConnected -= OnTextChannelConnected;
            _events.TextChannelDisconnecting -= OnTextChannelDisconnecting;
            _events.TextChannelDisconnected -= OnTextChannelDisconnected;

            _events.ChannelMessageRecieved -= OnChannelMessageRecieved;
            _events.EventMessageRecieved -= OnEventMessageRecieved;

            _events.DirectMessageRecieved -= OnDirectMessageRecieved;
            _events.DirectMessageFailed -= OnDirectMessageFailed;

            _events.UserMuted -= OnUserMuted;
            _events.UserUnmuted -= OnUserUnmuted;
            _events.UserSpeaking -= OnUserSpeaking;
            _events.UserNotSpeaking -= OnUserNotSpeaking;
            _events.LocalUserMuted -= OnLocalUserMuted;
            _events.LocalUserUnmuted -= OnLocalUserUnmuted;
            _events.UserCrossMuted -= OnCrossMuted;
            _events.UserCrossUnmuted -= OnCrossUnmuted;

            _events.UserJoinedChannel -= OnUserJoinedChannel;
            _events.UserLeftChannel -= OnUserLeftChannel;
            _events.UserValuesUpdated -= OnUserValuesUpdated;

            _events.TTSMessageAdded -= OnTTSMessageAdded;
            _events.TTSMessageRemoved -= OnTTSMessageRemoved;
            _events.TTSMessageUpdated -= OnTTSMessageUpdated;

        }




        #region Main Methods



        public void LoginToVivox(string userName, bool joinMuted = false)
        {
            _login.LoginToVivox(userName, joinMuted);
        }

        public void UpdateLoginProperties(string userName, ParticipantPropertyUpdateFrequency updateFrequency)
        {
            _login.UpdateLoginProperties(userName, updateFrequency);
        }

        public void LogoutOfVivox(string userName)
        {
            _login.LogoutOfVivox(userName);
        }

        public void JoinChannel(string userName, string channelName, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel, ChannelType channelType,
            bool joinMuted = false, Channel3DProperties channel3DProperties = default)
        {
            _channel.JoinChannel(userName, channelName, includeVoice, includeText, switchTransmissionToThisChannel, channelType, joinMuted, channel3DProperties);
        }

        public void JoinChannelRegion(string userName, string channelName, string matchRegion, string matchHash, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel, ChannelType channelType,
            bool joinMuted = false, Channel3DProperties channel3DProperties = default)
        {
            _channel.JoinChannelRegion(userName, channelName, matchRegion, matchHash, includeVoice, includeText, switchTransmissionToThisChannel, channelType, joinMuted, channel3DProperties);
        }


        public void LeaveChannel(string channelName, string userName)
        {
            _channel.LeaveChannel(channelName, userName);
        }

        public void ToggleAudioInChannel(string channelName, bool connect)
        {
            // todo fix error where channel disconects if both text and voice are disconnected and when you try and toggle 
            // you get an object null reference because channel name exists but channelsession doesnt exist
            IChannelSession channelSession = EasySession.ChannelSessions[channelName];
            _voiceChannel.ToggleAudioInChannel(channelSession, connect);
        }

        public void ToggleTextInChannel(string channelName, bool connect)
        {
            // todo fix error where channel disconects if both text and voice are disconnected and when you try and toggle 
            // you get an object null reference because channel name exists but channelsession doesnt exist
            IChannelSession channelSession = EasySession.ChannelSessions[channelName];
            _textChannel.ToggleTextInChannel(channelSession, connect);
        }

        public void SendChannelMessage(string userName, string channelName, string msg, string header = "", string body = "")
        {
            if (string.IsNullOrEmpty(header) || string.IsNullOrEmpty(body))
            {
                _messages.SendChannelMessage(_channel.GetExistingChannelSession(userName, channelName),
                msg);
            }
            else
            {
                _messages.SendChannelMessage(_channel.GetExistingChannelSession(userName, channelName),
                    msg, header, body);
            }
        }

        public void SendEventMessage(string userName, string channelName, string msg, string header = "", string body = "")
        {
            _messages.SendEventMessage(_channel.GetExistingChannelSession(userName, channelName),
                msg, header, body);
        }

        public void SendDirectMessage(string userName, string userToMsg, string msg, string header = "", string body = "")
        {
            // todo check if user is blocked and alert front end users
            // not supporting presence so not neccessary - leaving todo in case things change
            _messages.SendDirectMessage(EasySession.LoginSessions[userName], userToMsg, msg, header, body);
        }

        public void MuteSelf()
        {
            _mute.LocalMuteSelf(EasySession.Client);
        }

        public void UnmuteSelf()
        {
            _mute.LocalUnmuteSelf(EasySession.Client);
        }

        public void LocalMuteRemoteUser(string userName, string channelName)
        {
            _mute.LocalMuteRemoteUser(userName, _channel.GetExistingChannelSession(userName, channelName), true);
        }
        public void LocalUnmuteRemoteUser(string userName, string channelName)
        {
            _mute.LocalMuteRemoteUser(userName, _channel.GetExistingChannelSession(userName, channelName), false);
        }

        public void LocalMuteAllPlayers(string channelName)
        {
            if (EasySession.ChannelSessions.ContainsKey(channelName))
            {
                _mute.LocalMuteAllUsers(EasySession.ChannelSessions[channelName]);
            }
            else
            {
                if (_settings.LogEasyManager)
                    Debug.Log("Channel Does not exist. Cannot Mute player");
            }
        }

        public void LocalUnmuteAllPlayers(string channelName)
        {
            if (EasySession.ChannelSessions.ContainsKey(channelName))
            {
                _mute.LocalUnmuteAllUsers(EasySession.ChannelSessions[channelName]);
            }
            else
            {
                if (_settings.LogEasyManager)
                    Debug.Log("Channel Does not exist. Cannot Unmute player");
            }
        }

        public IEnumerable<ChannelId> GetTransmittingChannelsForPlayer(string userName)
        {
            return EasySession.LoginSessions[userName].TransmittingChannels;
        }

        public bool IsPlayerTransmittingInChannel(string channelName)
        {
            return EasySession.ChannelSessions[channelName].IsTransmitting;
        }

        public void SetPlayerTransmissionMode(string userName, TransmissionMode transmissionMode, ChannelId channelId = default)
        {
            _login.SetPlayerTransmissionMode(userName, transmissionMode, channelId);
        }

        public ChannelId GetChannelId(string userName, string channelName)
        {
            return _login.GetChannelId(userName, channelName);
        }

        public void AdjustLocalUserVolume(int volume)
        {
            _audio.AdjustLocalPlayerAudioVolume(volume, EasySession.Client);
        }

        public void AdjustRemoteUserVolume(string userName, string channelName, float volume)
        {
            IChannelSession channelSession = _channel.GetExistingChannelSession(userName, channelName);
            _audio.AdjustRemotePlayerAudioVolume(userName, channelSession, volume);
        }

        public void CrossMuteUser(string userName, string channelName, string userToMute, bool mute)
        {
            _mute.CrossMuteUser(userName, channelName, userToMute, mute);
        }

        public void CrossMuteUsers(string userName, string channelName, List<string> usersToMute, bool mute)
        {
            _mute.CrossMuteUsers(userName, channelName, usersToMute, mute);
        }

        public void ClearCrossMutedUsersForLoginSession(string loggedInUserName)
        {
            _mute.ClearAllCurrentCrossMutedAccounts(loggedInUserName);
        }

        public void InjectAudio(string username, string audioPath)
        {
            _audio.StartAudioInjection(audioPath, EasySession.LoginSessions[username]);
        }

        public void StopInjectedAudio(string username)
        {
            _audio.StopAudioInjection(EasySession.LoginSessions[username]);
        }

        public void SetAutoVoiceActivityDetection(string userName)
        {
            _audio.SetAutoVoiceActivityDetection(userName);
        }

        public void SetVoiceActivityDetection(string userName, int hangover, int sensitivity, int noiseFloor)
        {
            _audio.SetVoiceActivityDetection(userName, hangover, sensitivity, noiseFloor);
        }

        public void SetAudioInputDevice(string deviceName)
        {
            if (EasySession.Client.AudioInputDevices.AvailableDevices.Keys.Contains(deviceName))
            {
                if (_settings.LogEasyManager)
                    Debug.Log("Setting New Audio Input Device");
                _audio.SetAudioDeviceInput(deviceName, EasySession.Client);
                return;
            }
            if (_settings.LogEasyManager)
                Debug.Log("Could not find Audio device");
        }

        public void SetAudioOutputDevice(string deviceName)
        {
            if (EasySession.Client.AudioInputDevices.AvailableDevices.Keys.Contains(deviceName))
            {
                if (_settings.LogEasyManager)
                    Debug.Log("Setting New Audio Output Device");
                _audio.SetAudioDeviceInput(deviceName, EasySession.Client);
                return;
            }
            if (_settings.LogEasyManager)
                Debug.Log("Could not find Audio device");
        }

        public void EnablePushToTalk(bool enable, KeyCode keyCode)
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
            if (_settings.LogEasyManager)
                Debug.Log($"{key} is down, Local Player Is Unmuted");
            UnmuteSelf();
            yield return new WaitUntil(() => Input.GetKeyUp(key));
            if (_settings.LogEasyManager)
                Debug.Log($"{key} is up, Local Player Muted");
            MuteSelf();
            yield return PushToTalk(key);
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



        #endregion









        #region Event Callbacks



        #region Login / Logout Callbacks


        protected virtual void OnLoggingIn(ILoginSession loginSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"Logging In : {loginSession.LoginSessionId.DisplayName}");
        }

        protected virtual void OnLoggedIn(ILoginSession loginSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"Logged in : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
        }

        protected virtual void OnLoggingOut(ILoginSession loginSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"Logging out : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
        }

        protected virtual void OnLoggedOut(ILoginSession loginSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"Logged out : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
        }



        private void OnLoggedInSetup(ILoginSession loginSession)
        {
            EasyVivoxUtilities.RequestAndroidMicPermission();
            EasyVivoxUtilities.RequestIOSMicrophoneAccess();
            ChooseVoiceGender(_settings.VoiceGender, loginSession);
        }


        // todo add Extra/Multiple Login callbacks to EasyLogin

        protected virtual void OnLoginAdded(AccountId accountId)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"LoginSession Added : For user {accountId.Name}");
        }

        protected virtual void OnLoginRemoved(AccountId accountId)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"Login Removed : For user {accountId}");
        }

        protected virtual void OnLoginUpdated(ILoginSession loginSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"LoginSession has been Updated : {loginSession.LoginSessionId.DisplayName} : Presence = {loginSession.Presence.Status}");
        }


        #endregion


        #region Audio / Text / Channel Callbacks


        protected virtual void OnChannelConnecting(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Is Connecting");
        }

        protected virtual void OnChannelConnected(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
            {
                Debug.Log($"{channelSession.Channel.Name} Has Connected");
                Debug.Log($"Channel Type == {channelSession.Channel.Type}");
            }
        }

        protected virtual void OnChannelDisconnecting(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Is Disconnecting");
        }

        protected virtual void OnChannelDisconnected(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Has Disconnected");
            _channel.RemoveChannelSession(channelSession.Channel.Name);
        }




        protected virtual void OnAudioChannelConnecting(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Audio Is Connecting In Channel");
        }

        protected virtual void OnAudioChannelConnected(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Audio Has Connected In Channel");
        }

        protected virtual void OnAudioChannelDisconnecting(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Audio Is Disconnecting In Channel");
        }

        protected virtual void OnAudioChannelDisconnected(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Audio Has Disconnected In Channel");
        }




        protected virtual void OnTextChannelConnecting(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Text Is Connecting In Channel");
        }

        protected virtual void OnTextChannelConnected(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Text Has Connected In Channel");
        }

        protected virtual void OnTextChannelDisconnecting(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Text Is Disconnecting In Channel");
        }

        protected virtual void OnTextChannelDisconnected(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Text Has Disconnected In Channel");
        }



        #endregion


        #region Mute Callbacks

        protected virtual void OnLocalUserMuted()
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log("Local User is Muted");
        }

        protected virtual void OnLocalUserUnmuted()
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log("Local User is Unmuted");
        }

        protected virtual void OnCrossMuted(AccountId accountId)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"Player {accountId.DisplayName} has been Cross Muted");
        }

        protected virtual void OnCrossUnmuted(AccountId accountId)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"Player {accountId.DisplayName} has been Cross Unmuted");
        }

        protected virtual void OnUserMuted(IParticipant participant)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{participant.Account.DisplayName} Is Muted : (Muted For All : {participant.IsMutedForAll})");
        }

        protected virtual void OnUserUnmuted(IParticipant participant)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{participant.Account.DisplayName} Is Unmuted : (Muted For All : {participant.IsMutedForAll})");
        }

        protected virtual void OnUserSpeaking(IParticipant participant)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{participant.Account.DisplayName} Is Speaking : Audio Energy {participant.AudioEnergy}");
        }

        protected virtual void OnUserNotSpeaking(IParticipant participant)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{participant.Account.DisplayName} Is Not Speaking");
        }


        #endregion




        #region User Callbacks


        protected virtual void OnUserJoinedChannel(IParticipant participant)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{participant.Account.DisplayName} Has Joined The Channel");
        }

        protected virtual void OnUserLeftChannel(IParticipant participant)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{participant.Account.DisplayName} Has Left The Channel");
        }

        protected virtual void OnUserValuesUpdated(IParticipant participant)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{participant.Account.DisplayName} Has updated itself in the channel");
        }


        #endregion


        #region Message Callbacks


        protected virtual void OnChannelMessageRecieved(IChannelTextMessage textMessage)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"From {textMessage.Sender.DisplayName} : {textMessage.ReceivedTime} : {textMessage.Message}");
        }

        protected virtual void OnEventMessageRecieved(IChannelTextMessage textMessage)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"Event Message From {textMessage.Sender.DisplayName} : {textMessage.ReceivedTime} : {textMessage.ApplicationStanzaNamespace} : {textMessage.ApplicationStanzaBody} : {textMessage.Message}");
        }

        protected virtual void OnDirectMessageRecieved(IDirectedTextMessage directedTextMessage)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"Recived Message From : {directedTextMessage.Sender.DisplayName} : {directedTextMessage.ReceivedTime} : {directedTextMessage.Message}");
        }

        protected virtual void OnDirectMessageFailed(IFailedDirectedTextMessage failedMessage)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"Failed To Send Message From : {failedMessage.Sender}");
        }


        #endregion



        #region Audio Device Events

        private void OnAudioInputDeviceAdded(object sender, KeyEventArg<string> keyArgs)
        {
            var device = EasySession.Client.AudioInputDevices.AvailableDevices.Where(d => d.Key == keyArgs.Key).FirstOrDefault();
            if (!_settings.LogAllAudioDevices) { return; }
            Debug.Log($"Audio Input device has been added {device?.Name}");
        }

        private void OnAudioInputDeviceRemoved(object sender, KeyEventArg<string> keyArgs)
        {
            var device = EasySession.Client.AudioInputDevices.AvailableDevices.Where(d => d.Key == keyArgs.Key).FirstOrDefault();
            if (!_settings.LogAllAudioDevices) { return; }
            Debug.Log($"Audio Input device has been removed {device?.Name}");
        }

        private void OnAudioInputDeviceUpdated(object sender, ValueEventArg<string, IAudioDevice> valueArgs)
        {
            switch (valueArgs.PropertyName)
            {
                case "EventAfterDeviceAvailableAdded":
                    Debug.Log($"Audio Input Device Added {valueArgs.Value.Name}");
                    break;
                case "EventBeforeAvailableDeviceRemoved":
                    Debug.Log($"Audio Input Device Removed {valueArgs.Value.Name}");
                    break;
                case "EventEffectiveDeviceChanged":
                    Debug.Log($"Audio Input Device has been changed to {valueArgs.Value.Name}");
                    break;
            }
        }

        private void OnAudioOutputDeviceAdded(object sender, KeyEventArg<string> keyArgs)
        {
            var device = EasySession.Client.AudioOutputDevices.AvailableDevices.Where(d => d.Key == keyArgs.Key).FirstOrDefault();
            if (!_settings.LogAllAudioDevices) { return; }
            Debug.Log($"Audio Output device has been added {device?.Name}");
        }

        private void OnAudioOutputDeviceRemoved(object sender, KeyEventArg<string> keyArgs)
        {
            var device = EasySession.Client.AudioOutputDevices.AvailableDevices.Where(d => d.Key == keyArgs.Key).FirstOrDefault();
            if (!_settings.LogAllAudioDevices) { return; }
            Debug.Log($"Audio Output device has been removed {device?.Name}");
        }

        private void OnAudioOutputDeviceUpdated(object sender, ValueEventArg<string, IAudioDevice> valueArgs)
        {
            switch (valueArgs.PropertyName)
            {
                case "EventAfterDeviceAvailableAdded":
                    Debug.Log($"Audio Output Device Added {valueArgs.Value.Name}");
                    break;
                case "EventBeforeAvailableDeviceRemoved":
                    Debug.Log($"Audio Output Device Removed {valueArgs.Value.Name}");
                    break;
                case "EventEffectiveDeviceChanged":
                    Debug.Log($"Audio Output Device has been changed to {valueArgs.Value.Name}");
                    break;
            }
        }


        #endregion



        #region Text-to-Speech Callbacks

        protected virtual void OnTTSMessageAdded(ITTSMessageQueueEventArgs ttsArgs)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"TTS Message Has Been Added : {ttsArgs.Message.Text}");
        }

        protected virtual void OnTTSMessageRemoved(ITTSMessageQueueEventArgs ttsArgs)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"TTS Message Has Been Removed : {ttsArgs.Message.Text}");
        }

        protected virtual void OnTTSMessageUpdated(ITTSMessageQueueEventArgs ttsArgs)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"TTS Message Has Been Updated : {ttsArgs.Message.Text}");
        }


        #endregion



        #endregion






    }
}