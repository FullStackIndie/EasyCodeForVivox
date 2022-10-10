using EasyCodeForVivox.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using VivoxAccessToken;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyChannel : IChannel
    {

        public void Subscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged += OnChannelStatePropertyChanged;
        }

        public void Unsubscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged -= OnChannelStatePropertyChanged;
        }



        #region Channel Methods




        public void JoinChannel(string userName, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel,
           IChannelSession channelSession, bool joinMuted = false)
        {
            Subscribe(channelSession);
            var accessToken = GetChannelToken(userName, channelSession, joinMuted);
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


        public IChannelSession GetExistingChannelSession(string userName, string channelName)
        {
            if (EasySession.ChannelSessions[channelName].ChannelState == ConnectionState.Disconnected || EasySession.ChannelSessions[channelName] == null)
            {
                EasySession.ChannelSessions[channelName] = EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelName)));
            }

            return EasySession.ChannelSessions[channelName];
        }

        public IChannelSession CreateNewChannel(string userName, string channelName, ChannelType channelType, Channel3DProperties channel3DProperties = default)
        {
            if (channelType == ChannelType.Positional)
            {
                foreach (KeyValuePair<string, IChannelSession> channel in EasySession.ChannelSessions)
                {
                    if (channel.Value.Channel.Type == ChannelType.Positional)
                    {
                        Debug.Log($"{channel.Value.Channel.Name} Is already a 3D Positional Channel. Can Only Have One 3D Positional Channel. Refer To Vivox Documentation :: Returning Exisiting 3D Channel : {channel.Value.Channel.Name}");
                        return channel.Value;
                    }
                }

                EasySession.ChannelSessions.Add(userName, EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelType, channelName, channel3DProperties))));
            }
            else
            {
                if (EasySession.ChannelSessions.ContainsKey(userName)) { return EasySession.ChannelSessions[userName]; }
                EasySession.ChannelSessions.Add(userName, EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelType, channelName))));
            }

            return EasySession.ChannelSessions[userName];
        }

        public string GetChannelSIP(ChannelType channelType, string channelName, Channel3DProperties channel3DProperties = default)
        {
            switch (channelType)
            {
                case ChannelType.NonPositional:
                    return EasySIP.GetChannelSIP(ChannelType.NonPositional, EasySession.Issuer, channelName, EasySession.Domain);

                case ChannelType.Echo:
                    return EasySIP.GetChannelSIP(ChannelType.NonPositional, EasySession.Issuer, channelName, EasySession.Domain);

                case ChannelType.Positional:
                    return EasySIP.GetChannelSIP(ChannelType.Positional, EasySession.Issuer, channelName, EasySession.Domain, channel3DProperties);

            }
            return EasySIP.GetChannelSIP(ChannelType.NonPositional, EasySession.Issuer, channelName, EasySession.Domain);
        }

        public string GetChannelSIP(string channelName)
        {
            string result = "";
            foreach (var session in EasySession.ChannelSessions)
            {
                if (session.Value.Channel.Name == channelName)
                {
                    result = GetChannelSIP(session.Value.Channel.Type, channelName);
                    return result;
                }
            }
            return result;
        }

        public void RemoveChannelSession(string channelName)
        {
            if (EasySession.ChannelSessions.ContainsKey(channelName))
            {
                EasySession.ChannelSessions.Remove(channelName);
            }
        }




        public string GetChannelToken(string userName, IChannelSession channelSession, bool joinMuted = false, Channel3DProperties channel3DProperties = default)
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
                        EasySIP.GetUserSIP(EasySession.LoginSessions[userName]), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySession.Issuer, channelSession.Channel.Name,
                        EasySession.Domain));
                    break;
                case ChannelType.Echo:
                    accessToken = AccessToken.Token_f(EasySession.SecretKey, EasySession.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySession.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySession.LoginSessions[userName]), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySession.Issuer, channelSession.Channel.Name,
                        EasySession.Domain));
                    break;
                case ChannelType.Positional:
                    accessToken = AccessToken.Token_f(EasySession.SecretKey, EasySession.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySession.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySession.LoginSessions[userName]), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySession.Issuer, channelSession.Channel.Name,
                        EasySession.Domain, channel3DProperties ?? new Channel3DProperties()));
                    break;
            }
            return accessToken;
        }

        public string GetMatchChannelToken(string userName, IChannelSession channelSession, string matchRegion, string matchHash, bool joinMuted = false, Channel3DProperties channel3DProperties = default)
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
                        EasySIP.GetUserSIP(EasySession.LoginSessions[userName]), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySession.Issuer, channelSession.Channel.Name,
                        EasySession.Domain, matchRegion, matchHash));
                    break;
                case ChannelType.Echo:
                    accessToken = AccessToken.Token_f(EasySession.SecretKey, EasySession.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySession.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySession.LoginSessions[userName]), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySession.Issuer, channelSession.Channel.Name,
                        EasySession.Domain, matchRegion, matchHash));
                    break;
                case ChannelType.Positional:
                    accessToken = AccessToken.Token_f(EasySession.SecretKey, EasySession.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySession.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySession.LoginSessions[userName]), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySession.Issuer, channelSession.Channel.Name,
                        EasySession.Domain, matchRegion, matchHash, channel3DProperties));
                    break;
            }
            return accessToken;
        }

        #endregion



        #region Channel Callbacks


        public void OnChannelStatePropertyChanged(object sender, PropertyChangedEventArgs channelArgs)
        {
            var senderIChannelSession = (IChannelSession)sender;

            if (channelArgs.PropertyName == "ChannelState")
            {
                switch (senderIChannelSession.ChannelState)
                {
                    case ConnectionState.Connecting:
                        EasyEvents.OnChannelConnecting(senderIChannelSession);
                        break;
                    case ConnectionState.Connected:
                        EasyEvents.OnChannelConnected(senderIChannelSession);
                        break;
                    case ConnectionState.Disconnecting:
                        EasyEvents.OnChannelDisconnecting(senderIChannelSession);
                        break;
                    case ConnectionState.Disconnected:
                        EasyEvents.OnChannelDisconnected(senderIChannelSession);
                        Unsubscribe(senderIChannelSession);
                        break;
                }
            }
        }


        #endregion

    }
}