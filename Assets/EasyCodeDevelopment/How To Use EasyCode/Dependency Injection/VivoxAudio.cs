using EasyCodeForVivox;
using System.IO;
using System.Linq;
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

        public void GetAllAudioInputDevices()
        {
            var audioDevices = _audio.GetAudioInputDevices(EasySession.Client);
        }

        public void GetAllAudioOutputDevices()
        {
            var audioDevices = _audio.GetAudioOutputDevices(EasySession.Client);
        }

        public void SetAudioInputDevice()
        {
            var audioDevices = _audio.GetAudioInputDevices(EasySession.Client).ToList();
            foreach (var device in audioDevices)
            {
                Debug.Log(device.Name);
            }
            _audio.SetAudioInputDevice(audioDevices.First().Name, EasySession.Client);
        }

        public void SetAudioOutputDevice()
        {
            var audioDevices = _audio.GetAudioOutputDevices(EasySession.Client).ToList();
            foreach (var device in audioDevices)
            {
                Debug.Log(device.Name);
            }
            _audio.SetAudioOutputDevice(audioDevices.First().Name, EasySession.Client);
        }

        public void RefreshAllAudioDevices()
        {
            _audio.RefreshAudioInputDevices(EasySession.Client);
            _audio.RefreshAudioOutputDevices(EasySession.Client);
        }

        public void StartInjectingAudio()
        {
            _audio.StartAudioInjection($"{Directory.GetCurrentDirectory()}/pathToFile", EasySession.LoginSessions["userName"]);
        }

        public void StopInjectingAudio()
        {
            _audio.StopAudioInjection(EasySession.LoginSessions["userName"]);
        }
    }
}
