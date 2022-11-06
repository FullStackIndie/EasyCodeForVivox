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
        private readonly EasyEvents _events;
        private readonly EasyEventsAsync _eventsAsync;

        public EasyTextChannel(EasyEventsAsync eventsAsync, EasyEvents events)
        {
            _eventsAsync = eventsAsync;
            _events = events;
        }

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

        public void ToggleTextChannelActive<T>(IChannelSession channelSession, bool join, T eventParameter)
        {
            if (join)
            {
                Subscribe(channelSession);
            }
            else
            {
                Unsubscribe(channelSession);
            }

            channelSession.BeginSetTextConnected(join, async ar =>
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
                finally
                {
                    await HandleDynamicAsyncEvents(channelSession, eventParameter);
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
                        _events.OnTextChannelConnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Connected:
                        _events.OnTextChannelConnected(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnecting:
                        _events.OnTextChannelDisconnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnected:
                        _events.OnTextChannelDisconnected(senderIChannelSession);
                        break;
                }
                await HandleDynamicAsyncEvents(propArgs, senderIChannelSession);
            }
        }


        private async Task HandleDynamicAsyncEvents(PropertyChangedEventArgs propArgs, IChannelSession channelSession)
        {
            switch (channelSession.TextState)
            {
                case ConnectionState.Connecting:
                    await _eventsAsync.OnTextChannelConnectingAsync(channelSession);
                    break;

                case ConnectionState.Connected:
                    await _eventsAsync.OnTextChannelConnectedAsync(channelSession);
                    break;

                case ConnectionState.Disconnecting:
                    await _eventsAsync.OnTextChannelDisconnectingAsync(channelSession);
                    break;

                case ConnectionState.Disconnected:
                    await _eventsAsync.OnTextChannelDisconnectedAsync(channelSession);
                    break;
            }
        }


        private async Task HandleDynamicAsyncEvents<T>(IChannelSession channelSession, T value)
        {
            switch (channelSession.TextState)
            {
                case ConnectionState.Connecting:
                    _events.OnTextChannelConnecting(channelSession, value);
                    await _eventsAsync.OnTextChannelConnectingAsync(channelSession);
                    break;

                case ConnectionState.Connected:
                    _events.OnTextChannelConnected(channelSession, value);
                    await _eventsAsync.OnTextChannelConnectedAsync(channelSession);
                    break;

                case ConnectionState.Disconnecting:
                    _events.OnTextChannelDisconnecting(channelSession, value);
                    await _eventsAsync.OnTextChannelDisconnectingAsync(channelSession);
                    break;

                case ConnectionState.Disconnected:
                    _events.OnTextChannelDisconnected(channelSession, value);
                    await _eventsAsync.OnTextChannelDisconnectedAsync(channelSession);
                    break;
            }
        }


        #endregion
    }
}