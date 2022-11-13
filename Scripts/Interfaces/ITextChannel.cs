using System.ComponentModel;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface ITextChannel
    {
        void Subscribe(IChannelSession channelSession);
        void Unsubscribe(IChannelSession channelSession);
        void ToggleTextInChannel(IChannelSession channelSession, bool join);
        void ToggleTextChannelActive<T>(IChannelSession channelSession, bool join, T eventParameter);
        void OnChannelTextPropertyChanged(object sender, PropertyChangedEventArgs propArgs);
    }
}
