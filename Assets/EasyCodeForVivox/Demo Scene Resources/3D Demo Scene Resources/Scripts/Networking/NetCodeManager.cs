using EasyCodeForVivox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetCodeManager : MonoBehaviour
{
    [SerializeField] private SpawnSettings _spawnManager;
    private static Dictionary<ulong, PlayerInfo> _players;

    private void Awake()
    {
        _players = new Dictionary<ulong, PlayerInfo>();
    }

    private void OnApplicationQuit()
    {
        UnsubscribeToNetworkManagerEvents();
        LeaveGame();
        NetworkManager.Singleton?.Shutdown();
    }

    private void SubscribeToNetworkManagerEvents()
    {
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    private void UnsubscribeToNetworkManagerEvents()
    {
        if (NetworkManager.Singleton == null) { return; }

        NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
    }

    public static PlayerInfo? GetPlayerInfo(ulong clientId)
    {
        if (_players.ContainsKey(clientId))
        {
            return _players[clientId];
        }
        return null;
    }

    public void ServerSetup()
    {
        _players = new Dictionary<ulong, PlayerInfo>();

        NetworkManager.Singleton.StartServer();
        LoadGameScene();
    }

    public void HostSetup()
    {
        _players = new Dictionary<ulong, PlayerInfo>();
        var json = GetPlayerInfoAsJson(NetworkManager.Singleton.LocalClientId);

        NetworkManager.Singleton.ConnectionApprovalCallback += ApproveClient;
        NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.UTF8.GetBytes(json);
        NetworkManager.Singleton.StartHost();
        LoadGameScene();
    }


    public void ClientSetup()
    {
        var json = GetPlayerInfoAsJson(NetworkManager.Singleton.LocalClientId);

        NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.UTF8.GetBytes(json);
        NetworkManager.Singleton.StartClient();
        LoadGameScene();
    }

    private string GetPlayerInfoAsJson(ulong clientId)
    {
        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.playerName = EasySession.LoginSessions.FirstOrDefault().Value.LoginSessionId.DisplayName;
        playerInfo.playerId = clientId;

        var json = JsonUtility.ToJson(playerInfo);
        return json;
    }

    private void OnServerStarted()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            OnClientConnected(NetworkManager.ServerClientId);
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {

        }
    }

    private void OnClientDisconnected(ulong clientId)
    {
        if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsHost)
        {
            _players.Remove(clientId);
            return;
        }
        NetworkManager.Singleton.DisconnectClient(clientId);
    }

    private void ApproveClient(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        if (NetworkManager.Singleton.ConnectedClients.Count < 20)
        {
            if (request.Payload != null)
            {
                response.Approved = true;
                var json = Encoding.UTF8.GetString(request.Payload);
                PlayerInfo playerInfo = JsonUtility.FromJson<PlayerInfo>(json);
                _players.Add(request.ClientNetworkId, playerInfo);

                //response.Position = _spawnManager.GetSpawnPoint();
                //response.CreatePlayerObject = true;

                response.CreatePlayerObject = false;
                Debug.Log($"Client {request.ClientNetworkId} has been approved");
            }
        }
    }

    private void LoadGameScene()
    {
        if (EasySession.LoginSessions.Count == 0 || EasySession.ChannelSessions.Count == 0)
        {
            Debug.Log("Login or Join Channel before joining the game".Color(EasyDebug.Yellow));
            return;
        }
        if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsHost)
        {
            var status = NetworkManager.Singleton.SceneManager.LoadScene("3D Demo Scene", LoadSceneMode.Single);
            if (status != SceneEventProgressStatus.Started)
            {
                Debug.LogWarning($"Failed to load 3D Demo Scene : [ Status ] {status}");
            }
        }
    }

    public void LeaveGame()
    {
        if (NetworkManager.Singleton == null) { return; }

        if (NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.ConnectionApprovalCallback -= ApproveClient;
        }
        var result = SceneManager.LoadSceneAsync("Lobby");
        while (!result.isDone)
        {
            Debug.Log(result.progress);
        }
    }

}
