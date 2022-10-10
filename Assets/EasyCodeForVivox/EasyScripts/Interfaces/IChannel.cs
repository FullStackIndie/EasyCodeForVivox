using System.ComponentModel;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface IChannel
    {
        void Subscribe(IChannelSession channelSession);
        void Unsubscribe(IChannelSession channelSession);
        void JoinChannel(string userName, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel, IChannelSession channelSession, bool joinMuted = false);
        void LeaveChannel(ILoginSession loginSession, IChannelSession channelToRemove);
        string GetChannelToken(string userName, IChannelSession channelSession, bool joinMuted = false, Channel3DProperties channel3DProperties = default);
        string GetMatchChannelToken(string userName, IChannelSession channelSession, string matchRegion, string matchHash, bool joinMuted = false, Channel3DProperties channel3DProperties = default);
        void OnChannelStatePropertyChanged(object sender, PropertyChangedEventArgs channelArgs);
        IChannelSession GetExistingChannelSession(string userName, string channelName);
        IChannelSession CreateNewChannel(string userName, string channelName, ChannelType channelType, Channel3DProperties channel3DProperties = default);
        string GetChannelSIP(ChannelType channelType, string channelName, Channel3DProperties channel3DProperties = default);
        string GetChannelSIP(string channelName);
        void RemoveChannelSession(string channelName);
    }
}
