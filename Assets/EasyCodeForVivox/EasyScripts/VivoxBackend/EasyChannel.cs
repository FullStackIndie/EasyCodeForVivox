using EasyCodeForVivox.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using UnityEngine;
using VivoxAccessToken;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyChannel : IChannel
    {
        private readonly EasyTextChannel _textChannel;
        private readonly EasyAudioChannel _audioChannel;
        private readonly EasyUsers _users;
        private readonly EasyMessages _messages;

        public EasyChannel(EasyUsers users, EasyMessages messages,
            EasyAudioChannel audioChannel, EasyTextChannel textChannel)
        {
            _users = users;
            _messages = messages;
            _audioChannel = audioChannel;
            _textChannel = textChannel;
        }

        public void Subscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged += OnChannelStatePropertyChanged;
        }

        public void Unsubscribe(IChannelSession channelSession)
        {
            channelSession.PropertyChanged -= OnChannelStatePropertyChanged;
        }



        #region Channel Methods


        public void JoinChannel(string userName, string channelName, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel, ChannelType channelType,
    bool joinMuted = false, Channel3DProperties channel3DProperties = default)
        {
            if (!EasyVivoxHelpers.FilterChannelAndUserName(channelName)) { return; }
            IChannelSession channelSession = CreateNewChannel(userName, channelName, channelType, channel3DProperties);

            try
            {
                _textChannel.Subscribe(channelSession);
                _audioChannel.Subscribe(channelSession);
                _users.SubscribeToParticipantEvents(channelSession);
                _messages.SubscribeToChannelMessages(channelSession);

                JoinChannel(userName, includeVoice, includeText, switchTransmissionToThisChannel, channelSession, joinMuted);
            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
                _textChannel.Unsubscribe(channelSession);
                _audioChannel.Unsubscribe(channelSession);
                _users.UnsubscribeFromParticipantEvents(channelSession);
                _messages.UnsubscribeFromChannelMessages(channelSession);
            }
        }



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

        public void LeaveChannel(string channelName, string userName)
        {
            if (EasySessionStatic.ChannelSessions.ContainsKey(channelName))
            {
                if (EasySessionStatic.LoginSessions.ContainsKey(userName))
                {
                    _users.UnsubscribeFromParticipantEvents(EasySessionStatic.ChannelSessions[channelName]);
                    _messages.UnsubscribeFromChannelMessages(EasySessionStatic.ChannelSessions[channelName]);
                    LeaveChannel(EasySessionStatic.LoginSessions[userName], EasySessionStatic.ChannelSessions[channelName]);
                }
                else
                {
                    Debug.Log($"User Login Session does not exist");
                }
            }
            else
            {
                Debug.Log($"User Channel Session does not exist");
            }
        }

        public void LeaveChannel(ILoginSession loginSession, IChannelSession channelToRemove)
        {
            if (channelToRemove != null)
            {
                channelToRemove.Disconnect();

                loginSession.DeleteChannelSession(channelToRemove.Key);
                Unsubscribe(channelToRemove);
                EasySessionStatic.ChannelSessions.Remove(channelToRemove.Key.Name);
            }
        }


        public IChannelSession GetExistingChannelSession(string userName, string channelName)
        {
            if (EasySessionStatic.ChannelSessions.ContainsKey(channelName))
            {
                if (EasySessionStatic.ChannelSessions[channelName].ChannelState == ConnectionState.Disconnected || EasySessionStatic.ChannelSessions[channelName] == null)
                {
                    EasySessionStatic.ChannelSessions[channelName] = EasySessionStatic.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelName)));
                }
            }
            else
            {
                return null;
            }

            return EasySessionStatic.ChannelSessions[channelName];
        }

        public IChannelSession CreateNewChannel(string userName, string channelName, ChannelType channelType, Channel3DProperties channel3DProperties = default)
        {
            if (channelType == ChannelType.Positional)
            {
                var positionalChannel = EasySessionStatic.ChannelSessions.Where(c => c.Value.Channel.Type == ChannelType.Positional).Select(c => c.Value).FirstOrDefault();
                if (positionalChannel != null)
                {
                    Debug.Log($"{positionalChannel.Channel.Name} Is already a 3D Positional Channel. Can Only Have One 3D Positional Channel. " +
                        $"Refer To Vivox Documentation :: Returning Exisiting 3D Channel : {positionalChannel.Channel.Name}".Color(EasyDebug.Yellow));
                    return positionalChannel;
                }
                EasySessionStatic.ChannelSessions.Add(channelName, EasySessionStatic.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelType, channelName, channel3DProperties))));
            }
            else
            {
                if (EasySessionStatic.ChannelSessions.ContainsKey(channelName))
                {
                    return EasySessionStatic.ChannelSessions[channelName];
                }
                EasySessionStatic.ChannelSessions.Add(channelName, EasySessionStatic.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelType, channelName))));
            }

            return EasySessionStatic.ChannelSessions[channelName];
        }

        public string GetChannelSIP(ChannelType channelType, string channelName, Channel3DProperties channel3DProperties = default)
        {
            switch (channelType)
            {
                case ChannelType.NonPositional:
                    return EasySIP.GetChannelSIP(ChannelType.NonPositional, EasySessionStatic.Issuer, channelName, EasySessionStatic.Domain);

                case ChannelType.Echo:
                    return EasySIP.GetChannelSIP(ChannelType.NonPositional, EasySessionStatic.Issuer, channelName, EasySessionStatic.Domain);

                case ChannelType.Positional:
                    return EasySIP.GetChannelSIP(ChannelType.Positional, EasySessionStatic.Issuer, channelName, EasySessionStatic.Domain, channel3DProperties);

            }
            return EasySIP.GetChannelSIP(ChannelType.NonPositional, EasySessionStatic.Issuer, channelName, EasySessionStatic.Domain);
        }

        public string GetChannelSIP(string channelName)
        {
            string result = "";
            foreach (var session in EasySessionStatic.ChannelSessions)
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
            if (EasySessionStatic.ChannelSessions.ContainsKey(channelName))
            {
                EasySessionStatic.ChannelSessions.Remove(channelName);
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
                    accessToken = AccessToken.Token_f(EasySessionStatic.SecretKey, EasySessionStatic.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySessionStatic.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySessionStatic.LoginSessions[userName]), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySessionStatic.Issuer, channelSession.Channel.Name,
                        EasySessionStatic.Domain));
                    break;
                case ChannelType.Echo:
                    accessToken = AccessToken.Token_f(EasySessionStatic.SecretKey, EasySessionStatic.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySessionStatic.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySessionStatic.LoginSessions[userName]), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySessionStatic.Issuer, channelSession.Channel.Name,
                        EasySessionStatic.Domain));
                    break;
                case ChannelType.Positional:
                    accessToken = AccessToken.Token_f(EasySessionStatic.SecretKey, EasySessionStatic.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySessionStatic.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySessionStatic.LoginSessions[userName]), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySessionStatic.Issuer, channelSession.Channel.Name,
                        EasySessionStatic.Domain, channel3DProperties ?? new Channel3DProperties()));
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
                    accessToken = AccessToken.Token_f(EasySessionStatic.SecretKey, EasySessionStatic.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySessionStatic.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySessionStatic.LoginSessions[userName]), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySessionStatic.Issuer, channelSession.Channel.Name,
                        EasySessionStatic.Domain, matchRegion, matchHash));
                    break;
                case ChannelType.Echo:
                    accessToken = AccessToken.Token_f(EasySessionStatic.SecretKey, EasySessionStatic.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySessionStatic.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySessionStatic.LoginSessions[userName]), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySessionStatic.Issuer, channelSession.Channel.Name,
                        EasySessionStatic.Domain, matchRegion, matchHash));
                    break;
                case ChannelType.Positional:
                    accessToken = AccessToken.Token_f(EasySessionStatic.SecretKey, EasySessionStatic.Issuer,
                        AccessToken.SecondsSinceUnixEpochPlusDuration(TimeSpan.FromSeconds(90)), vivoxAction, EasySessionStatic.UniqueCounter, null,
                        EasySIP.GetUserSIP(EasySessionStatic.LoginSessions[userName]), EasySIP.GetChannelSIP(channelSession.Channel.Type, EasySessionStatic.Issuer, channelSession.Channel.Name,
                        EasySessionStatic.Domain, matchRegion, matchHash, channel3DProperties));
                    break;
            }
            return accessToken;
        }

        #endregion



        #region Channel Callbacks


        public async void OnChannelStatePropertyChanged(object sender, PropertyChangedEventArgs channelArgs)
        {
            var senderIChannelSession = (IChannelSession)sender;

            if (channelArgs.PropertyName == "ChannelState")
            {
                switch (senderIChannelSession.ChannelState)
                {
                    case ConnectionState.Connecting:
                        EasyEventsStatic.OnChannelConnecting(senderIChannelSession);
                        break;
                    case ConnectionState.Connected:
                        EasyEventsStatic.OnChannelConnected(senderIChannelSession);
                        break;
                    case ConnectionState.Disconnecting:
                        EasyEventsStatic.OnChannelDisconnecting(senderIChannelSession);
                        break;
                    case ConnectionState.Disconnected:
                        EasyEventsStatic.OnChannelDisconnected(senderIChannelSession);
                        break;
                }
                if (EasySessionStatic.UseDynamicEvents)
                {
                    await HandleDynamicEventsAsync(channelArgs, senderIChannelSession);
                }
            }
        }


        private async Task HandleDynamicEventsAsync(PropertyChangedEventArgs channelArgs, IChannelSession channelSession)
        {
            switch (channelSession.ChannelState)
            {
                case ConnectionState.Connecting:
                    await EasyEventsAsyncStatic.OnChannelConnectingAsync(channelSession);
                    break;
                case ConnectionState.Connected:
                    await EasyEventsAsyncStatic.OnChannelConnectedAsync(channelSession);
                    break;
                case ConnectionState.Disconnecting:
                    await EasyEventsAsyncStatic.OnChannelDisconnectingAsync(channelSession);
                    break;
                case ConnectionState.Disconnected:
                    await EasyEventsAsyncStatic.OnChannelDisconnectedAsync(channelSession);
                    break;
            }
        }


        #endregion

    }
}