using EasyCodeForVivox;
using EasyCodeForVivox.Extensions;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using VivoxUnity;
using Zenject;

public class VivoxChannel : MonoBehaviour
{
    EasyChannel _channel;

    [Inject]
    public void Initialize(EasyChannel channel)
    {
        _channel = channel;
    }

    public void JoinEchoChannel()
    {
        _channel.JoinChannel("username", "echo", true, false, true, ChannelType.Echo, joinMuted: false);
    }

    public void JoinChannel()
    {
        _channel.JoinChannel("username", "chat", true, true, false, ChannelType.NonPositional, joinMuted: false);
    }
    
    public void Join3DChannel()
    {
        var channelProperties = new Channel3DProperties(32, 1, 1.0f, AudioFadeModel.InverseByDistance);

        _channel.JoinChannel("username", "3D", true, false, false, ChannelType.Positional, joinMuted: false, channel3DProperties: channelProperties);
    }

    public async void Join3DRegionChannel()
    {
        var channelProperties = new Channel3DProperties(32, 1, 1.0f, AudioFadeModel.InverseByDistance);

        // using match hash
        var matchHash = "lobby name".GetMD5Hash();
        _channel.JoinChannelRegion("userName", "3D", "NA", matchHash, true, false, false, ChannelType.Positional, joinMuted: false, channel3DProperties: channelProperties);

        //using Unity's Lobby Service
        Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName: "lobby name", maxPlayers: 4);
        _channel.JoinChannelRegion("userName", "3D", "NA", lobby.Id, true, false, false, ChannelType.Positional, joinMuted: false, channel3DProperties: channelProperties);
    }

    public async void JoinSquadRegionChannel()
    {
        var channelProperties = new Channel3DProperties(32, 1, 1.0f, AudioFadeModel.InverseByDistance);

        // using match hash
        var matchHash = "lobby name".GetMD5Hash();
        _channel.JoinChannelRegion("userName", "sqaud1", "NA", matchHash, true, false, false, ChannelType.Positional, joinMuted: false, channel3DProperties: channelProperties);

        //using Unity's Lobby Service
        Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName: "lobby name", maxPlayers: 4);
        _channel.JoinChannelRegion("userName", "sqaud1", "NA", lobby.Id, true, false, false, ChannelType.Positional, joinMuted: false, channel3DProperties: channelProperties);
    }




}
