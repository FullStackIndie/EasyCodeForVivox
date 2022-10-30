﻿using EasyCodeForVivox.Events;
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
    public class EasyManager : MonoBehaviour
    {
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

            if (EasySession.IsClientInitialized)
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
                    EasySession.IsClientInitialized = true;
                    _audio.Subscribe(EasySession.Client);
                    SubscribeToVivoxEvents();
                    if (_settings.LogEasyManager)
                        Debug.Log($"Vivox Client Initialized".Color(EasyDebug.Green));
                    if (_settings.UseDynamicEvents)
                    {
                        // may seem redundant to use Task.Run because multiple Tasks are created in DynamicEvents.RegisterEvents()
                        // but I tested running DynamicEvents.RegisterEvents() without using Task.Run and it was way slower
                        // difference was approximately .900 milliseconds vs .090 milliseconds(results will vary based on how many assemblies are in your project
                        // and how many cpus you have)
                        await Task.Run(async () =>
                        {
                            await DynamicEvents.RegisterEvents(_settings.OnlySearchAssemblyCSharp, _settings.LogAssemblySearches, _settings.LogAllDynamicMethods);
                            if (DynamicEvents.Methods.Count == 0)
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
            EasySession.Client.AudioInputDevices.AvailableDevices.AfterKeyAdded += _audio.OnAudioInputDeviceAdded;
            EasySession.Client.AudioInputDevices.AvailableDevices.BeforeKeyRemoved += _audio.OnAudioInputDeviceRemoved;
            EasySession.Client.AudioInputDevices.AvailableDevices.AfterValueUpdated += _audio.OnAudioInputDeviceUpdated;

            EasySession.Client.AudioOutputDevices.AvailableDevices.AfterKeyAdded += _audio.OnAudioOutputDeviceAdded;
            EasySession.Client.AudioOutputDevices.AvailableDevices.BeforeKeyRemoved += _audio.OnAudioOutputDeviceRemoved;
            EasySession.Client.AudioOutputDevices.AvailableDevices.AfterValueUpdated += _audio.OnAudioOutputDeviceUpdated;

            _events.LoggingIn += OnLoggingIn;
            _events.LoggedIn += OnLoggedIn;
            _events.LoggedIn += OnLoggedInSetup;
            _events.LoggingOut += OnLoggingOut;
            _events.LoggedOut += OnLoggedOut;

            _events.ChannelConnecting += OnChannelConnecting;
            _events.ChannelConnected += OnChannelConnected;
            _events.ChannelDisconnecting += OnChannelDisconnecting;
            _events.ChannelDisconnected += OnChannelDisconnected;

            _events.AudioChannelConnecting += OnVoiceConnecting;
            _events.AudioChannelConnected += OnVoiceConnected;
            _events.AudioChannelDisconnecting += OnVoiceDisconnecting;
            _events.AudioChannelDisconnected += OnVoiceDisconnected;

            _events.TextChannelConnecting += OnTextChannelConnecting;
            _events.TextChannelConnected += OnTextChannelConnected;
            _events.TextChannelDisconnecting += OnTextChannelDisconnecting;
            _events.TextChannelDisconnected += OnTextChannelDisconnected;

            _events.ChannelMessageRecieved += OnChannelMessageRecieved;
            _events.EventMessageRecieved += OnEventMessageRecieved;

            _events.DirectMessageRecieved += OnDirectMessageRecieved;
            _events.DirectMessageFailed += OnDirectMessageFailed;

            _events.LocalUserMuted += OnLocalUserMuted;
            _events.LocalUserUnmuted += OnLocalUserUnmuted;

            _events.UserJoinedChannel += OnUserJoinedChannel;
            _events.UserLeftChannel += OnUserLeftChannel;
            _events.UserValuesUpdated += OnUserValuesUpdated;

            _events.UserMuted += OnUserMuted;
            _events.UserUnmuted += OnUserUnmuted;
            _events.UserSpeaking += OnUserSpeaking;
            _events.UserNotSpeaking += OnUserNotSpeaking;

            _events.TTSMessageAdded += OnTTSMessageAdded;
            _events.TTSMessageRemoved += OnTTSMessageRemoved;
            _events.TTSMessageUpdated += OnTTSMessageUpdated;

        }

        public void UnsubscribeToVivoxEvents()
        {
            EasySession.Client.AudioInputDevices.AvailableDevices.AfterKeyAdded -= _audio.OnAudioInputDeviceAdded;
            EasySession.Client.AudioInputDevices.AvailableDevices.BeforeKeyRemoved -= _audio.OnAudioInputDeviceRemoved;
            EasySession.Client.AudioInputDevices.AvailableDevices.AfterValueUpdated -= _audio.OnAudioInputDeviceUpdated;

            EasySession.Client.AudioOutputDevices.AvailableDevices.AfterKeyAdded -= _audio.OnAudioOutputDeviceAdded;
            EasySession.Client.AudioOutputDevices.AvailableDevices.BeforeKeyRemoved -= _audio.OnAudioOutputDeviceRemoved;
            EasySession.Client.AudioOutputDevices.AvailableDevices.AfterValueUpdated -= _audio.OnAudioOutputDeviceUpdated;

            _events.LoggingIn -= OnLoggingIn;
            _events.LoggedIn -= OnLoggedIn;
            _events.LoggedIn -= OnLoggedInSetup;
            _events.LoggingOut -= OnLoggingOut;
            _events.LoggedOut -= OnLoggedOut;

            _events.ChannelConnecting -= OnChannelConnecting;
            _events.ChannelConnected -= OnChannelConnected;
            _events.ChannelDisconnecting -= OnChannelDisconnecting;
            _events.ChannelDisconnected -= OnChannelDisconnected;

            _events.AudioChannelConnecting -= OnVoiceConnecting;
            _events.AudioChannelConnected -= OnVoiceConnected;
            _events.AudioChannelDisconnecting -= OnVoiceDisconnecting;
            _events.AudioChannelDisconnected -= OnVoiceDisconnected;

            _events.TextChannelConnecting -= OnTextChannelConnecting;
            _events.TextChannelConnected -= OnTextChannelConnected;
            _events.TextChannelDisconnecting -= OnTextChannelDisconnecting;
            _events.TextChannelDisconnected -= OnTextChannelDisconnected;

            _events.ChannelMessageRecieved -= OnChannelMessageRecieved;
            _events.EventMessageRecieved -= OnEventMessageRecieved;

            _events.DirectMessageRecieved -= OnDirectMessageRecieved;
            _events.DirectMessageFailed -= OnDirectMessageFailed;

            _events.LocalUserMuted -= OnLocalUserMuted;
            _events.LocalUserUnmuted -= OnLocalUserUnmuted;

            _events.UserJoinedChannel -= OnUserJoinedChannel;
            _events.UserLeftChannel -= OnUserLeftChannel;
            _events.UserValuesUpdated -= OnUserValuesUpdated;

            _events.UserMuted -= OnUserMuted;
            _events.UserUnmuted -= OnUserUnmuted;
            _events.UserSpeaking -= OnUserSpeaking;
            _events.UserNotSpeaking -= OnUserNotSpeaking;

            _events.TTSMessageAdded -= OnTTSMessageAdded;
            _events.TTSMessageRemoved -= OnTTSMessageRemoved;
            _events.TTSMessageUpdated -= OnTTSMessageUpdated;

        }



        #region Main Methods



        protected void LoginToVivox(string userName, bool joinMuted = false)
        {
            _login.LoginToVivox(userName, joinMuted);
        }

        protected void LoginToVivox<T>(string userName, T eventParameter, bool joinMuted = false)
        {
            _login.LoginToVivox(userName, eventParameter, joinMuted);
        }


        protected void LogoutOfVivox(string userName)
        {
            _login.Logout(userName);
        }

        protected void JoinChannel(string userName, string channelName, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel, ChannelType channelType,
            bool joinMuted = false, Channel3DProperties channel3DProperties = default)
        {
            _channel.JoinChannel(userName, channelName, includeVoice, includeText, switchTransmissionToThisChannel, channelType, joinMuted, channel3DProperties);
        }

        protected void LeaveChannel(string channelName, string userName)
        {
            _channel.LeaveChannel(channelName, userName);
        }

        protected void SetVoiceActiveInChannel(string userName, string channelName, bool connect)
        {
            // todo fix error where channel disconects if both text and voice are disconnected and when you try and toggle 
            // you get an object null reference because channel name exists but channelsession doesnt exist
            IChannelSession channelSession = EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(_channel.GetChannelSIP(channelName)));
            _voiceChannel.ToggleAudioChannelActive(channelSession, connect);
        }

        protected void SetTextActiveInChannel(string userName, string channelName, bool connect)
        {
            // todo fix error where channel disconects if both text and voice are disconnected and when you try and toggle 
            // you get an object null reference because channel name exists but channelsession doesnt exist
            IChannelSession channelSession = EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(_channel.GetChannelSIP(channelName)));
            _textChannel.ToggleTextChannelActive(channelSession, connect);
        }

        protected void SendChannelMessage(string userName, string channelName, string msg, string title = "", string body = "")
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

        protected void SendEventMessage(string userName, string channelName, string msg, string title = "", string body = "")
        {
            _messages.SendChannelMessage(_channel.GetExistingChannelSession(userName, channelName),
                msg, title, body);
        }

        protected void SendDirectMessage(string userName, string userToMsg, string msg, string title = "", string body = "")
        {
            // todo check if user is blocked and alert front end users
            // not supporting presence so not neccessary - leaving todo in case things change
            _messages.SendDirectMessage(EasySession.LoginSessions[userName], userToMsg, msg, title, body);
        }

        protected void MuteSelf()
        {
            _mute.LocalMuteSelf(EasySession.Client);
        }

        protected void UnmuteSelf()
        {
            _mute.LocalUnmuteSelf(EasySession.Client);
        }

        protected void ToggleMuteRemoteUser(string userName, string channelName)
        {
            _mute.LocalToggleMuteRemoteUser(userName, _channel.GetExistingChannelSession(userName, channelName));
        }

        protected void MuteAllPlayers(string channelName)
        {
            if (EasySession.ChannelSessions.ContainsKey(channelName))
            {
                _mute.MuteAllUsers(EasySession.ChannelSessions[channelName]);
            }
            else
            {
                if (_settings.LogEasyManager)
                    Debug.Log("Channel Does not exist. Cannot Mute player");
            }
        }

        protected void UnmuteAllPlayers(string channelName)
        {
            if (EasySession.ChannelSessions.ContainsKey(channelName))
            {
                _mute.UnmuteAllUsers(EasySession.ChannelSessions[channelName]);
            }
            else
            {
                if (_settings.LogEasyManager)
                    Debug.Log("Channel Does not exist. Cannot Unmute player");
            }
        }

        protected IEnumerable<ChannelId> GetTransmittingChannelsForPlayer(string userName)
        {
            return EasySession.LoginSessions[userName].TransmittingChannels;
        }

        protected bool IsPlayerTransmittingInChannel(string channelName)
        {
            return EasySession.ChannelSessions[channelName].IsTransmitting;
        }

        protected void SetPlayerTransmissionMode(string userName, TransmissionMode transmissionMode, ChannelId channelId = default)
        {
            EasySession.LoginSessions[userName].SetTransmissionMode(transmissionMode, channelId);
        }

        protected void AdjustLocalUserVolume(int volume)
        {
            _audio.AdjustLocalPlayerAudioVolume(volume, EasySession.Client);
        }

        protected void AdjustRemoteUserVolume(string userName, string channelName, float volume)
        {
            IChannelSession channelSession = _channel.GetExistingChannelSession(userName, channelName);
            _audio.AdjustRemotePlayerAudioVolume(userName, channelSession, volume);
        }

        protected void CrossMuteUser(string userName, string channelName, string userToMute, bool mute)
        {
            _mute.CrossMuteUser(userName, channelName, userToMute, mute);
        }

        protected void CrossMuteUsers(string userName, string channelName, List<string> usersToMute, bool mute)
        {
            _mute.CrossMuteUsers(userName, channelName, usersToMute, mute);
        }

        protected void ClearCrossMutedUsersForLoginSession(string loggedInUserName)
        {
            _mute.ClearAllCurrentCrossMutedAccounts(loggedInUserName);
        }

        protected void InjectAudio(string username, string audioPath)
        {
            _audio.StartAudioInjection(audioPath, EasySession.LoginSessions[username]);
        }

        protected void StopInjectedAudio(string username)
        {
            _audio.StopAudioInjection(EasySession.LoginSessions[username]);
        }

        protected void SetAutoVoiceActivityDetection(string userName)
        {
            _audio.SetAutoVoiceActivityDetection(userName);
        }

        protected void SetVoiceActivityDetection(string userName, int hangover, int sensitivity, int noiseFloor)
        {
            _audio.SetVoiceActivityDetection(userName, hangover, sensitivity, noiseFloor);
        }

        protected void SetAudioInputDevice(string deviceName)
        {
            var audioDevice = EasySession.Client.AudioInputDevices.AvailableDevices.Where(d => d.Name.Contains(deviceName)).FirstOrDefault();
            if (audioDevice != null)
            {
                if (_settings.LogEasyManager)
                    Debug.Log("Setting New Audio Device");
                _audio.SetAudioDeviceInput(audioDevice, EasySession.Client);
                return;
            }
            if (_settings.LogEasyManager)
                Debug.Log("Could not find Audio device");
        }

        protected void SetAudioOutputDevice(string deviceName)
        {
            var audioDevice = EasySession.Client.AudioOutputDevices.AvailableDevices.Where(d => d.Name.Contains(deviceName)).FirstOrDefault();
            if (audioDevice != null)
            {
                _audio.SetAudioDeviceOutput(audioDevice, EasySession.Client);
            }
        }

        protected void PushToTalk(bool enable, KeyCode keyCode)
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


        protected void SpeakTTS(string msg, string userName)
        {
            _textToSpeech.TTSSpeak(msg, TTSDestination.QueuedLocalPlayback, EasySession.LoginSessions[userName]);
        }

        protected void SpeakTTS(string msg, string userName, TTSDestination playMode)
        {
            _textToSpeech.TTSSpeak(msg, playMode, EasySession.LoginSessions[userName]);
        }

        protected void ChooseVoiceGender(VoiceGender voiceGender, ILoginSession loginSession)
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
            ChooseVoiceGender(VoiceGender.female, loginSession);
        }


        // todo add Extra/Multiple Login callbacks to EasyLogin

        protected virtual void OnLoginAdded(ILoginSession loginSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"Login Added : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
        }

        protected virtual void OnLoginRemoved(ILoginSession loginSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"Login Removed : {loginSession.LoginSessionId.DisplayName} : Presence = {loginSession.Presence.Status}");
        }

        protected virtual void OnLoginUpdated(ILoginSession loginSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"Login Updated : Login Updated : {loginSession.LoginSessionId.DisplayName} : Presence = {loginSession.Presence.Status}");
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




        protected virtual void OnVoiceConnecting(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Audio Is Connecting In Channel");
        }

        protected virtual void OnVoiceConnected(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Audio Has Connected In Channel");
        }

        protected virtual void OnVoiceDisconnecting(IChannelSession channelSession)
        {
            if (_settings.LogEasyManagerEventCallbacks)
                Debug.Log($"{channelSession.Channel.Name} Audio Is Disconnecting In Channel");
        }

        protected virtual void OnVoiceDisconnected(IChannelSession channelSession)
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


        #region Local User Mute Callbacks

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