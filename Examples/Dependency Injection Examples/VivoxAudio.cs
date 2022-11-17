using EasyCodeForVivox;
using UnityEngine;
using Zenject;

namespace EasyCodeDevelopment
{
    public class VivoxAudio : MonoBehaviour
    {
        EasyAudio _audio;

        [Inject]
        private void Initialize(EasyAudio audio)
        {
            _audio = audio;
        }

        public void AdjustLocalPlayerAudioVolume()
        {
            _audio.AdjustLocalPlayerAudioVolume(15, EasySession.Client);
        }

        public void AdjustRemotePlayerAudioVolume()
        {
            _audio.SetAudioInputDevice("deviceName", EasySession.Client);
            _audio.AdjustRemotePlayerAudioVolume("userName", EasySession.ChannelSessions["channelName"], 15);
        }

        public void SetAutoVoiceActivityDetection()
        {
            _audio.SetAutoVoiceActivityDetection("userName");
        }

        public void SetVoiceActivityDetection()
        {
            _audio.SetVoiceActivityDetection("userName", hangover: 2000, sensitivity: 43, noiseFloor: 576);
        }

        public void SetAudioInputDevice()
        {
            // get list of audio device names
            _audio.SetAudioInputDevice("deviceName", EasySession.Client);
        }
    }
}
