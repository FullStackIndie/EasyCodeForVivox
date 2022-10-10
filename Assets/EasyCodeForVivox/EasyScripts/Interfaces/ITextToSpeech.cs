using System.ComponentModel;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface ITextToSpeech
    {
        string FemaleVoice { get; }
        string MaleVoice { get; }

        void Subscribe(ILoginSession loginSession);
        void Unsubscribe(ILoginSession loginSession);
        void TTSChooseVoice(string voiceName, ILoginSession loginSession);
        void TTSSpeak(string message, TTSDestination destination, ILoginSession loginSession);
        void OnTTSMessageAdded(object sender, ITTSMessageQueueEventArgs ttsArgs);
        void OnTTSMessageRemoved(object sender, ITTSMessageQueueEventArgs ttsArgs);
        void OnTTSMessageUpdated(object sender, ITTSMessageQueueEventArgs ttsArgs);
        void OnTTSPropertyChanged(object sender, PropertyChangedEventArgs ttsPropArgs);
    }
}

