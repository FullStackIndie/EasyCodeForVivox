using EasyCodeForVivox.Events;
using EasyCodeForVivox.Events.Internal;
using EasyCodeForVivox.Extensions;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyTextToSpeech : ITextToSpeech
    {
        private readonly EasyEvents _events;
        private readonly EasyEventsAsync _eventsAsync;

        public EasyTextToSpeech(EasyEvents events, EasyEventsAsync eventsAsync)
        {
            _events = events;
            _eventsAsync = eventsAsync;
        }

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
                    message.TTSMsgLocalPlayOverCurrent(loginSession);
                    break;
                case TTSDestination.RemoteTransmission:
                    message.TTSMsgLocalRemotePlayOverCurrent(loginSession);
                    break;
                case TTSDestination.RemoteTransmissionWithLocalPlayback:
                    message.TTSMsgLocalRemotePlayOverCurrent(loginSession);
                    break;
                case TTSDestination.QueuedLocalPlayback:
                    message.TTSMsgQueueLocal(loginSession);
                    break;
                case TTSDestination.QueuedRemoteTransmission:
                    message.TTSMsgQueueRemote(loginSession);
                    break;
                case TTSDestination.QueuedRemoteTransmissionWithLocalPlayback:
                    message.TTSMsgQueueRemoteLocal(loginSession);
                    break;
                case TTSDestination.ScreenReader:
                    message.TTSMsgLocalReplaceCurrentPlaying(loginSession);
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
            _events.OnTTSMessageAdded(ttsArgs);
            await _eventsAsync.OnTTSMessageAddedAsync(ttsArgs);
        }

        public async void OnTTSMessageRemoved(object sender, ITTSMessageQueueEventArgs ttsArgs)
        {
            var source = (ITTSMessageQueue)sender;
            if (source.Count >= 9)
            {
                Debug.Log("Cant keep over 10 messages in Queue");
            }
            _events.OnTTSMessageRemoved(ttsArgs);
            await _eventsAsync.OnTTSMessageRemovedAsync(ttsArgs);
        }

        public async void OnTTSMessageUpdated(object sender, ITTSMessageQueueEventArgs ttsArgs)
        {
            var source = (ITTSMessageQueue)sender;
            if (source.Count >= 9)
            {
                Debug.Log("Cant keep over 10 messages in Queue");
            }
            _events.OnTTSMessageUpdated(ttsArgs);
            await _eventsAsync.OnTTSMessageUpdatedAsync(ttsArgs);
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