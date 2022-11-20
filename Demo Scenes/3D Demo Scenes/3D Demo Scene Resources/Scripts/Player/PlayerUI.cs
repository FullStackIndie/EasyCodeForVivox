using EasyCodeForVivox.DemoScene;
using EasyCodeForVivox.Extensions;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerUI : NetworkBehaviour
{
    [SerializeField] private TMP_Text _playerNameText;
    NetworkVariable<FixedString32Bytes> _playerName = new NetworkVariable<FixedString32Bytes>();
    private EasySettingsSO _settings;

    public override void OnNetworkSpawn()
    {
        _settings = ScriptableObject.CreateInstance<EasySettingsSO>();
        if (NetworkManager.Singleton.IsServer)
        {
            NetworkObject party = Instantiate(Resources.Load<NetworkObject>("Network/NetworkParty"));
            party.SpawnWithOwnership(OwnerClientId, false);
            _playerName.Value = NetCodeManager.GetPlayerInfo(OwnerClientId).Value.playerName.ToString();
            // sets the client text (including client that is acting as the Host) doesnt work on client only host
            //UpdatePlayerNameClientRpc(NetCodeManager.GetPlayerInfo(OwnerClientId).Value); 
            _playerNameText.text = NetCodeManager.GetPlayerInfo(OwnerClientId).Value.playerName.ToString(); // sets the players names on the server
            if (_settings.LogEasyNetCode)
                Debug.Log($"ServerOwned: Owner Id [ {OwnerClientId} ] player name has been set".Color(EasyDebug.Yellow));
        }
        if (IsOwnedByServer && !IsServer)
        {
            _playerNameText.text = _playerName.Value.ToString(); // sets client player names that arent the local player
            if (_settings.LogEasyNetCode)
                Debug.Log($"ServerOwned: Owner Id [ {OwnerClientId} ] player name has been set".Color(EasyDebug.Yellow));
        }
        _playerName.OnValueChanged += OnPlayerNameChanged; // 
    }

    public override void OnNetworkDespawn()
    {
        _playerName.OnValueChanged -= OnPlayerNameChanged;
        base.OnNetworkDespawn();
    }


    [ClientRpc]
    private void UpdatePlayerNameClientRpc(PlayerInfo playerInfo)
    {
        if (OwnerClientId == playerInfo.playerId)
        {
            _playerNameText.text = playerInfo.playerName.ToString();
            if (_settings.LogEasyNetCode)
                Debug.Log($"ClientOwned: Owner Id [ {OwnerClientId} ] player name has been set".Color(EasyDebug.Yellow));
        }
        else
        {
            if (_settings.LogEasyNetCode)
                Debug.Log($"Error : Owner Id [ {playerInfo.playerId} ] did not own this object".Color(EasyDebug.Yellow));
        }
    }

    public void OnPlayerNameChanged(FixedString32Bytes oldValue, FixedString32Bytes newValue)
    {
        _playerNameText.text = newValue.ToString();
        if (IsLocalPlayer)
        {
            _playerNameText.transform.Rotate(new Vector3(0f, 180f, 0f));
        }
        if (_settings.LogEasyNetCode)
            Debug.Log($"ClientOwned: Owner Id [ {OwnerClientId} ] player name has been set".Color(EasyDebug.Yellow));
    }


}
