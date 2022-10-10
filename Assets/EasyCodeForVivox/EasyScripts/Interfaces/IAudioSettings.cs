using System.ComponentModel;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface IAudioSettings
    {
        void SetAudioDeviceInput(IAudioDevice device, VivoxUnity.Client client);
        void SetAudioDeviceOutput(IAudioDevice device, VivoxUnity.Client client);
        void SetAudioDevicesInput(VivoxUnity.Client client, IAudioDevice targetInput = null);
        void SetAudioDevicesOutput(VivoxUnity.Client client, IAudioDevice targetOutput = null);
        void AdjustLocalPlayerAudioVolume(int value, VivoxUnity.Client client);
        void AdjustRemotePlayerAudioVolume(string userName, IChannelSession channelSession, float value);
        void StartAudioInjection(string wavToInject, ILoginSession loginSession);
        void StopAudioInjection(ILoginSession loginSession);
        void RefreshAudioDevices(VivoxUnity.Client client);
        void OnAudioDeviceAdded(object sender, PropertyChangedEventArgs propArgs);
        void OnAudioDeviceRemoved(object sender, PropertyChangedEventArgs propArgs);
    }
}
