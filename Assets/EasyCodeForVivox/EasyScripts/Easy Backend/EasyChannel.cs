using System;
using System.ComponentModel;
using UnityEngine;
using VivoxUnity;
using VivoxAccessToken;

namespace EasyCodeForVivox
{
    public class EasyChannel
    {
        public static event Action<IChannelSession> ChannelConnecting;
        public static event Action<IChannelSession> ChannelConnected;
        public static event Action<IChannelSession> ChannelDisconnecting;
        public static event Action<IChannelSession> ChannelDisconnected;


        public void Subscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged += OnChannelStatePropertyChanged;
        }

        public void Unsubscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged -= OnChannelStatePropertyChanged;
        }



        #region Channel Event Methods


        private void OnChannelConnecting(IChannelSession channelSession)
        {
            ChannelConnecting?.Invoke(channelSession);
        }

        private void OnChannelConnected(IChannelSession channelSession)
        {
            ChannelConnected?.Invoke(channelSession);
        }

        private void OnChannelDisconnecting(IChannelSession channelSession)
        {
            ChannelDisconnecting?.Invoke(channelSession);
        }

        private void OnChannelDisconnected(IChannelSession channelSession)
        {
            ChannelDisconnected?.Invoke(channelSession);

            Unsubscribe(channelSession);
        }


        #endregion



        #region Channel Methods


        public void JoinChannel(bool includeVoice, bool includeText, bool switchTransmissionToThisChannel,
           IChannelSession channelSession, bool joinMuted = false)
        {
            Subscribe(channelSession);
            var accessToken = GetChannelToken(channelSession, joinMuted);
            channelSession.BeginConnect(includeVoice, includeText, switchTransmissionToThisChannel, accessToken, ar =>
            {
                try
                {
                    channelSession.EndConnect(ar);
                }
                catch (Exception e)
                {
                    Unsubscribe(channelSession);
                    Debug.Log(e.StackTrace);
                }
            });
        }

        public void LeaveChannel(ILoginSession loginSession, IChannelSession channelToRemove)
        {
            if (channelToRemove != null)
            {
                channelToRemove.Disconnect();

                loginSession.DeleteChannelSession(channelToRemove.Key);
            }
        }

        public string GetChannelToken(IChannelSession channelSession, bool joinMuted = false, Channel3DProperties channel3DProperties = null)
        {
            var accessToken = "Error : Easy token invalid";
            var vivoxAction = "join";
            if (joinMuted)
            {
                vivoxAction = "join_muted";
            }
            switch (channelSession.Channel.Type)
            {
                case ChannelType.NonPositional:
                    accessToken = AccessToken.Token_f(EasySession.SecretKey, EasySession.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySession.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySession.mainLoginSession), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySession.Issuer, channelSession.Channel.Name,
                        EasySession.Domain));
                    break;
                case ChannelType.Echo:
                    accessToken = AccessToken.Token_f(EasySession.SecretKey, EasySession.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySession.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySession.mainLoginSession), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySession.Issuer, channelSession.Channel.Name,
                        EasySession.Domain));
                    break;
                case ChannelType.Positional:
                    accessToken = AccessToken.Token_f(EasySession.SecretKey, EasySession.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySession.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySession.mainLoginSession), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySession.Issuer, channelSession.Channel.Name,
                        EasySession.Domain, channel3DProperties ?? new Channel3DProperties()));
                    break;
            }
            return accessToken;
        }

        public string GetMatchChannelToken(IChannelSession channelSession, string matchRegion, string matchHash, bool joinMuted = false, Channel3DProperties channel3DProperties = null)
        {
            var accessToken = "Error : Easy token invalid";
            var vivoxAction = "join";
            if (joinMuted)
            {
                vivoxAction = "join_muted";
            }
            switch (channelSession.Channel.Type)
            {
                case ChannelType.NonPositional:
                    accessToken = AccessToken.Token_f(EasySession.SecretKey, EasySession.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySession.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySession.mainLoginSession), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySession.Issuer, channelSession.Channel.Name,
                        EasySession.Domain, matchRegion, matchHash));
                    break;
                case ChannelType.Echo:
                    accessToken = AccessToken.Token_f(EasySession.SecretKey, EasySession.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySession.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySession.mainLoginSession), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySession.Issuer, channelSession.Channel.Name,
                        EasySession.Domain, matchRegion, matchHash));
                    break;
                case ChannelType.Positional:
                    accessToken = AccessToken.Token_f(EasySession.SecretKey, EasySession.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySession.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySession.mainLoginSession), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySession.Issuer, channelSession.Channel.Name,
                        EasySession.Domain, matchRegion, matchHash, channel3DProperties));
                    break;
            }
            return accessToken;
        }

        #endregion



        #region Channel Callbacks


        private void OnChannelStatePropertyChanged(object sender, PropertyChangedEventArgs channelArgs)
        {
            var senderIChannelSession = (IChannelSession)sender;

            if (channelArgs.PropertyName == "ChannelState")
            {
                switch (senderIChannelSession.ChannelState)
                {
                    case ConnectionState.Connecting:
                        OnChannelConnecting(senderIChannelSession);
                        break;
                    case ConnectionState.Connected:
                        OnChannelConnected(senderIChannelSession);
                        break;
                    case ConnectionState.Disconnecting:
                        OnChannelDisconnecting(senderIChannelSession);
                        break;
                    case ConnectionState.Disconnected:
                        OnChannelDisconnected(senderIChannelSession);
                        break;
                }
            }
        }


        #endregion

    }
}