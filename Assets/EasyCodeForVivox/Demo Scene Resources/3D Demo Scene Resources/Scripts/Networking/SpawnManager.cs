using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private SpawnSettings _spawnSettings;

    public override void OnNetworkSpawn()
    {
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
        Debug.Log($"Spawned player {clientId}");
        GameObject player = Instantiate(_playerPrefab, _spawnSettings.GetSpawnPoint(), Quaternion.identity);
        player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        SetNameClientRpc(player, NetCodeManager.GetPlayerInfo(clientId).Value);
    }

    [ClientRpc]
    private void SetNameClientRpc(NetworkObjectReference player, PlayerInfo playerInfo)
    {
        Debug.Log($"Setting name for  player {playerInfo.playerId}");
        ((GameObject)player).GetComponentInChildren<PlayerUI>().UpdatePlayerName(playerInfo);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
