using System;
using System.Linq;
using UnityEngine;
using VivoxUnity;


namespace EasyCodeForVivox
{

    public class EasyAudio : IAudio
    {
        private readonly EasySettings _settings;

        public EasyAudio(EasySettings settings)
        {
            _settings = settings;
        }


        public void Subscribe(VivoxUnity.Client client)
        {
            client.AudioInputDevices.AvailableDevices.AfterKeyAdded += OnAudioInputDeviceAdded;
            client.AudioInputDevices.AvailableDevices.BeforeKeyRemoved += OnAudioInputDeviceRemoved;
            client.AudioInputDevices.AvailableDevices.AfterValueUpdated += OnAudioInputDeviceUpdated;
            
            client.AudioOutputDevices.AvailableDevices.AfterKeyAdded += OnAudioOutputDeviceAdded;
            client.AudioOutputDevices.AvailableDevices.BeforeKeyRemoved += OnAudioOutputDeviceRemoved;
            client.AudioOutputDevices.AvailableDevices.AfterValueUpdated += OnAudioOutputDeviceUpdated;
        }

        public void Unsubscribe(VivoxUnity.Client client)
        {
            client.AudioInputDevices.AvailableDevices.AfterKeyAdded -= OnAudioInputDeviceAdded;
            client.AudioInputDevices.AvailableDevices.BeforeKeyRemoved -= OnAudioInputDeviceRemoved;
            client.AudioInputDevices.AvailableDevices.AfterValueUpdated -= OnAudioInputDeviceUpdated;
            
            client.AudioOutputDevices.AvailableDevices.AfterKeyAdded -= OnAudioOutputDeviceAdded;
            client.AudioOutputDevices.AvailableDevices.BeforeKeyRemoved -= OnAudioOutputDeviceRemoved;
            client.AudioOutputDevices.AvailableDevices.AfterValueUpdated -= OnAudioOutputDeviceUpdated;
        }

        #region Audio Methods

        public void SetAudioDeviceInput(IAudioDevice device, VivoxUnity.Client client)
        {
            if (device == client.AudioInputDevices.ActiveDevice)
            {
                Debug.Log($"{device.Name} is already active".Color(EasyDebug.Yellow));
                return;
            }
            client.AudioInputDevices.BeginSetActiveDevice(device, ar =>
            {
                try
                {
                    if (ar.IsCompleted)
                    {
                        Debug.Log($"Selected Audio Input Device [ {device.Name} ] should be enabled");
                        client.AudioInputDevices.EndSetActiveDevice(ar);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }

        public void SetAudioDeviceOutput(IAudioDevice device, VivoxUnity.Client client)
        {
            if (device == client.AudioOutputDevices.ActiveDevice)
            {
                Debug.Log($"{device.Name} is already active".Color(EasyDebug.Yellow));
                return;
            }
            client.AudioOutputDevices.BeginSetActiveDevice(device, ar =>
            {
                try
                {
                    if (ar.IsCompleted)
                    {
                        Debug.Log($"Selected Audio Output Device [ {device.Name} ]should be enabled");
                        client.AudioOutputDevices.EndSetActiveDevice(ar);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }


        public void AdjustLocalPlayerAudioVolume(int value, VivoxUnity.Client client)
        {
            client.AudioOutputDevices.BeginRefresh(ar =>
            {
                try
                {
                    client.AudioOutputDevices.VolumeAdjustment = value;
                    client.AudioOutputDevices.EndRefresh(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }

        public void AdjustRemotePlayerAudioVolume(string userName, IChannelSession channelSession, float value)
        {
            Debug.Log(channelSession.Parent.Key.Issuer);
            Debug.Log(userName);
            Debug.Log(channelSession.Parent.Key.Domain);
            var userVolumeToUpdate = EasySIP.GetUserSIP(channelSession.Parent.Key.Issuer, userName, channelSession.Parent.Key.Domain);
            Debug.Log(userVolumeToUpdate);
            channelSession.Participants[userVolumeToUpdate].LocalVolumeAdjustment = Mathf.RoundToInt(value);
        }

        public void StartAudioInjection(string wavToInject, ILoginSession loginSession)
        {
            loginSession.StartAudioInjection(wavToInject);
        }

        public void StopAudioInjection(ILoginSession loginSession)
        {
            loginSession.StopAudioInjection();
        }


        public void RefreshAudioDevices(VivoxUnity.Client client)
        {
            client.AudioOutputDevices.BeginRefresh(ar =>
            {
                try
                {
                    client.AudioOutputDevices.EndRefresh(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }
        #endregion


        public void OnAudioInputDeviceAdded(object sender, KeyEventArg<string> keyArgs) 
        {
            var device = EasySession.Client.AudioInputDevices.AvailableDevices.Where(d => d.Key == keyArgs.Key).FirstOrDefault();
            if (!_settings.LogAllAudioDevices) { return; }
            Debug.Log($"Audio Input device has been added {device?.Name}");
        }

        public void OnAudioInputDeviceRemoved(object sender, KeyEventArg<string> keyArgs) 
        {
            var device = EasySession.Client.AudioInputDevices.AvailableDevices.Where(d => d.Key == keyArgs.Key).FirstOrDefault();
            if (!_settings.LogAllAudioDevices) { return; }
            Debug.Log($"Audio Input device has been removed {device?.Name}");
        }

        public void OnAudioOutputDeviceAdded(object sender, KeyEventArg<string> keyArgs) 
        {
            var device = EasySession.Client.AudioOutputDevices.AvailableDevices.Where(d => d.Key == keyArgs.Key).FirstOrDefault();
            if (!_settings.LogAllAudioDevices) { return; }
            Debug.Log($"Audio Output device has been added {device?.Name}");
        }

        public void OnAudioOutputDeviceRemoved(object sender, KeyEventArg<string> keyArgs) 
        {
            var device = EasySession.Client.AudioOutputDevices.AvailableDevices.Where(d => d.Key == keyArgs.Key).FirstOrDefault();
            if (!_settings.LogAllAudioDevices) { return; }
            Debug.Log($"Audio Output device has been removed {device?.Name}");
        }

        public void OnAudioInputDeviceUpdated(object sender, ValueEventArg<string, IAudioDevice> valueArgs) 
        {
            switch (valueArgs.PropertyName)
            {
                case "EventAfterDeviceAvailableAdded":
                    Debug.Log($"Audio Input Device Added {valueArgs.Value.Name}");
                    break;
                case "EventBeforeAvailableDeviceRemoved":
                    Debug.Log($"Audio Input Device Removed {valueArgs.Value.Name}");
                    break;
                case "EventEffectiveDeviceChanged":
                    Debug.Log($"Audio Input Device has been changed to {valueArgs.Value.Name}");
                    break;
            }
        }

        public void OnAudioOutputDeviceUpdated(object sender, ValueEventArg<string, IAudioDevice> valueArgs) // todo investigate more cuz this Doesnt really work
        {
            switch (valueArgs.PropertyName)
            {
                case "EventAfterDeviceAvailableAdded":
                    Debug.Log($"Audio Output Device Added {valueArgs.Value.Name}");
                    break;
                case "EventBeforeAvailableDeviceRemoved":
                    Debug.Log($"Audio Output Device Removed {valueArgs.Value.Name}");
                    break;
                case "EventEffectiveDeviceChanged":
                    Debug.Log($"Audio Output Device has been changed to {valueArgs.Value.Name}");
                    break;
            }
        }

    }
}