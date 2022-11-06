using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox.Examples
{
    public class ApiResponse : MonoBehaviour
    {
        [Serializable]
        public class PlayerInfo
        {
            public string Id { get; set; }
            public int RemotePlayersVolumeLevel { get; set; }
            public bool JoinMuted { get; set; }
            public string Name { get; set; }
            public string ApiResponse { get; set; }
            public List<string> FriendsList { get; set; }
        }


        public async void LoginCustom()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts/1");
            var json = await response.Content.ReadAsStringAsync();

            PlayerInfo playerInfo = new PlayerInfo();
            playerInfo.ApiResponse = json;
            playerInfo.JoinMuted = true;
            playerInfo.Name = "player";
            playerInfo.RemotePlayersVolumeLevel = 10;
            playerInfo.FriendsList = new List<string>() { "Friend1", "Friend2" };
            var easyManager = FindObjectOfType<EasyManager>();
            easyManager.LoginToVivox(playerInfo.Name, playerInfo);
        }
        [LoginEventAsync(LoginStatus.LoggedIn)]
        public async Task LoginAsync(ILoginSession loginSession, PlayerInfo playerInfo)
        {
            await Task.Run(() =>
            {
                Debug.Log($"Player has joined {loginSession.LoginSessionId.DisplayName}");
                Debug.Log($"{playerInfo.Name} api response message is {playerInfo.ApiResponse}");
            });
        }

        [LoginEventAsync(LoginStatus.LoggedIn)]
        public async void LoginCustomAsync(ILoginSession loginSession, PlayerInfo playerInfo)
        {
            Debug.Log($"Player has joined {loginSession.LoginSessionId.DisplayName}");
            Debug.Log($"{playerInfo.Name} api response message is {playerInfo.ApiResponse}");
        }

        public async void Custom()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts/1");
            var json = await response.Content.ReadAsStringAsync();

            PlayerInfo playerInfo = new PlayerInfo();
            playerInfo.ApiResponse = json;
            playerInfo.JoinMuted = true;
            playerInfo.Name = EasySession.LoginSessions.FirstOrDefault().Value.LoginSessionId.DisplayName;
            playerInfo.RemotePlayersVolumeLevel = 10;
            playerInfo.FriendsList = new List<string>() { "Friend1", "Friend2" };
            var easyManager = FindObjectOfType<EasyManager>();
            easyManager.JoinChannelCustom(EasySession.LoginSessions.FirstOrDefault().Value.LoginSessionId.DisplayName, "3D", playerInfo, true, true, false, ChannelType.Positional);
        }

        [ChannelEventAsync(ChannelStatus.ChannelConnected)]
        public void ChannelJoinedCustom(IChannelSession channelSession)
        {
            Debug.Log(channelSession.Channel.Name);
        }

        [ChannelEventAsync(ChannelStatus.ChannelConnected)]
        public async void ChannelJoinedCustomd(IChannelSession channelSession, PlayerInfo playerInfo)
        {
            Debug.Log(channelSession.Channel.Name);
            Debug.Log(playerInfo.ApiResponse);
        }


    }
}
