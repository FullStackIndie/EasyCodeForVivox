using System;
using System.Collections.Generic;
using UnityEngine;
using VivoxAccessToken;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyMute
    {

        public void LocalToggleMuteRemoteUser(string userName, IChannelSession channelSession)
        {
            var participants = channelSession.Participants;
            string userToMute = EasySIP.GetUserSIP(EasySession.Issuer, userName, EasySession.Domain);
            Debug.Log($"Sip address of User to mute - {userToMute}");
            if (participants[userToMute].InAudio && !participants[userToMute].IsSelf)
            {
                if (participants[userToMute].LocalMute)
                {
                    participants[userToMute].LocalMute = false;
                }
                else
                {
                    participants[userToMute].LocalMute = true;
                }
            }
            else
            {
                Debug.Log($"Failed to mute {participants[userToMute].Account.DisplayName}");
            }
        }



        public void MuteAllUsers(IChannelSession channelSession)
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
                    Debug.Log($"Failed to mute {player.Account.DisplayName}, Might be local player");
                }
            }
        }

        public void UnmuteAllUsers(IChannelSession channelSession)
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
                    Debug.Log($"Failed to mute {player.Account.DisplayName}, Might be local player");
                }
            }
        }


        public void LocalMuteSelf(VivoxUnity.Client client)
        {
            client.AudioInputDevices.Muted = true;
            EasyEvents.OnLocalUserMuted(true);
        }

        public void LocalUnmuteSelf(VivoxUnity.Client client)
        {
            client.AudioInputDevices.Muted = false;
            EasyEvents.OnLocalUserUnmuted(false);
        }

        public void CrossMuteUser(ILoginSession loginSession, bool mute)
        {
            if (mute)
            {
                Debug.Log($"Muting {loginSession.LoginSessionId.DisplayName}");
            }
            else
            {
                Debug.Log($"Unmuting {loginSession.LoginSessionId.DisplayName}");
            }
            // todo check if it works
            loginSession.SetCrossMutedCommunications(loginSession.LoginSessionId, mute, CrossMuteResult);
        }

        public void CrossMuteUsers(ILoginSession loginSessionToMute, bool mute)
        {
            List<AccountId> accountIds = new List<AccountId>();
            loginSessionToMute.SetCrossMutedCommunications(accountIds, mute, CrossMuteResult);
            // todo check if this actually works
            // add this callback listener
            // loginSessionToMute.CrossMutedCommunications.AfterKeyAdded +=
        }

        public void ClearAllCurrentCrossMutedAccounts(ILoginSession loginSession)
        {
            loginSession.ClearCrossMutedCommunications(CrossMuteResult);
        }

        public void CrossMuteResult(IAsyncResult ar)
        {
            try
            {
                if (ar.IsCompleted)
                {
                    Debug.Log("Clear Cross Muted Communications has Completed : Not sure if it works");
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return;
            }
        }
    }
}