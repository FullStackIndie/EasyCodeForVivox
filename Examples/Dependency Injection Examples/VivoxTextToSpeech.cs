using EasyCodeForVivox;
using UnityEngine;
using VivoxUnity;
using Zenject;

public class VivoxTextToSpeech : MonoBehaviour
{
    EasyTextToSpeech _textToSpeech;

    [Inject]
    private void Initialize(EasyTextToSpeech textToSpeech)
    {
        _textToSpeech = textToSpeech;
    }

    public void ChooseVivoxVoice()
    {
        _textToSpeech.ChooseVoiceGender(VoiceGender.female, "userName");
    }

    public void TTSMsgLocalPlayOverCurrent()
    {
        _textToSpeech.PlayTTSMessage("my message to play", TTSDestination.LocalPlayback, EasySession.LoginSessions["userName"]);
    }

    public void TTSMsgRemotePlayOverCurrent()
    {
        _textToSpeech.PlayTTSMessage("my message to play", TTSDestination.RemoteTransmission, EasySession.LoginSessions["userName"]);
    }

    public void TTSMsgLocalRemotePlayOverCurrent()
    {
        _textToSpeech.PlayTTSMessage("my message to play", TTSDestination.RemoteTransmissionWithLocalPlayback, EasySession.LoginSessions["userName"]);
    }

    public void TTSMsgQueueLocal()
    {
        _textToSpeech.PlayTTSMessage("my message to play", TTSDestination.QueuedLocalPlayback, EasySession.LoginSessions["userName"]);
    }

    public void TTSMsgQueueRemote()
    {
        _textToSpeech.PlayTTSMessage("my message to play", TTSDestination.QueuedRemoteTransmission, EasySession.LoginSessions["userName"]);
    }

    public void TTSMsgQueueRemoteLocal()
    {
        _textToSpeech.PlayTTSMessage("my message to play", TTSDestination.QueuedRemoteTransmissionWithLocalPlayback, EasySession.LoginSessions["userName"]);
    }

    public void TTSMsgLocalReplaceCurrentMessagePlaying()
    {
        _textToSpeech.PlayTTSMessage("my message to play", TTSDestination.ScreenReader, EasySession.LoginSessions["userName"]);
    }

}
