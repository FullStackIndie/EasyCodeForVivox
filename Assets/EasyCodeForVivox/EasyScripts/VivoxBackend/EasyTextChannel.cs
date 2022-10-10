using EasyCodeForVivox.Events;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyTextChannel : ITextChannel
    {

        public void Subscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged += OnChannelTextPropertyChanged;
        }

        public void Unsubscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged -= OnChannelTextPropertyChanged;
        }



        #region Channel - Text Methods


        public void ToggleTextChannelActive(IChannelSession channelSession, bool join)
        {
            if (join)
            {
                Subscribe(channelSession);
            }

            channelSession.BeginSetTextConnected(join, ar =>
            {
                try
                {
                    channelSession.EndSetTextConnected(ar);
                }
                catch (Exception e)
                {
                    Unsubscribe(channelSession);
                    Debug.Log(e.Message);
                }
            });
        }


        #endregion



        #region Channel - Text Callbacks


        public async void OnChannelTextPropertyChanged(object sender, PropertyChangedEventArgs propArgs)
        {
            var senderIChannelSession = (IChannelSession)sender;

            if (propArgs.PropertyName == "TextState")
            {
                switch (senderIChannelSession.TextState)
                {
                    case ConnectionState.Connecting:
                        EasyEvents.OnTextChannelConnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Connected:
                        EasyEvents.OnTextChannelConnected(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnecting:
                        EasyEvents.OnTextChannelDisconnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnected:
                        EasyEvents.OnTextChannelDisconnected(senderIChannelSession);
                        Unsubscribe(senderIChannelSession);
                        break;
                }
                if (EasySession.UseDynamicEvents)
                {
                    await HandleDynamicAsyncEvents(propArgs, senderIChannelSession);
                }
            }
        }


        private async Task HandleDynamicAsyncEvents(PropertyChangedEventArgs propArgs, IChannelSession channelSession)
        {
            switch (channelSession.TextState)
            {
                case ConnectionState.Connecting:
                    await EasyEventsAsync.OnTextChannelConnectingAsync(channelSession);
                    break;

                case ConnectionState.Connected:
                    await EasyEventsAsync.OnTextChannelConnectedAsync(channelSession);
                    break;

                case ConnectionState.Disconnecting:
                    await EasyEventsAsync.OnTextChannelDisconnectingAsync(channelSession);
                    break;

                case ConnectionState.Disconnected:
                    await EasyEventsAsync.OnTextChannelDisconnectedAsync(channelSession);
                    break;
            }
        }


        #endregion
    }
}