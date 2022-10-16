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
            else
            {
                Unsubscribe(channelSession);
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
                        EasyEventsStatic.OnTextChannelConnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Connected:
                        EasyEventsStatic.OnTextChannelConnected(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnecting:
                        EasyEventsStatic.OnTextChannelDisconnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnected:
                        EasyEventsStatic.OnTextChannelDisconnected(senderIChannelSession);
                        break;
                }
                if (EasySessionStatic.UseDynamicEvents)
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
                    await EasyEventsAsyncStatic.OnTextChannelConnectingAsync(channelSession);
                    break;

                case ConnectionState.Connected:
                    await EasyEventsAsyncStatic.OnTextChannelConnectedAsync(channelSession);
                    break;

                case ConnectionState.Disconnecting:
                    await EasyEventsAsyncStatic.OnTextChannelDisconnectingAsync(channelSession);
                    break;

                case ConnectionState.Disconnected:
                    await EasyEventsAsyncStatic.OnTextChannelDisconnectedAsync(channelSession);
                    break;
            }
        }


        #endregion
    }
}