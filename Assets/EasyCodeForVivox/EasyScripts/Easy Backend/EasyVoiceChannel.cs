using System;
using System.ComponentModel;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyVoiceChannel
    {
        public void Subscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged += OnChannelAudioPropertyChanged;
        }

        public void Unsubscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged -= OnChannelAudioPropertyChanged;
        }



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
                        EasyEvents.OnAudioChannelConnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Connected:
                        EasyEvents.OnAudioChannelConnected(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnecting:
                        EasyEvents.OnAudioChannelDisconnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnected:
                        EasyEvents.OnAudioChannelDisconnected(senderIChannelSession);
                        Unsubscribe(senderIChannelSession);
                        break;
                }
            }
        }


        #endregion
    }
}