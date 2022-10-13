using EasyCodeForVivox.Events;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{

    public class EasyAudioChannel : IAudioChannel
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


        public async void OnChannelAudioPropertyChanged(object sender, PropertyChangedEventArgs propArgs)
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
                if (EasySession.UseDynamicEvents)
                {
                    await HandleDynamicEventsAsync(propArgs, senderIChannelSession);
                }
            }
        }

        private async Task HandleDynamicEventsAsync(PropertyChangedEventArgs propArgs, IChannelSession channelSession)
        {
            switch (channelSession.AudioState)
            {
                case ConnectionState.Connecting:
                    await EasyEventsAsync.OnAudioChannelConnectingAsync(channelSession);
                    break;

                case ConnectionState.Connected:
                   await  EasyEventsAsync.OnAudioChannelConnectedAsync(channelSession);
                    break;

                case ConnectionState.Disconnecting:
                    await EasyEventsAsync.OnAudioChannelDisconnectingAsync(channelSession);
                    break;

                case ConnectionState.Disconnected:
                    await EasyEventsAsync.OnAudioChannelDisconnectedAsync(channelSession);
                    break;
            }
        }


        #endregion
    }
}