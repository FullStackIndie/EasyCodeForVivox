using EasyCodeForVivox.Events;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{

    public class EasyAudioChannel 
    {

        private readonly EasyEvents _events;
        private readonly EasyEventsAsync _eventsAsync;

        public EasyAudioChannel(EasyEvents events, EasyEventsAsync eventsAsync)
        {
            _events = events;
            _eventsAsync = eventsAsync;
        }

        public void Subscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged += OnChannelAudioPropertyChanged;
        }

        public void Unsubscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged -= OnChannelAudioPropertyChanged;
        }



        #region Channel - Voice Methods


        public void ToggleAudioInChannel(IChannelSession channelSession, bool join)
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

        public void ToggleAudioInChannel<T>(IChannelSession channelSession, bool join, T eventParameter)
        {
            if (join)
            {
                Subscribe(channelSession);
            }
            else
            {
                Unsubscribe(channelSession);
            }

            channelSession.BeginSetAudioConnected(join, true, async ar =>
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
                finally
                {
                    await HandleDynamicEventsAsync(channelSession, eventParameter);
                }
            });
        }


        #endregion



        #region Channel - Voice Callbacks


        private async void OnChannelAudioPropertyChanged(object sender, PropertyChangedEventArgs propArgs)
        {
            var senderIChannelSession = (IChannelSession)sender;

            if (propArgs.PropertyName == "AudioState")
            {
                switch (senderIChannelSession.AudioState)
                {
                    case ConnectionState.Connecting:
                        _events.OnAudioChannelConnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Connected:
                        _events.OnAudioChannelConnected(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnecting:
                        _events.OnAudioChannelDisconnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnected:
                        _events.OnAudioChannelDisconnected(senderIChannelSession);
                        break;
                }
                await HandleDynamicEventsAsync(propArgs, senderIChannelSession);
            }
        }

        private async Task HandleDynamicEventsAsync(PropertyChangedEventArgs propArgs, IChannelSession channelSession)
        {
            switch (channelSession.AudioState)
            {
                case ConnectionState.Connecting:
                    await _eventsAsync.OnAudioChannelConnectingAsync(channelSession);
                    break;

                case ConnectionState.Connected:
                    await _eventsAsync.OnAudioChannelConnectedAsync(channelSession);
                    break;

                case ConnectionState.Disconnecting:
                    await _eventsAsync.OnAudioChannelDisconnectingAsync(channelSession);
                    break;

                case ConnectionState.Disconnected:
                    await _eventsAsync.OnAudioChannelDisconnectedAsync(channelSession);
                    break;
            }
        }

        private async Task HandleDynamicEventsAsync<T>(IChannelSession channelSession, T value)
        {
            switch (channelSession.AudioState)
            {
                case ConnectionState.Connecting:
                    _events.OnAudioChannelConnecting(channelSession, value);
                    await _eventsAsync.OnAudioChannelConnectingAsync(channelSession);
                    break;

                case ConnectionState.Connected:
                    _events.OnAudioChannelConnected(channelSession, value);
                    await _eventsAsync.OnAudioChannelConnectedAsync(channelSession);
                    break;

                case ConnectionState.Disconnecting:
                    _events.OnAudioChannelDisconnecting(channelSession, value);
                    await _eventsAsync.OnAudioChannelDisconnectingAsync(channelSession);
                    break;

                case ConnectionState.Disconnected:
                    _events.OnAudioChannelDisconnected(channelSession, value);
                    await _eventsAsync.OnAudioChannelDisconnectedAsync(channelSession);
                    break;
            }
        }


        #endregion
    }
}