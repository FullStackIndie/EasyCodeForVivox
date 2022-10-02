using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyTextToSpeech
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


        private void OnTTSMessageAdded(object sender, ITTSMessageQueueEventArgs ttsArgs)
        {
            var source = (ITTSMessageQueue)sender;
            if (source.Count > 9)
            {
                 // todo update and research docs
                Debug.Log("Cant keep over 10 messages in Queue");
            }
            EasyEvents.OnTTSMessageAdded(ttsArgs);
        }

        private void OnTTSMessageRemoved(object sender, ITTSMessageQueueEventArgs ttsArgs)
        {
            var source = (ITTSMessageQueue)sender;
            if (source.Count >= 9)
            {
                Debug.Log("Cant keep over 10 messages in Queue");
            }
            EasyEvents.OnTTSMessageRemoved(ttsArgs);
        }

        private void OnTTSMessageUpdated(object sender, ITTSMessageQueueEventArgs ttsArgs)
        {
            var source = (ITTSMessageQueue)sender;
            if (source.Count >= 9)
            {
                Debug.Log("Cant keep over 10 messages in Queue");
            }
            EasyEvents.OnTTSMessageUpdated(ttsArgs);
        }

        private void OnTTSPropertyChanged(object sender, PropertyChangedEventArgs ttsPropArgs)
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