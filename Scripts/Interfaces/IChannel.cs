using System.ComponentModel;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface IChannel
    {
        IChannelSession CreateNewChannel(string userName, string channelName, ChannelType channelType, Channel3DProperties channel3DProperties = null);

        string GetChannelSIP(ChannelType channelType, string channelName, Channel3DProperties channel3DProperties = null);
        string GetChannelSIP(string channelName);
        string GetChannelToken(string userName, IChannelSession channelSession, bool joinMuted = false, Channel3DProperties channel3DProperties = null);

        IChannelSession GetExistingChannelSession(string userName, string channelName);
        string GetMatchChannelToken(string userName, IChannelSession channelSession, string matchRegion, string matchHash, bool joinMuted = false, Channel3DProperties channel3DProperties = null);

        void JoinChannel(string userName, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel, IChannelSession channelSession, bool joinMuted = false);
        void JoinChannel(string userName, string channelName, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel, ChannelType channelType, bool joinMuted = false, Channel3DProperties channel3DProperties = null);

        void JoinChannelCustom<T>(string userName, string channelName, T eventParameter, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel, ChannelType channelType, bool joinMuted = false, Channel3DProperties channel3DProperties = null);
        void JoinChannelCustom<T>(string userName, T value, bool includeVoice, bool includeText, bool switchTransmissionToThisChannel, IChannelSession channelSession, bool joinMuted = false);

        void LeaveChannel(ILoginSession loginSession, IChannelSession channelToRemove);
        void LeaveChannel(string channelName, string userName);

        void OnChannelStatePropertyChanged(object sender, PropertyChangedEventArgs channelArgs);
        void RemoveChannelSession(string channelName);
        void Subscribe(IChannelSession channelSession);
        void Unsubscribe(IChannelSession channelSession);
    }
}
