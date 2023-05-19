using EasyCodeForVivox.Events;
using EasyCodeForVivox.Events.Internal;
using EasyCodeForVivox.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyMute
    {

        private readonly EasyEvents _events;
        private readonly EasyEventsAsync _eventsAsync;
        private readonly EasySettingsSO _settings;

        public EasyMute(EasyEvents events, EasyEventsAsync eventsAync,
            EasySettingsSO settings)
        {
            _events = events;
            _eventsAsync = eventsAync;
            _settings = settings;
        }

        public void Subscribe(ILoginSession loginSession)
        {
            loginSession.CrossMutedCommunications.AfterKeyAdded += OnParticipantCrossMuted;
            loginSession.CrossMutedCommunications.BeforeKeyRemoved += OnParticipantCrossUnmuted;
        }

        public void Unsubscribe(ILoginSession loginSession)
        {
            loginSession.CrossMutedCommunications.AfterKeyAdded -= OnParticipantCrossMuted;
            loginSession.CrossMutedCommunications.BeforeKeyRemoved -= OnParticipantCrossUnmuted;
        }

        public void LocalMuteRemoteUser(string userName, IChannelSession channelSession, bool mute)
        {
            var participants = channelSession.Participants;
            string userToMute = EasySIP.GetUserSIP(EasySession.Issuer, userName, EasySession.Domain);

            Debug.Log($"Sip address of User to mute - {userToMute}");
            if (participants[userToMute].InAudio && !participants[userToMute].IsSelf)
            {
                participants[userToMute].LocalMute = mute;
            }
            else
            {
                Debug.Log($"Failed to mute {participants[userToMute].Account.DisplayName}".Color(EasyDebug.Red));
            }
        }

        public void LocalMuteAllUsers(IChannelSession channelSession)
        {
            foreach (var player in channelSession.Participants)
            {
                if (player.InAudio && !player.IsSelf)
                {
                    if (!player.LocalMute)
                    {
                        player.LocalMute = true;
                        Debug.Log($"Muted {player.Account.DisplayName}");
                    }
                }
                else
                {
                    Debug.Log($"Failed to mute {player.Account.DisplayName}, Might be local player".Color(EasyDebug.Red));
                }
            }
        }

        public void LocalUnmuteAllUsers(IChannelSession channelSession)
        {
            foreach (var player in channelSession.Participants)
            {
                if (player.InAudio && !player.IsSelf)
                {
                    if (player.LocalMute)
                    {
                        player.LocalMute = false;
                        Debug.Log($"Unmuted {player.Account.DisplayName}");
                    }
                }
                else
                {
                    Debug.Log($"Failed to unmute {player.Account.DisplayName}, Might be local player".Color(EasyDebug.Red));
                }
            }
        }


        public async void LocalMuteSelf(VivoxUnity.Client client)
        {
            client.AudioInputDevices.Muted = true;
            _events.OnLocalUserMuted();
            await _eventsAsync.OnLocalUserMutedAsync();
        }

        public async void LocalMuteSelf<T>(VivoxUnity.Client client, T value)
        {
            client.AudioInputDevices.Muted = true;
            _events.OnLocalUserMuted(value);
            await _eventsAsync.OnLocalUserMutedAsync(value);
        }

        public async void LocalUnmuteSelf(VivoxUnity.Client client)
        {
            client.AudioInputDevices.Muted = false;
            _events.OnLocalUserUnmuted();
            await _eventsAsync.OnLocalUserUnmutedAsync();
        }

        public async void LocalUnmuteSelf<T>(VivoxUnity.Client client, T value)
        {
            client.AudioInputDevices.Muted = false;
            _events.OnLocalUserUnmuted(value);
            await _eventsAsync.OnLocalUserUnmutedAsync(value);
        }


        public void CrossMuteUser(string userName, string channelName, string userToMute, bool mute)
        {
            var participant = EasySession.ChannelSessions[channelName].Participants.Where(p => p.Account.Name == userToMute).FirstOrDefault();
            if (participant != null)
            {
                EasySession.LoginSessions[userName].SetCrossMutedCommunications(participant.Account, mute, ar =>
                {
                    try
                    {
                        if (ar.IsCompleted)
                        {
                            Debug.Log($"Cross Muted Communications has Completed muting the following player {participant.Account.Name}".Color(EasyDebug.Green));
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                        throw;
                    }
                });
            }
            else
            {
                Debug.Log($"Could not find player {userToMute} in channel {channelName}".Color(EasyDebug.Red));
            }
        }

        public void CrossMuteUsers(string loggedInUserName, string channelName, List<string> usersToMute, bool mute)
        {
            HashSet<AccountId> accountIds = new HashSet<AccountId>();
            foreach (var userName in usersToMute)
            {
                accountIds.Add(EasySession.ChannelSessions[channelName].Participants.Where(p => p.Account.Name == userName).FirstOrDefault().Account);
            }
            EasySession.LoginSessions[loggedInUserName].SetCrossMutedCommunications(accountIds.ToList(), mute, ar =>
            {
                try
                {
                    if (ar.IsCompleted)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (var account in accountIds)
                        {
                            stringBuilder.AppendLine(account.Name);
                        }
                        Debug.Log($"Cross Muted Communications has Completed muting the following players {stringBuilder}".Color(EasyDebug.Green));
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    return;
                }
            });
        }

        public void ClearAllCurrentCrossMutedAccounts(string loggedInUserName)
        {
            EasySession.LoginSessions[loggedInUserName].ClearCrossMutedCommunications(ar =>
            {
                try
                {
                    if (ar.IsCompleted)
                    {
                        Debug.Log($"Clearing Cross Muted Communications for LoginSession for player {loggedInUserName} has Completed");
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    return;
                }
            });
        }

        private async void OnParticipantCrossMuted(object sender, KeyEventArg<AccountId> account)
        {
            if (account != null)
            {
                _events.OnUserCrossMuted(account.Key);
                await _eventsAsync.OnUserCrossMutedAsync(account.Key);
            }
        }

        private async void OnParticipantCrossUnmuted(object sender, KeyEventArg<AccountId> account)
        {
            if (account != null)
            {
                _events.OnUserCrossMuted(account.Key);
                await _eventsAsync.OnUserCrossMutedAsync(account.Key);
            }
        }

    }
}