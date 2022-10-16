using EasyCodeForVivox.Events;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyTextToSpeech : ITextToSpeech
    {
        public string MaleVoice { get; } = "en_US male";
        public string FemaleVoice { get; } = "en_US female";


        public void Subscribe(ILoginSession loginSession)
        {
            loginSession.TTS.Messages.AfterMessageAdded += OnTTSMessageAdded;
            loginSession.TTS.Messages.BeforeMessageRemoved += OnTTSMessageRemoved;
            loginSession.TTS.Messages.AfterMessageUpdated += OnTTSMessageUpdated;
        }

        public void Unsubscribe(ILoginSession loginSession)
        {
            loginSession.TTS.Messages.AfterMessageAdded -= OnTTSMessageAdded;
            loginSession.TTS.Messages.BeforeMessageRemoved -= OnTTSMessageRemoved;
            loginSession.TTS.Messages.AfterMessageUpdated -= OnTTSMessageUpdated;
        }


        #region Text-To-Speech Methods


        public void TTSChooseVoice(string voiceName, ILoginSession loginSession)
        {
            ITTSVoice voice = loginSession.TTS.AvailableVoices.FirstOrDefault(v => v.Name == voiceName);
            if (voice != null)
            {
                loginSession.TTS.CurrentVoice = voice;
            }
        }

        public void TTSSpeak(string message, TTSDestination destination, ILoginSession loginSession)
        {
            switch (destination)
            {
                case TTSDestination.LocalPlayback:
                    message.TTS_Msg_Local_PlayOverCurrent(loginSession);
                    break;
                case TTSDestination.RemoteTransmission:
                    message.TTS_Msg_Local_Remote_PlayOverCurrent(loginSession);
                    break;
                case TTSDestination.RemoteTransmissionWithLocalPlayback:
                    message.TTS_Msg_Local_Remote_PlayOverCurrent(loginSession);
                    break;
                case TTSDestination.QueuedLocalPlayback:
                    message.TTS_Msg_Queue_Local(loginSession);
                    break;
                case TTSDestination.QueuedRemoteTransmission:
                    message.TTS_Msg_Queue_Remote(loginSession);
                    break;
                case TTSDestination.QueuedRemoteTransmissionWithLocalPlayback:
                    message.TTS_Msg_Queue_Remote_Local(loginSession);
                    break;
                case TTSDestination.ScreenReader:
                    message.TTS_Msg_Local_ReplaceCurrentPlaying(loginSession);
                    break;
            }
        }


        #endregion


        #region Text-To-Speech Callbacks


        public async void OnTTSMessageAdded(object sender, ITTSMessageQueueEventArgs ttsArgs)
        {
            var source = (ITTSMessageQueue)sender;
            if (source.Count > 9)
            {
                // todo update and research docs
                Debug.Log("Cant keep over 10 messages in Queue");
            }
            EasyEventsStatic.OnTTSMessageAdded(ttsArgs);
            if (EasySessionStatic.UseDynamicEvents)
            {
                await EasyEventsAsyncStatic.OnTTSMessageAddedAsync(ttsArgs);
            }
        }

        public async void OnTTSMessageRemoved(object sender, ITTSMessageQueueEventArgs ttsArgs)
        {
            var source = (ITTSMessageQueue)sender;
            if (source.Count >= 9)
            {
                Debug.Log("Cant keep over 10 messages in Queue");
            }
            EasyEventsStatic.OnTTSMessageRemoved(ttsArgs);
            if (EasySessionStatic.UseDynamicEvents)
            {
                await EasyEventsAsyncStatic.OnTTSMessageRemovedAsync(ttsArgs);
            }
        }

        public async void OnTTSMessageUpdated(object sender, ITTSMessageQueueEventArgs ttsArgs)
        {
            var source = (ITTSMessageQueue)sender;
            if (source.Count >= 9)
            {
                Debug.Log("Cant keep over 10 messages in Queue");
            }
            EasyEventsStatic.OnTTSMessageUpdated(ttsArgs);
            if (EasySessionStatic.UseDynamicEvents)
            {
                await EasyEventsAsyncStatic.OnTTSMessageUpdatedAsync(ttsArgs);
            }
        }

        public void OnTTSPropertyChanged(object sender, PropertyChangedEventArgs ttsPropArgs)
        {
            Debug.Log($"TTS Property Name == {ttsPropArgs.PropertyName}");
            // if(ttsPropArgs.PropertyName == "")
            // {
            // todo check documentation and experiment
            //}

        }

        #endregion

    }
}