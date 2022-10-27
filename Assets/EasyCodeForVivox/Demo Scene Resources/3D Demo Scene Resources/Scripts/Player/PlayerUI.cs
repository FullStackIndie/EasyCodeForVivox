using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerUI : NetworkBehaviour
{
    [SerializeField] private TMP_Text _playerNameText;


    public override void OnNetworkSpawn()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            NetworkObject party = Instantiate(Resources.Load<NetworkObject>("Network/NetworkParty"));
            party.SpawnWithOwnership(OwnerClientId, false);
            JoinPartyServerRpc(NetCodeManager.GetPlayerInfo(OwnerClientId).Value);
            //UpdatePlayerNameClientRpc(NetCodeManager.GetPlayerInfo(OwnerClientId).Value);
        }
    }

    //[ServerRpc(RequireOwnership = false)]
    //private void UpdatePlayerNameServerRpc(PlayerInfo playerInfo)
    //{
    //    _playerName.Value = playerInfo.playerName;
    //}

    [ClientRpc]
    private void UpdatePlayerNameClientRpc(PlayerInfo playerInfo)
    {
        if (OwnerClientId == playerInfo.playerId)
            _playerNameText.text = playerInfo.playerName.ToString();
        else
            Debug.Log($"Owner Id {OwnerClientId} did not own this object");
    }


    private void UpdatePlayerName(PlayerInfo? playerInfo)
    {
        if (playerInfo.HasValue)
        {
            if (OwnerClientId == playerInfo.Value.playerId)
                _playerNameText.text = playerInfo.Value.playerName.ToString();
            else
                Debug.Log($"Owner Id {OwnerClientId} did not own this object");
        }
    }


    public void UpdatePlayerName(PlayerInfo playerInfo)
    {
        _playerNameText.text = playerInfo.playerName.ToString();
    }



    //private void UpdatePlayerName(PlayerInfo? playerInfo)
    //{
    //    if (playerInfo.HasValue)
    //    {
    //        _playerName.Value = playerInfo.Value.playerName;
    //    }
    //}

    //private void UpdatePlayerNameUI(FixedString32Bytes oldValue, FixedString32Bytes newValue)
    //{
    //    _playerNameText.text = newValue.ToString();
    //    if (IsLocalPlayer)
    //    {
    //        _playerNameText.transform.Rotate(new Vector3(0f, 180f, 0f));
    //    }
    //}

    [ServerRpc]
    private void JoinPartyServerRpc(PlayerInfo playerInfo)
    {
        var party = FindObjectOfType<NetworkParty>();
        playerInfo.playerId = NetworkManager.LocalClientId;
        party.JoinParty(playerInfo);
    }
}
