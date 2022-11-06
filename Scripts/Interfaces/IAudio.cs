using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface IAudio
    {
        void SetAudioDeviceInput(IAudioDevice device, VivoxUnity.Client client);
        void SetAudioDeviceOutput(IAudioDevice device, VivoxUnity.Client client);
        void AdjustLocalPlayerAudioVolume(int value, VivoxUnity.Client client);
        void AdjustRemotePlayerAudioVolume(string userName, IChannelSession channelSession, float value);
        void StartAudioInjection(string wavToInject, ILoginSession loginSession);
        void StopAudioInjection(ILoginSession loginSession);
        void RefreshAudioDevices(VivoxUnity.Client client);
        void OnAudioInputDeviceAdded(object sender, KeyEventArg<string> propArgs);
        void OnAudioInputDeviceRemoved(object sender, KeyEventArg<string> propArgs);
        void OnAudioInputDeviceUpdated(object sender, ValueEventArg<string, IAudioDevice> valueArgs);
        void OnAudioOutputDeviceAdded(object sender, KeyEventArg<string> propArgs);
        void OnAudioOutputDeviceRemoved(object sender, KeyEventArg<string> propArgs);
        void OnAudioOutputDeviceUpdated(object sender, ValueEventArg<string, IAudioDevice> valueArgs);
    }
}
