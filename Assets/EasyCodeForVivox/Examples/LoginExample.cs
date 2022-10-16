using EasyCodeForVivox.Events;
using System;
using UnityEngine;
using UnityEngine.UI;
using VivoxUnity;
using Zenject;

namespace EasyCodeForVivox
{
    public class LoginExample : MonoBehaviour
    {

        [SerializeField] InputField userName;

        private ILogin _login;
        private IMessages _messages;
        private ITextToSpeech _textToSpeech;
        private EasySession _session;

        [Inject]
        private void Initialize(ILogin login, IMessages messages, ITextToSpeech textToSpeech, EasySession session)
        {
            _login = login;
            _messages = messages;
            _textToSpeech = textToSpeech;
            _session = session;
        }

        private void Start()
        {
            EasyEventsStatic.LoggingIn += OnLoggingIn;
            EasyEventsStatic.LoggedIn += OnLoggedIn;
            EasyEventsStatic.LoggedIn += OnLoggedInSetup;
            EasyEventsStatic.LoggingOut += OnLoggingOut;
            EasyEventsStatic.LoggedOut += OnLoggedOut;
        }


        public void LoginToVivox()
        {
            try
            {
                EasySessionStatic.LoginSessions.Add(userName.text, EasySessionStatic.Client.GetLoginSession(new AccountId(EasySessionStatic.Issuer, userName.text, EasySessionStatic.Domain)));
                _messages.SubscribeToDirectMessages(EasySessionStatic.LoginSessions[userName.text]);
                _textToSpeech.Subscribe(EasySessionStatic.LoginSessions[userName.text]);

                _login.LoginToVivox(userName.text);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
                _messages.UnsubscribeFromDirectMessages(EasySessionStatic.LoginSessions[userName.text]);
                _textToSpeech.Unsubscribe(EasySessionStatic.LoginSessions[userName.text]);
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



