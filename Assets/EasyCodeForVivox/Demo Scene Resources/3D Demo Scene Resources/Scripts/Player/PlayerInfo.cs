using System;
using Unity.Collections;
using Unity.Netcode;

public struct PlayerInfo : INetworkSerializable, IEquatable<PlayerInfo>
{
    public FixedString32Bytes playerName;
    public ulong playerId;


    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerName);
    }

    public bool Equals(PlayerInfo other)
    {
        if(other.playerName == playerName)
        {
            return true;
        }
        return false;
    }
}
