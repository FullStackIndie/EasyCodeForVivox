using EasyCodeForVivox;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EasyCodeForVivox.Examples
{
    public class VivoxMute : MonoBehaviour
    {
        EasyMute _mute;

        [Inject]
        private void Initialize(EasyMute mute)
        {
            _mute = mute;
        }

        private void LocalMuteSelf()
        {
            _mute.LocalMuteSelf(EasySession.Client);
        }

        private void LocalUnmuteSelf()
        {
            _mute.LocalUnmuteSelf(EasySession.Client);
        }

        public void MuteRemoteUser()
        {
            _mute.LocalMuteRemoteUser("userName", EasySession.ChannelSessions["chat"], true);
        }

        public void UnmuteRemoteUser()
        {
            _mute.LocalMuteRemoteUser("userName", EasySession.ChannelSessions["chat"], false);
        }

        public void MuteAllPlayers()
        {
            if (EasySession.ChannelSessions.ContainsKey("chat"))
            {
                _mute.LocalMuteAllUsers(EasySession.ChannelSessions["chat"]);
            }
            else
            {
                Debug.Log("Channel Does not exist. Cannot Mute player");
            }
        }

        public void UnmuteAllPlayers()
        {
            if (EasySession.ChannelSessions.ContainsKey("chat"))
            {
                _mute.LocalUnmuteAllUsers(EasySession.ChannelSessions["chat"]);
            }
            else
            {
                Debug.Log("Channel Does not exist. Cannot Unmute player");
            }
        }

        public void CrossMuteUser()
        {
            _mute.CrossMuteUser("userName", "channelName", "usernameToMute", true);
        }

        public void CrossUnmuteUser()
        {
            _mute.CrossMuteUser("userName", "channelName", "usernameToMute", true);
        }

        public void CrossMuteUsers()
        {
            List<string> usersToMute = new List<string>() { "player1", "player2" };
            _mute.CrossMuteUsers("userName", "channelName", usersToMute, true);
        }

        public void CrossUnmuteUsers()
        {
            List<string> usersToMute = new List<string>() { "player1", "player2" };
            _mute.CrossMuteUsers("userName", "channelName", usersToMute, false);
        }

        public void ClearCrossMutedUsersForLoginSession()
        {
            _mute.ClearAllCurrentCrossMutedAccounts("userName");
        }
    }
}
