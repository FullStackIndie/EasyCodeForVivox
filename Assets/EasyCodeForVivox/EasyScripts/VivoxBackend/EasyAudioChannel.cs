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
            else
            {
                Unsubscribe(channelSession);
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
                        EasyEventsStatic.OnAudioChannelConnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Connected:
                        EasyEventsStatic.OnAudioChannelConnected(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnecting:
                        EasyEventsStatic.OnAudioChannelDisconnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnected:
                        EasyEventsStatic.OnAudioChannelDisconnected(senderIChannelSession);
                        break;
                }
                if (EasySessionStatic.UseDynamicEvents)
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
                    await EasyEventsAsyncStatic.OnAudioChannelConnectingAsync(channelSession);
                    break;

                case ConnectionState.Connected:
                   await  EasyEventsAsyncStatic.OnAudioChannelConnectedAsync(channelSession);
                    break;

                case ConnectionState.Disconnecting:
                    await EasyEventsAsyncStatic.OnAudioChannelDisconnectingAsync(channelSession);
                    break;

                case ConnectionState.Disconnected:
                    await EasyEventsAsyncStatic.OnAudioChannelDisconnectedAsync(channelSession);
                    break;
            }
        }


        #endregion
    }
}