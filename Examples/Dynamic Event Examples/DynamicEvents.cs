﻿using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class DynamicEvents : MonoBehaviour
    {
        [SerializeField] private string cubeName;
        [SerializeField] private GameObject cube;

        [LoginEvent(LoginStatus.LoggingIn)]
        public void PlayerLoggingIn(ILoginSession loginSession)
        {
            Debug.Log($"Invoking Synchronous Event Dynamically from {nameof(PlayerLoggingIn)}");
        }

        [LoginEvent(LoginStatus.LoggedIn)]
        public void PlayerLoggedIn(ILoginSession loginSession)
        {
            // handle some UI logic
            cubeName = loginSession.LoginSessionId.Name;
            cube.GetComponent<Renderer>().material.color = Color.green;
        }

        [ChannelEvent(ChannelStatus.ChannelConnected)]
        public static void PlayerJoinedChannel(IChannelSession channelSession)
        {
            Debug.Log($"A Player has joined {channelSession.Channel.Name}");
        }

        [AudioChannelEvent(AudioChannelStatus.AudioChannelConnected)]
        public static void PlayerJoinedAudioChannel(IChannelSession channelSession)
        {
            Debug.Log($"A Player has joined Audio in channel {channelSession.Channel.Name}");
        }

        [TextChannelEvent(TextChannelStatus.TextChannelConnected)]
        public static void PlayerJoinedTextChannel(IChannelSession channelSession)
        {
            Debug.Log($"A Player has joined Text in channel {channelSession.Channel.Name}");
        }

        [ChannelMessageEvent(ChannelMessageStatus.ChannelMessageRecieved)]
        public void ChannelMessageSentEvent(IChannelTextMessage channelTextMessage)
        {
            Debug.Log($"A Message has been received from {channelTextMessage.Sender.Name} : {channelTextMessage.Message}");
        }

        [ChannelMessageEvent(ChannelMessageStatus.ChannelMessageSent)]
        public void ChannelMessageSentEvent()
        {
            Debug.Log($"A Message has been sent. Do some work");
        }

        [DirectMessageEvent(DirectMessageStatus.DirectMessageRecieved)]
        public void DirectMessageRecievedEvent(IDirectedTextMessage directedTextMessage)
        {
            Debug.Log($"A Direct Message has been recieved from {directedTextMessage.Sender.Name} : {directedTextMessage.Message}");
        }

        [DirectMessageEvent(DirectMessageStatus.DirectMessageSent)]
        public void DirectMessageSentEvent()
        {
            Debug.Log($"A Direct Message has been sent. Do some work");
        }


        [UserEvent(UserStatus.UserJoinedChannel)]
        public void UserHasJoinedChannel(IParticipant participant)
        {
            Debug.Log($"User {participant.Account.Name} has joined this channel");
        }

        [UserEvent(UserStatus.UserMuted)]
        public void UserHasBeenMuted(IParticipant participant)
        {
            Debug.Log($"User {participant.Account.Name} has been muted");
        }

        [TextToSpeechEvent(TextToSpeechStatus.TTSMessageAdded)]
        public void TextToSpeechMessageHasBeenAddedToTheQueue(ITTSMessageQueueEventArgs messageQueue)
        {
            Debug.Log($"TTS Message : [ {messageQueue.Message.Text} ] has been added to the Message Queue");
        }
    }
}