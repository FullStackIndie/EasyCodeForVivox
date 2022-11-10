using EasyCodeForVivox.Events;
using UnityEngine;
using VivoxUnity;
using Zenject;

namespace EasyCodeForVivox.Examples
{
    internal class DependencyInjectionExample
    {
        private EasyEvents _events;

        [Inject]
        private void Initialize(EasyEvents events)
        {
            _events = events;
        }

        public void AddEvent()
        {
            _events.LoggedIn += OnLoggedIn;
        }

        public void RemoveEvent()
        {
            _events.LoggedIn -= OnLoggedIn;
        }

        private void OnLoggedIn(ILoginSession loginSession)
        {
            Debug.Log($"User {loginSession.LoginSessionId.DisplayName} has logged in");
        }

        [Inject]
        private void AudioSettings(EasyAudio audio)
        {
            audio.AdjustLocalPlayerAudioVolume(25, EasySession.Client);
        }
    }
}
