﻿using EasyCodeForVivox.Events;
using EasyCodeForVivox.Events.Internal;
using EasyCodeForVivox.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VivoxUnity;


namespace EasyCodeForVivox
{

    public class EasyAudio
    {
        private readonly EasySettingsSO _settings;
        private readonly EasyEvents _events;
        private readonly EasyEventsAsync _eventsAsync;

        public EasyAudio(EasySettingsSO settings, EasyEvents events,
            EasyEventsAsync eventsAsync)
        {
            _settings = settings;
            _events = events;
            _eventsAsync = eventsAsync;
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

        public IEnumerable<IAudioDevice> GetAudioInputDevices(VivoxUnity.Client client)
        {
            return client.AudioInputDevices.AvailableDevices;
        }

        public IEnumerable<IAudioDevice> GetAudioOutputDevices(VivoxUnity.Client client)
        {
            return client.AudioOutputDevices.AvailableDevices;
        }

        public void SetAudioInputDevice(string deviceName, VivoxUnity.Client client)
        {
            IAudioDevice device = client.AudioInputDevices.AvailableDevices[deviceName];
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

        public void SetAudioOutputDevice(string deviceName, VivoxUnity.Client client)
        {
            IAudioDevice device = client.AudioInputDevices.AvailableDevices[deviceName];
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


        public void RefreshAudioInputDevices(VivoxUnity.Client client)
        {
            client.AudioInputDevices.BeginRefresh(ar =>
            {
                try
                {
                    client.AudioInputDevices.EndRefresh(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }

        public void RefreshAudioOutputDevices(VivoxUnity.Client client)
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

        public void SetAutoVoiceActivityDetection(string userName)
        {
            var request = new vx_req_aux_set_vad_properties_t();
            request.account_handle = userName;
            request.vad_auto = 1; // 1 = true
            VxClient.Instance.BeginIssueRequest(request, ar =>
            {
                try
                {
                    if (ar.IsCompleted)
                    {
                        if (_settings.LogVoiceActivityDetection)
                            Debug.Log($"Successfully set Auto Voice Activity Detection (VAD) for logged in player {userName}");
                    }
                }
                catch (Exception e)
                {
                    Debug.Log($"Error Setting Auto Voice Activity Detection (VAD) for logged in player {userName}");
                    Debug.LogException(e);
                }
            });
        }

        public void SetVoiceActivityDetection(string userName, int hangover = 2000, int sensitivity = 43, int noiseFloor = 576)
        {
            // https://support.unity.com/hc/en-us/articles/4418142182804-Vivox-How-to-Access-VAD-settings#h_4650a9f8-6c2b-4e31-b0c9-7d0a90509378
            var request = new vx_req_aux_set_vad_properties_t();
            request.account_handle = userName;
            request.vad_hangover = hangover; // milliseconds to switch back to silence after player stopped talking
            request.vad_sensitivity = sensitivity;
            request.vad_noise_floor = noiseFloor;
            request.vad_auto = 0; // 0 = false
            VxClient.Instance.BeginIssueRequest(request, ar =>
            {
                try
                {
                    if (ar.IsCompleted)
                    {
                        if (_settings.LogVoiceActivityDetection)
                            Debug.Log($"Successfully set Voice Activity Detection (VAD) for logged in player {userName}");
                    }
                }
                catch (Exception e)
                {
                    Debug.Log($"Error Setting  Voice Activity Detection (VAD) for logged in player {userName}");
                    Debug.LogException(e);
                }
            });
        }

        #endregion


        private void OnAudioInputDeviceAdded(object sender, KeyEventArg<string> keyArgs)
        {
            var source = (VivoxUnity.IReadOnlyDictionary<string, IAudioDevice>)sender;
            var device = EasySession.Client.AudioInputDevices.AvailableDevices.Where(d => d.Key == keyArgs.Key).FirstOrDefault();
            _events.OnAudioInputDeviceAdded(source[keyArgs.Key]);
            _eventsAsync.OnAudioInputDeviceAddedAsync(device);
        }

        private void OnAudioInputDeviceRemoved(object sender, KeyEventArg<string> keyArgs)
        {
            var device = EasySession.Client.AudioInputDevices.AvailableDevices.Where(d => d.Key == keyArgs.Key).FirstOrDefault();
            _events.OnAudioInputDeviceRemoved(device);
            _eventsAsync.OnAudioInputDeviceRemovedAsync(device);
        }

        private void OnAudioInputDeviceUpdated(object sender, ValueEventArg<string, IAudioDevice> valueArgs)
        {
            // todo test if I need the other audio device events or if these events are for 1 device only
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
            _events.OnAudioInputDeviceUpdated(valueArgs?.Value);
            _eventsAsync.OnAudioInputDeviceUpdatedAsync(valueArgs?.Value);
        }

        private void OnAudioOutputDeviceAdded(object sender, KeyEventArg<string> keyArgs)
        {
            var device = EasySession.Client.AudioOutputDevices.AvailableDevices.Where(d => d.Key == keyArgs.Key).FirstOrDefault();
            _events.OnAudioOutputDeviceAdded(device);
            _eventsAsync.OnAudioOutputDeviceAdded(device);
        }

        private void OnAudioOutputDeviceRemoved(object sender, KeyEventArg<string> keyArgs)
        {
            var device = EasySession.Client.AudioOutputDevices.AvailableDevices.Where(d => d.Key == keyArgs.Key).FirstOrDefault();
            _events.OnAudioOutputDeviceRemoved(device);
            _eventsAsync.OnAudioOutputDeviceRemoved(device);
        }

        private void OnAudioOutputDeviceUpdated(object sender, ValueEventArg<string, IAudioDevice> valueArgs)
        {
            // todo test if I need the other audio device events or if these events are for 1 device only
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
            _events.OnAudioOutputDeviceUpdated(valueArgs?.Value);
            _eventsAsync.OnAudioOutputDeviceUpdated(valueArgs?.Value);
        }

    }
}