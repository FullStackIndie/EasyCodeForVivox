using EasyCodeForVivox.Events;
using EasyCodeForVivox.Events.Internal;
using EasyCodeForVivox.Extensions;
using EasyCodeForVivox.Utilities;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyLogin
    {
        private readonly EasyMessages _messages;
        private readonly EasyTextToSpeech _textToSpeech;
        private readonly EasyMute _mute;
        private readonly EasyEvents _events;
        private readonly EasyEventsAsync _eventsAync;
        private readonly EasySettingsSO _settings;

        public EasyLogin(EasyMessages messages, EasyTextToSpeech textToSpeech,
            EasyEvents eventsSync, EasyEventsAsync eventsAync,
            EasySettingsSO easySettings, EasyMute mute)
        {
            _messages = messages;
            _textToSpeech = textToSpeech;
            _events = eventsSync;
            _eventsAync = eventsAync;
            _mute = mute;
            _settings = easySettings;
        }

        private void Subscribe(ILoginSession loginSession)
        {
            loginSession.PropertyChanged += OnLoginPropertyChanged;
            EasySession.Client.LoginSessions.AfterKeyAdded += OnLoginAdded;
            EasySession.Client.LoginSessions.BeforeKeyRemoved += OnLoginRemoved;
            EasySession.Client.LoginSessions.AfterValueUpdated += OnLoginUpdated;
        }

        private void Unsubscribe(ILoginSession loginSession)
        {
            loginSession.PropertyChanged -= OnLoginPropertyChanged;
            EasySession.Client.LoginSessions.AfterKeyAdded -= OnLoginAdded;
            EasySession.Client.LoginSessions.BeforeKeyRemoved -= OnLoginRemoved;
            EasySession.Client.LoginSessions.AfterValueUpdated -= OnLoginUpdated;
        }



        #region Login Methods

        public void LoginToVivox(string userName, string displayName = default, bool joinMuted = false)
        {
            try
            {
                if (!EasyVivoxUtilities.FilterChannelAndUserName(userName)) { return; }

                EasySession.LoginSessions.Add(userName, EasySession.Client.GetLoginSession(new AccountId(EasySession.Issuer, userName, EasySession.Domain, displayName)));
                _messages.SubscribeToDirectMessages(EasySession.LoginSessions[userName]);
                _textToSpeech.Subscribe(EasySession.LoginSessions[userName]);
                _mute.Subscribe(EasySession.LoginSessions[userName]);

                LoginToVivox(EasySession.LoginSessions[userName], EasySession.APIEndpoint, userName, joinMuted);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                _messages.UnsubscribeFromDirectMessages(EasySession.LoginSessions[userName]);
                _textToSpeech.Unsubscribe(EasySession.LoginSessions[userName]);
                _mute.Unsubscribe(EasySession.LoginSessions[userName]);
            }
        }

        public void LoginToVivox<T>(string userName, T value, string displayName = default, bool joinMuted = false)
        {
            try
            {
                if (!EasyVivoxUtilities.FilterChannelAndUserName(userName)) { return; }

                EasySession.LoginSessions.Add(userName, EasySession.Client.GetLoginSession(new AccountId(EasySession.Issuer, userName, EasySession.Domain, displayName)));
                _messages.SubscribeToDirectMessages(EasySession.LoginSessions[userName]);
                _textToSpeech.Subscribe(EasySession.LoginSessions[userName]);
                _mute.Subscribe(EasySession.LoginSessions[userName]);

                LoginToVivox<T>(EasySession.LoginSessions[userName], value, EasySession.APIEndpoint, userName, joinMuted);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                _messages.UnsubscribeFromDirectMessages(EasySession.LoginSessions[userName]);
                _textToSpeech.Unsubscribe(EasySession.LoginSessions[userName]);
                _mute.Unsubscribe(EasySession.LoginSessions[userName]);
            }
        }

        protected void LoginToVivox(ILoginSession loginSession,
            Uri serverUri, string userName, bool joinMuted = false)
        {
            Subscribe(loginSession);
            var accessToken = EasyAccessToken.CreateToken(EasySession.SecretKey, EasySession.Issuer,
                EasyAccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), "login", EasySession.UniqueCounter, null, EasySIP.GetUserSIP(
                    EasySession.Issuer, userName, EasySession.Domain), null);

            loginSession.ParticipantPropertyFrequency = _settings.VivoxParticipantPropertyUpdateFrequency;
            loginSession.BeginLogin(serverUri, accessToken, SubscriptionMode.Accept, null, null, null, ar =>
            {
                try
                {
                    loginSession.EndLogin(ar);
                }
                catch (Exception e)
                {
                    Unsubscribe(loginSession);
                    Debug.LogException(e);
                }
                finally
                {
                    EasySession.Client.AudioInputDevices.Muted = joinMuted;
                    loginSession.SetTransmissionMode(TransmissionMode.All);
                }
            });
        }

        protected void LoginToVivox<T>(ILoginSession loginSession, T value,
            Uri serverUri, string userName, bool joinMuted = false)
        {
            Subscribe(loginSession);
            var accessToken = EasyAccessToken.CreateToken(EasySession.SecretKey, EasySession.Issuer,
                EasyAccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), "login", EasySession.UniqueCounter, null, EasySIP.GetUserSIP(
                    EasySession.Issuer, userName, EasySession.Domain), null);

            loginSession.ParticipantPropertyFrequency = _settings.VivoxParticipantPropertyUpdateFrequency;
            loginSession.BeginLogin(serverUri, accessToken, SubscriptionMode.Accept, null, null, null, async ar =>
            {
                try
                {
                    loginSession.EndLogin(ar);
                }
                catch (Exception e)
                {
                    Unsubscribe(loginSession);
                    Debug.LogException(e);
                }
                finally
                {
                    EasySession.Client.AudioInputDevices.Muted = joinMuted;
                    loginSession.SetTransmissionMode(TransmissionMode.All);
                    await HandleDynamicEventsAsync(loginSession, value);
                }
            });
        }

        public void UpdateLoginProperties(string userName, ParticipantPropertyUpdateFrequency updateFrequency)
        {
            EasySession.LoginSessions[userName].BeginAccountSetLoginProperties(updateFrequency, ar =>
            {
                try
                {
                    EasySession.LoginSessions[userName].EndAccountSetLoginProperties(ar);
                }
                catch (Exception e)
                {
                    Unsubscribe(EasySession.LoginSessions[userName]);
                    Debug.LogException(e);
                }
            });
        }

        public void SetPlayerTransmissionMode(string userName, TransmissionMode transmissionMode, ChannelId channelId = default)
        {
            EasySession.LoginSessions[userName].SetTransmissionMode(transmissionMode, channelId);
            Debug.Log($"Switching Transmission to {transmissionMode} {(channelId != null ? $"for channel {channelId.Name}" : "")}");
        }

        public ChannelId GetChannelId(string userName, string channelName)
        {
            return EasySession.LoginSessions[userName].GetChannelId(channelName);
        }

        public async void LogoutOfVivox(string userName)
        {
            if (!EasySession.LoginSessions.TryGetValue(userName, out ILoginSession loginSession)) { return; }

            if (loginSession.State == LoginState.LoggedIn)
            {
                _messages.UnsubscribeFromDirectMessages(EasySession.LoginSessions[userName]);
                _textToSpeech.Unsubscribe(EasySession.LoginSessions[userName]);

                _events.OnLoggingOut(loginSession);
                await Task.Run(async () => await _eventsAync.OnLoggingOutAsync(loginSession));
                loginSession.Logout();
                _events.OnLoggedOut(loginSession);
                await Task.Run(async () => await _eventsAync.OnLoggedOutAsync(loginSession));
                Unsubscribe(loginSession);
                EasySession.LoginSessions.Remove(userName);

                Debug.Log($"Logging Out... Vivox does not have a Logging Out event callbacks because when you disconnect from there server their is no way to send a callback.".Color(EasyDebug.Yellow) +
                $" The events LoggingOut and LoggedOut are custom callback events. LoggingOut event will be called before the Logout method is called and LoggedOut event will be called after Logout method is called.".Color(EasyDebug.Yellow));

            }

            Debug.Log($"Not logged in".Color(EasyDebug.Yellow));
        }

        public async void LogoutOfVivox<T>(string userName, T value)
        {
            if (!EasySession.LoginSessions.TryGetValue(userName, out ILoginSession loginSession))
            {
                Debug.Log($"Login Session for Username {userName} does not exist".Color(EasyDebug.Red));
                return;
            }

            if (loginSession.State == LoginState.LoggedIn)
            {
                _messages.UnsubscribeFromDirectMessages(EasySession.LoginSessions[userName]);
                _textToSpeech.Unsubscribe(EasySession.LoginSessions[userName]);

                _events.OnLoggingOut(loginSession, value);
                await Task.Run(async () => await _eventsAync.OnLoggingOutAsync(loginSession, value));
                loginSession.Logout();
                _events.OnLoggedOut(loginSession);
                await Task.Run(async () => await _eventsAync.OnLoggedOutAsync(loginSession, value));
                Unsubscribe(loginSession);
                EasySession.LoginSessions.Remove(userName);

                Debug.Log($"Logging Out... Vivox does not have a Logging Out event callbacks because when you disconnect from there server their is no way to send a callback.".Color(EasyDebug.Yellow) +
                $" The events LoggingOut and LoggedOut are custom callback events. LoggingOut event will be called before the Logout method is called and LoggedOut event will be called after Logout method is called.".Color(EasyDebug.Yellow));

            }

            Debug.Log($"Not logged in".Color(EasyDebug.Yellow));
        }

        #endregion


        #region Login Callbacks

        // login status changed
        public async void OnLoginPropertyChanged(object sender, PropertyChangedEventArgs propArgs)
        {
            var senderLoginSession = (ILoginSession)sender;

            if (propArgs.PropertyName == "State")
            {
                switch (senderLoginSession.State)
                {
                    case LoginState.LoggingIn:
                        _events.OnLoggingIn(senderLoginSession);
                        await _eventsAync.OnLoggingInAsync(senderLoginSession);
                        break;
                    case LoginState.LoggedIn:
                        _events.OnLoggedIn(senderLoginSession);
                        await _eventsAync.OnLoggedInAsync(senderLoginSession);
                        break;
                    case LoginState.LoggingOut:
                        _events.OnLoggingOut(senderLoginSession);
                        await _eventsAync.OnLoggingOutAsync(senderLoginSession);
                        break;
                    case LoginState.LoggedOut:
                        _events.OnLoggedOut(senderLoginSession);
                        await _eventsAync.OnLoggedOutAsync(senderLoginSession);
                        break;

                    default:
                        Debug.Log($"Event Error - Logging In/Out events failed".Color(EasyDebug.Red));
                        break;
                }
            }
        }

        private async Task HandleDynamicEventsAsync<T>(ILoginSession senderLoginSession, T value)
        {
            switch (senderLoginSession.State)
            {
                case LoginState.LoggingIn:
                    _events.OnLoggingIn(senderLoginSession, value);
                    await _eventsAync.OnLoggingInAsync(senderLoginSession, value);
                    break;
                case LoginState.LoggedIn:
                    _events.OnLoggedIn(senderLoginSession, value);
                    await _eventsAync.OnLoggedInAsync(senderLoginSession, value);
                    break;
                case LoginState.LoggingOut:
                    _events.OnLoggingOut(senderLoginSession, value);
                    await _eventsAync.OnLoggingOutAsync(senderLoginSession, value);
                    break;
                case LoginState.LoggedOut:
                    _events.OnLoggedOut(senderLoginSession, value);
                    await _eventsAync.OnLoggedOutAsync(senderLoginSession, value);
                    break;

                default:
                    Debug.Log($"Dynamic Event Error - Logging In/Out Dynamic events failed".Color(EasyDebug.Red));
                    break;
            }
        }


        public void OnLoginAdded(object sender, KeyEventArg<AccountId> accountId)
        {
            _events.OnLoginAdded(accountId.Key);
        }

        public void OnLoginRemoved(object sender, KeyEventArg<AccountId> accountId)
        {
            _events.OnLoginRemoved(accountId.Key);
        }

        public void OnLoginUpdated(object sender, ValueEventArg<AccountId, ILoginSession> valueEventArgs)
        {
            _events.OnLoginUpdated(valueEventArgs.Value);
        }


        #endregion
    }
}