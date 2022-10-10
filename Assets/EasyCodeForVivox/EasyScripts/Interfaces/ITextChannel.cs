using System.ComponentModel;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface ITextChannel
    {
        void Subscribe(IChannelSession channelSession);
        void Unsubscribe(IChannelSession channelSession);
        void ToggleTextChannelActive(IChannelSession channelSession, bool join);
        void OnChannelTextPropertyChanged(object sender, PropertyChangedEventArgs propArgs);
    }
}
