using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;
using static UnityEngine.Application;

namespace EasyCodeForVivox
{
    public class DynamicAttributeEvents : MonoBehaviour
    {
        [SerializeField] private string cubeName;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

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
            gameObject.GetComponent<Renderer>().material.color = Color.green;
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

        [ChannelMessageEvent(ChannelMessageStatus.ChannelMessageSent)]
        public void ChannelMessageSentEventWithDynamicObject(DynamicEventModel dynamicEventModel)
        {
            Debug.Log($"A Message has been sent. Do some work");
            Debug.Log($"Recieved a message from {dynamicEventModel.Name} with Player Id {dynamicEventModel.PlayerId}");
            Debug.Log($"Message : {dynamicEventModel.Message}");
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

        [DirectMessageEvent(DirectMessageStatus.DirectMessageSent)]
        public void DirectMessageSentEventWithDynamicObject(DynamicEventModel dynamicEventModel)
        {
            Debug.Log($"A Direct Message has been sent. Do some work");
            Debug.Log($"Recieved a message from {dynamicEventModel.Name} with Player Id {dynamicEventModel.PlayerId}");
            Debug.Log($"Message : {dynamicEventModel.Message}");
        }

        [UserEvent(UserStatus.UserJoinedChannel)]
        public void UserHasJoinedChannel(IParticipant participant)
        {
            Debug.Log($"User {participant.Account.Name} has joined this channel");
        }

        [UserAudioEvent(UserAudioStatus.UserMuted)]
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
