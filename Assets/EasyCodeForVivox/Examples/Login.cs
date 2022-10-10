using System;
using UnityEngine;
using UnityEngine.UI;
using VivoxUnity;
using Zenject;

namespace EasyCodeForVivox
{
    public class Login : MonoBehaviour
    {

        [SerializeField] InputField userName;

        private ILogin _login;
        private IMessages _messages;
        private ITextToSpeech _textToSpeech;

        [Inject]
        private void Initialize(ILogin login, IMessages messages, ITextToSpeech textToSpeech)
        {
            _login = login;
            _messages = messages;
            _textToSpeech = textToSpeech;
        }

        private void Start()
        {
            EasyEvents.LoggingIn += OnLoggingIn;
            EasyEvents.LoggedIn += OnLoggedIn;
            EasyEvents.LoggedIn += OnLoggedInSetup;
            EasyEvents.LoggingOut += OnLoggingOut;
            EasyEvents.LoggedOut += OnLoggedOut;
        }


        public void LoginToVivox()
        {
            try
            {
                EasySession.LoginSessions.Add(userName.text, EasySession.Client.GetLoginSession(new AccountId(EasySession.Issuer, userName.text, EasySession.Domain)));
                _messages.SubscribeToDirectMessages(EasySession.LoginSessions[userName.text]);
                _textToSpeech.Subscribe(EasySession.LoginSessions[userName.text]);

                _login.LoginToVivox(EasySession.LoginSessions[userName.text], EasySession.APIEndpoint, userName.text);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
                _messages.UnsubscribeFromDirectMessages(EasySession.LoginSessions[userName.text]);
                _textToSpeech.Unsubscribe(EasySession.LoginSessions[userName.text]);
            }
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

    }
}



