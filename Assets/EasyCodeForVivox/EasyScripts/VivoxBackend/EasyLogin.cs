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

        public void Subscribe(ILoginSession loginSession)
        {
            loginSession.PropertyChanged += OnLoginPropertyChanged;
        }

        public void Unsubscribe(ILoginSession loginSession)
        {
            loginSession.PropertyChanged -= OnLoginPropertyChanged;
        }



        #region Login Methods


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
                    EasySession.LoggedInUserName = userName;
                }
            });
        }


        public void Logout(ILoginSession loginSession)
        {
            EasyEvents.OnLoggingOut(loginSession);
            loginSession.Logout();
            EasyEvents.OnLoggedOut(loginSession);
            Debug.Log($"Logging Out... Vivox does not have a Logging Out event callbacks because when you disconnect from there server their is no way to send a callback.".Color(EasyDebug.Yellow) +
                $" The events LoggingOut and LoggedOut are custom callback events. LoggingOut event will be called before the Logout method is called and LoggedOut event will be called after Logout method is called.".Color(EasyDebug.Yellow));
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

                    break;
                case LoginState.LoggedOut:

                    break;

                default:
                    Debug.Log($"Logging Callback Error - Logging In/Out failed");
                    break;
            }
        }


        #endregion
    }
}