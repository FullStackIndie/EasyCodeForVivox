using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface IUsers
    {
        void SubscribeToParticipantEvents(IChannelSession channelSession);
        void UnsubscribeFromParticipantEvents(IChannelSession channelSession);
        void OnUserJoinedChannel(object sender, KeyEventArg<string> keyArg);
        void OnUserLeftChannel(object sender, KeyEventArg<string> keyArg);
        void OnUserValuesUpdated(object sender, ValueEventArg<string, IParticipant> valueArg);

    }
}

