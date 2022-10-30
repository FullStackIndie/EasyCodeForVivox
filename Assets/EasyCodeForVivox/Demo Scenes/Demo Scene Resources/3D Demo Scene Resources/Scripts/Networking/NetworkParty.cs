using Unity.Netcode;

public class NetworkParty : NetworkBehaviour
{
    public NetworkList<PlayerInfo> Players;

    private void Awake()
    {
        Players = new NetworkList<PlayerInfo>();
    }

    public void JoinParty(PlayerInfo playerInfo)
    {
        if (IsServer)
        {
            Players.Add(playerInfo);
        }
    }
}
