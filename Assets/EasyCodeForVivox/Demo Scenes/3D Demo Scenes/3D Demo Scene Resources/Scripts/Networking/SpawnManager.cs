using Unity.Netcode;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private SpawnSettingsSO _spawnSettings;
    private EasySettingsSO _settings;


    public override void OnNetworkSpawn()
    {
        _settings = ScriptableObject.CreateInstance<EasySettingsSO>();
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientDisconnected;
        SpawnExistingClients();
    }

    public override void OnNetworkDespawn()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientDisconnected;
        base.OnNetworkDespawn();
    }

    public void OnClientConnected(ulong clientId)
    {
        if (IsServer)
        {
            if (_settings.LogEasyNetCode)
                Debug.Log($"Spawned player {clientId}");
            SpawnPlayer(clientId);
        }
    }

    public void OnClientDisconnected(ulong clientId)
    {

    }

    public void SpawnExistingClients()
    {
        if (IsServer)
        {
            foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
            {
                SpawnPlayer(client.ClientId);
            }
        }
    }

    private void SpawnPlayer(ulong clientId)
    {
        if (_settings.LogEasyNetCode)
            Debug.Log($"Spawned player {clientId}");
        GameObject player = Instantiate(_playerPrefab, _spawnSettings.GetSpawnPoint(), Quaternion.identity);
        player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
    }
}
