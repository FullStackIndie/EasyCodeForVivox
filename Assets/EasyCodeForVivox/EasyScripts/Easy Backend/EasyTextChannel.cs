using System;
using System.ComponentModel;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyTextChannel
    {

        public static event Action<IChannelSession> TextChannelConnecting;
        public static event Action<IChannelSession> TextChannelConnected;
        public static event Action<IChannelSession> TextChannelDisconnecting;
        public static event Action<IChannelSession> TextChannelDisconnected;


        public void Subscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged += OnChannelTextPropertyChanged;
        }

        public void Unsubscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged -= OnChannelTextPropertyChanged;
        }



        #region Text Channel Events


        private void OnTextChannelConnecting(IChannelSession channelSession)
        {
            if (channelSession != null)
            {
                TextChannelConnecting?.Invoke(channelSession);
            }
        }

        private void OnTextChannelConnected(IChannelSession channelSession)
        {
            if (channelSession != null)
            {
                TextChannelConnected?.Invoke(channelSession);
            }
        }

        private void OnTextChannelDisconnecting(IChannelSession channelSession)
        {
            if (channelSession != null)
            {
                TextChannelDisconnecting?.Invoke(channelSession);
            }
        }

        private void OnTextChannelDisconnected(IChannelSession channelSession)
        {
            if (channelSession != null)
            {
                TextChannelDisconnected?.Invoke(channelSession);

                Unsubscribe(channelSession);
            }
        }


        #endregion



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

            if (!join)
            {
                Unsubscribe(channelSession);
            }
        }


        #endregion



        #region Channel - Text Callbacks


        private void OnChannelTextPropertyChanged(object sender, PropertyChangedEventArgs propArgs)
        {
            var senderIChannelSession = (IChannelSession)sender;

            if (propArgs.PropertyName == "TextState")
            {
                switch (senderIChannelSession.TextState)
                {
                    case ConnectionState.Connecting:
                        OnTextChannelConnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Connected:
                        OnTextChannelConnected(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnecting:
                        OnTextChannelDisconnecting(senderIChannelSession);
                        break;

                    case ConnectionState.Disconnected:
                        OnTextChannelDisconnected(senderIChannelSession);
                        break;
                }
            }
        }



        #endregion
    }
}