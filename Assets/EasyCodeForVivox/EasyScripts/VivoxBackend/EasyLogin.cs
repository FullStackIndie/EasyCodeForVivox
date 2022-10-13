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

        public EasyLogin(EasyMessages messages, EasyTextToSpeech textToSpeech)
        {
            _messages = messages;
            _textToSpeech = textToSpeech;
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

                EasySession.LoginSessions.Add(userName, EasySession.Client.GetLoginSession(new AccountId(EasySession.Issuer, userName, EasySession.Domain)));
                _messages.SubscribeToDirectMessages(EasySession.LoginSessions[userName]);
                _textToSpeech.Subscribe(EasySession.LoginSessions[userName]);

                LoginToVivox(EasySession.LoginSessions[userName], EasySession.APIEndpoint, userName, joinMuted);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
                _messages.UnsubscribeFromDirectMessages(EasySession.LoginSessions[userName]);
                _textToSpeech.Unsubscribe(EasySession.LoginSessions[userName]);
            }
        }

        public void LoginToVivox(ILoginSession loginSession,
            Uri serverUri, string userName, bool joinMuted = false)
        {
            Subscribe(loginSession);
            var accessToken = AccessToken.Token_f(EasySession.SecretKey, EasySession.Issuer,
                AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), "login", EasySession.UniqueCounter, null, EasySIP.GetUserSIP(
                    EasySession.Issuer, userName, EasySession.Domain), null);
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
                    EasySession.Client.AudioInputDevices.Muted = joinMuted;
                }
            });
        }

        public void Logout(string userName)
        {
            var loginSession = EasySession.LoginSessions[userName];
            if (loginSession.State == LoginState.LoggedIn)
            {
                _messages.UnsubscribeFromDirectMessages(EasySession.LoginSessions[userName]);
                _textToSpeech.Unsubscribe(EasySession.LoginSessions[userName]);
                Logout(userName);

                EasySession.LoginSessions.Remove(userName);
                EasyEvents.OnLoggingOut(loginSession);
                loginSession.Logout();
                EasyEvents.OnLoggedOut(loginSession);

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
                        EasyEvents.OnLoggingIn(senderLoginSession);
                        break;
                    case LoginState.LoggedIn:
                        EasyEvents.OnLoggedIn(senderLoginSession);
                        break;
                    case LoginState.LoggingOut:
                        EasyEvents.OnLoggingOut(senderLoginSession);
                        break;
                    case LoginState.LoggedOut:
                        EasyEvents.OnLoggedOut(senderLoginSession);
                        Unsubscribe(senderLoginSession);
                        break;

                    default:
                        Debug.Log($"Logging Callback Error - Logging In/Out failed");
                        break;
                }
                if (EasySession.UseDynamicEvents)
                {
                    await HandleDynamicEvents(senderLoginSession);
                }
            }
        }

        private async Task HandleDynamicEvents(ILoginSession senderLoginSession)
        {
            switch (senderLoginSession.State)
            {
                case LoginState.LoggingIn:
                    await EasyEventsAsync.OnLoggingInAsync(senderLoginSession);
                    break;
                case LoginState.LoggedIn:
                    await EasyEventsAsync.OnLoggedInAsync(senderLoginSession);
                    break;
                case LoginState.LoggingOut:
                    await EasyEventsAsync.OnLoggingOutAsync(senderLoginSession);
                    break;
                case LoginState.LoggedOut:
                    await EasyEventsAsync.OnLoggedOutAsync(senderLoginSession);
                    break;

                default:
                    Debug.Log($"Logging Callback Error - Logging In/Out failed");
                    break;
            }
        }


        #endregion
    }
}