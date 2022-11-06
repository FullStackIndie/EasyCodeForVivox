using EasyCodeForVivox.Events;
using EasyCodeForVivox.Utilities;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VivoxAccessToken;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyChannel
    {
        private readonly EasyTextChannel _textChannel;
        private readonly EasyAudioChannel _audioChannel;
        private readonly EasyUsers _users;
        private readonly EasyMessages _messages;
        private readonly EasyEvents _events;
        private readonly EasyEventsAsync _eventsAsync;

        public EasyChannel(EasyUsers users, EasyMessages messages,
            EasyAudioChannel audioChannel, EasyTextChannel textChannel,
            EasyEventsAsync eventsAsync, EasyEvents events)
        {
            _users = users;
            _messages = messages;
            _audioChannel = audioChannel;
            _textChannel = textChannel;
            _eventsAsync = eventsAsync;
            _events = events;
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
            if (!EasyVivoxUtilities.FilterChannelAndUserName(channelName)) { return; }
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
                Debug.LogException(e);
                _textChannel.Unsubscribe(channelSession);
                _audioChannel.Unsubscribe(channelSession);
                _users.UnsubscribeFromParticipantEvents(channelSession);
                _messages.UnsubscribeFromChannelMessages(channelSession);
            }
        }

        public void JoinChannelCustom<T>(string userName, string channelName, T eventParameter, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel, ChannelType channelType,
    bool joinMuted = false, Channel3DProperties channel3DProperties = default)
        {
            if (!EasyVivoxUtilities.FilterChannelAndUserName(channelName)) { return; }
            IChannelSession channelSession = CreateNewChannel(userName, channelName, channelType, channel3DProperties);

            try
            {
                _textChannel.Subscribe(channelSession);
                _audioChannel.Subscribe(channelSession);
                _users.SubscribeToParticipantEvents(channelSession);
                _messages.SubscribeToChannelMessages(channelSession);

                JoinChannelCustom(userName, eventParameter, includeVoice, includeText, switchTransmissionToThisChannel, channelSession, joinMuted);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                _textChannel.Unsubscribe(channelSession);
                _audioChannel.Unsubscribe(channelSession);
                _users.UnsubscribeFromParticipantEvents(channelSession);
                _messages.UnsubscribeFromChannelMessages(channelSession);
            }
        }



        protected void JoinChannel(string userName, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel,
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
                    Debug.LogException(e);
                    throw;
                }
            });
        }

        protected void JoinChannelCustom<T>(string userName, T value, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel,
           IChannelSession channelSession, bool joinMuted = false)
        {
            Subscribe(channelSession);
            var accessToken = GetChannelToken(userName, channelSession, joinMuted);
            channelSession.BeginConnect(includeVoice, includeText, switchTransmissionToThisChannel, accessToken, async ar =>
            {
                try
                {
                    channelSession.EndConnect(ar);
                }
                catch (Exception e)
                {
                    Unsubscribe(channelSession);
                    Debug.Log(e.StackTrace);
                    throw;
                }
                finally
                {
                    await HandleDynamicEventsAsync(channelSession, value);
                }
            });
        }

        public void LeaveChannel(string channelName, string userName)
        {
            if (EasySession.ChannelSessions.ContainsKey(channelName))
            {
                if (EasySession.LoginSessions.ContainsKey(userName))
                {
                    _users.UnsubscribeFromParticipantEvents(EasySession.ChannelSessions[channelName]);
                    _messages.UnsubscribeFromChannelMessages(EasySession.ChannelSessions[channelName]);
                    LeaveChannel(EasySession.LoginSessions[userName], EasySession.ChannelSessions[channelName]);
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
                EasySession.ChannelSessions.Remove(channelToRemove.Key.Name);
            }
        }


        public IChannelSession GetExistingChannelSession(string userName, string channelName)
        {
            if (EasySession.ChannelSessions.ContainsKey(channelName))
            {
                if (EasySession.ChannelSessions[channelName].ChannelState == ConnectionState.Disconnected || EasySession.ChannelSessions[channelName] == null)
                {
                    EasySession.ChannelSessions[channelName] = EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelName)));
                }
            }
            else
            {
                return null;
            }

            return EasySession.ChannelSessions[channelName];
        }

        public IChannelSession CreateNewChannel(string userName, string channelName, ChannelType channelType, Channel3DProperties channel3DProperties = default)
        {
            if (channelType == ChannelType.Positional)
            {
                var positionalChannel = EasySession.ChannelSessions.Where(c => c.Value.Channel.Type == ChannelType.Positional).Select(c => c.Value).FirstOrDefault();
                if (positionalChannel != null)
                {
                    Debug.Log($"{positionalChannel.Channel.Name} Is already a 3D Positional Channel. Can Only Have One 3D Positional Channel. " +
                        $"Refer To Vivox Documentation :: Returning Exisiting 3D Channel : {positionalChannel.Channel.Name}".Color(EasyDebug.Yellow));
                    return positionalChannel;
                }
                EasySession.ChannelSessions.Add(channelName, EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelType, channelName, channel3DProperties))));
            }
            else
            {
                if (EasySession.ChannelSessions.ContainsKey(channelName))
                {
                    return EasySession.ChannelSessions[channelName];
                }
                EasySession.ChannelSessions.Add(channelName, EasySession.LoginSessions[userName].GetChannelSession(new ChannelId(GetChannelSIP(channelType, channelName))));
            }

            return EasySession.ChannelSessions[channelName];
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


        public async void OnChannelStatePropertyChanged(object sender, PropertyChangedEventArgs channelArgs)
        {
            var senderIChannelSession = (IChannelSession)sender;

            if (channelArgs.PropertyName == "ChannelState")
            {
                switch (senderIChannelSession.ChannelState)
                {
                    case ConnectionState.Connecting:
                        _events.OnChannelConnecting(senderIChannelSession);
                        break;
                    case ConnectionState.Connected:
                        _events.OnChannelConnected(senderIChannelSession);
                        break;
                    case ConnectionState.Disconnecting:
                        _events.OnChannelDisconnecting(senderIChannelSession);
                        break;
                    case ConnectionState.Disconnected:
                        _events.OnChannelDisconnected(senderIChannelSession);
                        break;
                }
                await HandleDynamicEventsAsync(channelArgs, senderIChannelSession);
            }
        }


        private async Task HandleDynamicEventsAsync(PropertyChangedEventArgs channelArgs, IChannelSession channelSession)
        {
            switch (channelSession.ChannelState)
            {
                case ConnectionState.Connecting:
                    await _eventsAsync.OnChannelConnectingAsync(channelSession);
                    break;
                case ConnectionState.Connected:
                    await _eventsAsync.OnChannelConnectedAsync(channelSession);
                    break;
                case ConnectionState.Disconnecting:
                    await _eventsAsync.OnChannelDisconnectingAsync(channelSession);
                    break;
                case ConnectionState.Disconnected:
                    await _eventsAsync.OnChannelDisconnectedAsync(channelSession);
                    break;
            }
        }

        private async Task HandleDynamicEventsAsync<T>(IChannelSession channelSession, T value)
        {
            switch (channelSession.ChannelState)
            {
                case ConnectionState.Connecting:
                    _events.OnChannelConnecting(channelSession, value);
                    await _eventsAsync.OnChannelConnectingAsync(channelSession, value);
                    break;
                case ConnectionState.Connected:
                    _events.OnChannelConnected(channelSession, value);
                    await _eventsAsync.OnChannelConnectedAsync(channelSession, value);
                    break;
                case ConnectionState.Disconnecting:
                    _events.OnChannelDisconnecting(channelSession, value);
                    await _eventsAsync.OnChannelDisconnectingAsync(channelSession, value);
                    break;
                case ConnectionState.Disconnected:
                    _events.OnChannelDisconnected(channelSession, value);
                    await _eventsAsync.OnChannelDisconnectedAsync(channelSession, value);
                    break;
            }
        }


        #endregion

    }
}