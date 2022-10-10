using System;
using System.ComponentModel;
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


        public void OnChannelTextPropertyChanged(object sender, PropertyChangedEventArgs propArgs)
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
            }
        }



        #endregion
    }
}