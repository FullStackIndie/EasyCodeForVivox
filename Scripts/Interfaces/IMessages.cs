using System.Collections.Generic;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface IMessages
    {
        void SubscribeToChannelMessages(IChannelSession channelSession);
        void SubscribeToDirectMessages(ILoginSession loginSession);

        void UnsubscribeFromChannelMessages(IChannelSession channelSession);
        void UnsubscribeFromDirectMessages(ILoginSession loginSession);

        void SendChannelMessage(IChannelSession channel, string inputMsg);
        void SendChannelMessage<T>(IChannelSession channel, string inputMsg, T value);

        void SendChannelMessage(IChannelSession channel, string inputMsg, string stanzaNameSpace, string stanzaBody);
        void SendChannelMessage<T>(IChannelSession channel, string inputMsg, T value, string stanzaNameSpace, string stanzaBody);

        void SendEventMessage(IChannelSession channel, string eventMessage, string stanzaNameSpace, string stanzaBody);

        void SendDirectMessage(ILoginSession loginSession, string targetID, string message, string stanzaNameSpace = null, string stanzaBody = null);
        void SendDirectMessage<T>(ILoginSession loginSession, string targetID, string message, T value, string stanzaNameSpace = null, string stanzaBody = null);

        void SendDirectMessage(ILoginSession login, Dictionary<string, string> attemptedDirectMessages, string targetID, string message, string stanzaNameSpace = null, string stanzaBody = null);
        void SendDirectMessage<T>(ILoginSession login, Dictionary<string, string> attemptedDirectMessages, string targetID, string message, T value, string stanzaNameSpace = null, string stanzaBody = null);

        void OnDirectMessageRecieved(object sender, QueueItemAddedEventArgs<IDirectedTextMessage> directMessage);
        void OnDirectMessageFailedCallback(object sender, QueueItemAddedEventArgs<IFailedDirectedTextMessage> failedMessage);
        void OnChannelMessageRecieved(object sender, QueueItemAddedEventArgs<IChannelTextMessage> channelMessage);
    }
}
