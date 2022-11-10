using System.ComponentModel;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface IAudioChannel
    {
        void Subscribe(IChannelSession channelSession);
        void Unsubscribe(IChannelSession channelSession);
        void ToggleAudioInChannel(IChannelSession channelSession, bool join);
        void ToggleAudioChannelActive<T>(IChannelSession channelSession, bool join, T eventParameter);
        void OnChannelAudioPropertyChanged(object sender, PropertyChangedEventArgs propArgs);
    }
}
