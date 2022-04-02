using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyVoiceChannel
    {
        public static event Action<IChannelSession> VoiceChannelConnecting;
        public static event Action<IChannelSession> VoiceChannelConnected;
        public static event Action<IChannelSession> VoiceChannelDisconnecting;
        public static event Action<IChannelSession> VoiceChannelDisconnected;


        public void Subscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged += OnChannelAudioPropertyChanged;
        }

        public void Unsubscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged -= OnChannelAudioPropertyChanged;
        }



        #region Voice Channel Events


        private void OnAudioChannelConnecting(IChannelSession channelSession)
        {
            if (channelSession != null)
            {
                VoiceChannelConnecting?.Invoke(channelSession);
            }
        }

        private void OnAudioChannelConnected(IChannelSession channelSession)
        {
            if (channelSession != null)
            {
                VoiceChannelConnected?.Invoke(channelSession);
            }
        }

        private void OnAudioChannelDisconnecting(IChannelSession channelSession)
        {
            if (channelSession != null)
            {
                VoiceChannelDisconnecting?.Invoke(channelSession);
            }
        }

        private void OnAudioChannelDisconnected(IChannelSession channelSession)
        {
            if (channelSession != null)
            {
                VoiceChannelDisconnected?.Invoke(channelSession);

                Unsubscribe(channelSession);
            }
        }


        #endregion



        #region Channel - Voice Methods


        public void ToggleAudioChannelActive(IChannelSession channelSession, bool join)
        {
            if (join)
            {
                Subscribe(channelSession);
            }

            channelSession.BeginSetAudioConnected(join, true, ar =>
            {
                try
                {
                    channelSession.EndSetAudioConnected(ar);
                }
                catch (Exception e)
                {
                    Unsubscribe(channelSession);
                    Debug.Log(e.Message);
                }
            });

            if (!join)
            {
                Unsubscribe(channelSession);
            }
        }


        #endregion



        #region Channel - Voice Callbacks


        private void OnChannelAudioPropertyChanged(object sender, PropertyChangedEventArgs propArgs)
        {
            var senderIChannelSession = (IChannelSession)sender;

            if (propArgs.PropertyName == "AudioState")
            {
                switch (senderIChannelSession.AudioState)
                {
                    case ConnectionState.Connecting:
                        OnAudioChannelConnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Connected:
                        OnAudioChannelConnected(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnecting:
                        OnAudioChannelDisconnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnected:
                        Unsubscribe(senderIChannelSession);
                        OnAudioChannelDisconnected(senderIChannelSession);
                        break;
                }
            }
        }


        #endregion
    }
}