using System.ComponentModel;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface IAudioChannel
    {
        void Subscribe(IChannelSession channelSession);
        void Unsubscribe(IChannelSession channelSession);
        void ToggleAudioChannelActive(IChannelSession channelSession, bool join);
        void OnChannelAudioPropertyChanged(object sender, PropertyChangedEventArgs propArgs);
    }
}
