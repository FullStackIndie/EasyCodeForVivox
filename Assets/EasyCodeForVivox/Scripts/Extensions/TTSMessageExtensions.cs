using System.Collections;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox.Extensions
{
    public static class TTSMessageExtensions
    {

        public static IEnumerator WaitForMessage(ILoginSession loginSession, TTSMessage ttsMessage)
        {
            Debug.Log("TTS Message Queue is full, Waiting until count is below 9 to add message to the queue");
            yield return new WaitUntil(() => loginSession.TTS.Messages.Count < 9);
            loginSession.TTS.Speak(ttsMessage);
        }

        /// <summary>
        /// Play this message locally and override current playing TTS message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTSMsgLocalPlayOverCurrent(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.LocalPlayback);
            if (loginSession != null && loginSession.TTS.Messages.Count < 10)
            {
                loginSession.TTS.Speak(msg);
            }
            else
            {
                WaitForMessage(loginSession, msg);
            }
        }

        /// <summary>
        /// Play this message remotely and override current playing TTS message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTSMsgRemotePlayOverCurrent(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.RemoteTransmission);
            if (loginSession != null && loginSession.TTS.Messages.Count < 10)
            {
                loginSession.TTS.Speak(msg);
            }
            else
            {
                WaitForMessage(loginSession, msg);
            }
        }

        /// <summary>
        /// Play this message locally and remotely and ovverride current playing TTS message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTSMsgLocalRemotePlayOverCurrent(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.RemoteTransmissionWithLocalPlayback);
            if (loginSession != null && loginSession.TTS.Messages.Count < 10)
            {
                loginSession.TTS.Speak(msg);
            }
            else
            {
                WaitForMessage(loginSession, msg);
            }
        }

        /// <summary>
        /// Replace current playing TTS message with this message locally
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTSMsgLocalReplaceCurrentMessagePlaying(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.ScreenReader);
            if (loginSession != null && loginSession.TTS.Messages.Count < 10)
            {
                loginSession.TTS.Speak(msg);
            }
            else
            {
                WaitForMessage(loginSession, msg);
            }
        }


        /// <summary>
        /// Play TTS message locally, adds to current queue if a message is already playing
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTSMsgQueueLocal(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.QueuedLocalPlayback);
            if (loginSession != null && loginSession.TTS.Messages.Count < 10)
            {
                loginSession.TTS.Speak(msg);
            }
            else
            {
                WaitForMessage(loginSession, msg);
            }
        }

        /// <summary>
        /// Play TTS message remotely, adds to current queue if a message is already playing
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTSMsgQueueRemote(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.QueuedRemoteTransmission);
            if (loginSession != null && loginSession.TTS.Messages.Count < 10)
            {
                loginSession.TTS.Speak(msg);
            }
            else
            {
                WaitForMessage(loginSession, msg);
            }
        }

        /// <summary>
        /// Play TTS message remotely and locally, adds to current queue if a message is already playing
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loginSession"></param>
        public static void TTSMsgQueueRemoteLocal(this string message, ILoginSession loginSession)
        {
            TTSMessage msg = new TTSMessage(message, TTSDestination.QueuedRemoteTransmissionWithLocalPlayback);
            if (loginSession != null && loginSession.TTS.Messages.Count < 10)
            {
                loginSession.TTS.Speak(msg);
            }
            else
            {
                WaitForMessage(loginSession, msg);
            }
        }


    }
}
