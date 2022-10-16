using EasyCodeForVivox.Events;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using VivoxAccessToken;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyLogin : ILogin
    {
        private readonly EasyMessages _messages;
        private readonly EasyTextToSpeech _textToSpeech;
        private readonly EasyEvents _events;
        private readonly EasyEventsAsync _eventsAync;

        public EasyLogin(EasyMessages messages, EasyTextToSpeech textToSpeech,
            EasyEvents eventsSync, EasyEventsAsync eventsAync)
        {
            _messages = messages;
            _textToSpeech = textToSpeech;
            _events = eventsSync;
            _eventsAync = eventsAync;
        }

        public void Subscribe(ILoginSession loginSession)
        {
            loginSession.PropertyChanged += OnLoginPropertyChanged;
        }

        public void Unsubscribe(ILoginSession loginSession)
        {
            loginSession.PropertyChanged -= OnLoginPropertyChanged;
        }



        #region Login Methods

        public void LoginToVivox(string userName, bool joinMuted = false)
        {
            try
            {
                if (!EasyVivoxHelpers.FilterChannelAndUserName(userName)) { return; }

                EasySessionStatic.LoginSessions.Add(userName, EasySessionStatic.Client.GetLoginSession(new AccountId(EasySessionStatic.Issuer, userName, EasySessionStatic.Domain)));
                _messages.SubscribeToDirectMessages(EasySessionStatic.LoginSessions[userName]);
                _textToSpeech.Subscribe(EasySessionStatic.LoginSessions[userName]);

                LoginToVivox(EasySessionStatic.LoginSessions[userName], EasySessionStatic.APIEndpoint, userName, joinMuted);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
                _messages.UnsubscribeFromDirectMessages(EasySessionStatic.LoginSessions[userName]);
                _textToSpeech.Unsubscribe(EasySessionStatic.LoginSessions[userName]);
            }
        }

        public void LoginToVivox<T>(string userName, T value, bool joinMuted = false)
        {
            try
            {
                if (!EasyVivoxHelpers.FilterChannelAndUserName(userName)) { return; }

                EasySessionStatic.LoginSessions.Add(userName, EasySessionStatic.Client.GetLoginSession(new AccountId(EasySessionStatic.Issuer, userName, EasySessionStatic.Domain)));
                _messages.SubscribeToDirectMessages(EasySessionStatic.LoginSessions[userName]);
                _textToSpeech.Subscribe(EasySessionStatic.LoginSessions[userName]);

                LoginToVivox<T>(EasySessionStatic.LoginSessions[userName], value, EasySessionStatic.APIEndpoint, userName, joinMuted);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
                _messages.UnsubscribeFromDirectMessages(EasySessionStatic.LoginSessions[userName]);
                _textToSpeech.Unsubscribe(EasySessionStatic.LoginSessions[userName]);
            }
        }

        protected void LoginToVivox(ILoginSession loginSession,
            Uri serverUri, string userName, bool joinMuted = false)
        {
            Subscribe(loginSession);
            var accessToken = AccessToken.Token_f(EasySessionStatic.SecretKey, EasySessionStatic.Issuer,
                AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), "login", EasySessionStatic.UniqueCounter, null, EasySIP.GetUserSIP(
                    EasySessionStatic.Issuer, userName, EasySessionStatic.Domain), null);
            loginSession.BeginLogin(serverUri, accessToken, SubscriptionMode.Accept, null, null, null, ar =>
            {
                try
                {
                    loginSession.EndLogin(ar);
                }
                catch (Exception e)
                {
                    Unsubscribe(loginSession);
                    Debug.Log(e.StackTrace);
                }
                finally
                {
                    EasySessionStatic.Client.AudioInputDevices.Muted = joinMuted;
                }
            });
        }

        protected void LoginToVivox<T>(ILoginSession loginSession, T value,
            Uri serverUri, string userName, bool joinMuted = false)
        {
            Subscribe(loginSession);
            var accessToken = AccessToken.Token_f(EasySessionStatic.SecretKey, EasySessionStatic.Issuer,
                AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), "login", EasySessionStatic.UniqueCounter, null, EasySIP.GetUserSIP(
                    EasySessionStatic.Issuer, userName, EasySessionStatic.Domain), null);
            loginSession.BeginLogin(serverUri, accessToken, SubscriptionMode.Accept, null, null, null, async ar =>
            {
                try
                {
                    loginSession.EndLogin(ar);
                }
                catch (Exception e)
                {
                    Unsubscribe(loginSession);
                    Debug.Log(e.StackTrace);
                }
                finally
                {
                    EasySessionStatic.Client.AudioInputDevices.Muted = joinMuted;
                    await HandleDynamicEventsAsync(loginSession, value);
                }
            });
        }

        public async void Logout(string userName)
        {
            if (!EasySessionStatic.LoginSessions.TryGetValue(userName, out ILoginSession loginSession)) { return; }

            if (loginSession.State == LoginState.LoggedIn)
            {
                _messages.UnsubscribeFromDirectMessages(EasySessionStatic.LoginSessions[userName]);
                _textToSpeech.Unsubscribe(EasySessionStatic.LoginSessions[userName]);

                _events.OnLoggingOut(loginSession);
                await Task.Run(async () => await _eventsAync.OnLoggingOutAsync(loginSession));
                loginSession.Logout();
                _events.OnLoggedOut(loginSession);
                await Task.Run(async () => await _eventsAync.OnLoggedOutAsync(loginSession));
                Unsubscribe(loginSession);
                EasySessionStatic.LoginSessions.Remove(userName);

                Debug.Log($"Logging Out... Vivox does not have a Logging Out event callbacks because when you disconnect from there server their is no way to send a callback.".Color(EasyDebug.Yellow) +
                $" The events LoggingOut and LoggedOut are custom callback events. LoggingOut event will be called before the Logout method is called and LoggedOut event will be called after Logout method is called.".Color(EasyDebug.Yellow));

            }

            Debug.Log($"Not logged in".Color(EasyDebug.Yellow));
        }

        public async void Logout<T>(string userName, T value)
        {
            if (!EasySessionStatic.LoginSessions.TryGetValue(userName, out ILoginSession loginSession))
            {
                Debug.Log($"Login Session for Username {userName} does not exist".Color(EasyDebug.Red));
                return;
            }

            if (loginSession.State == LoginState.LoggedIn)
            {
                _messages.UnsubscribeFromDirectMessages(EasySessionStatic.LoginSessions[userName]);
                _textToSpeech.Unsubscribe(EasySessionStatic.LoginSessions[userName]);

                _events.OnLoggingOut(loginSession, value);
                await Task.Run(async () => await _eventsAync.OnLoggingOutAsync(loginSession, value));
                loginSession.Logout();
                _events.OnLoggedOut(loginSession);
                await Task.Run(async () => await _eventsAync.OnLoggedOutAsync(loginSession, value));
                Unsubscribe(loginSession);
                EasySessionStatic.LoginSessions.Remove(userName);

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


        #endregion
    }
}