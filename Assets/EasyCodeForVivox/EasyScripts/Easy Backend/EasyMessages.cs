using System;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyMessages
    {
        public static event Action ChannelMesssageSent;
        public static event Action<IChannelTextMessage> ChannelMessageRecieved;
        public static event Action<IChannelTextMessage> EventMessageRecieved;

        public static event Action DirectMesssageSent;
        public static event Action<IDirectedTextMessage> DirectMessageRecieved;
        public static event Action<IFailedDirectedTextMessage> DirectMessageFailed;


        public void SubscribeToChannelMessages(IChannelSession channelSession)
        {
            channelSession.MessageLog.AfterItemAdded += OnChannelMessageRecieved;
        }

        public void SubscribeToDirectMessages(ILoginSession loginSession)
        {
            loginSession.DirectedMessages.AfterItemAdded += OnDirectMessageRecieved;
            loginSession.FailedDirectedMessages.AfterItemAdded += OnDirectMessageFailedCallback;
        }

        public void UnsubscribeFromChannelMessages(IChannelSession channelSession)
        {
            channelSession.MessageLog.AfterItemAdded -= OnChannelMessageRecieved;
        }

        public void UnsubscribeFromDirectMessages(ILoginSession loginSession)
        {
            loginSession.DirectedMessages.AfterItemAdded -= OnDirectMessageRecieved;
            loginSession.FailedDirectedMessages.AfterItemAdded -= OnDirectMessageFailedCallback;
        }



        #region Message Events


        private void OnChannelMessageRecieved(IChannelTextMessage channelTextMessage)
        {
            if (channelTextMessage != null)
            {
                ChannelMessageRecieved?.Invoke(channelTextMessage);
            }

        }

        private void OnEventMessageRecieved(IChannelTextMessage channelTextMessage)
        {
            if (channelTextMessage != null)
            {
                EventMessageRecieved?.Invoke(channelTextMessage);
            }

        }

        private void OnChannelMessageSent()
        {
            ChannelMesssageSent?.Invoke();
        }

        private void OnDirectMessageSent()
        {
            DirectMesssageSent?.Invoke();
        }

        private void OnDirectMessageRecieved(IDirectedTextMessage message)
        {
            if (message != null)
            {
                DirectMessageRecieved?.Invoke(message);
            }

        }

        private void OnDirectMessageFailed(IFailedDirectedTextMessage failedMessage)
        {
            if (failedMessage != null)
            {
                DirectMessageFailed?.Invoke(failedMessage);
            }

        }


        #endregion


        #region Channel - Text Methods


        public void SendChannelMessage(IChannelSession channel, string inputMsg, string stanzaNameSpace = "", string stanzaBody = "")
        {
            if (channel.TextState == ConnectionState.Disconnected)
            {
                return;
            }

            channel.BeginSendText(null, inputMsg, stanzaNameSpace, stanzaBody, ar =>
            {
                try
                {
                    channel.EndSendText(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    return;
                }
                finally
                {
                    OnChannelMessageSent();
                }
            });
        }

        public void SendEventMessage(IChannelSession channel, string eventMessage, string stanzaNameSpace, string stanzaBody)
        {
            if (channel.TextState == ConnectionState.Disconnected)
            {
                return;
            }

            channel.BeginSendText(null, eventMessage, stanzaNameSpace, stanzaBody, ar =>
            {
                try
                {
                    channel.EndSendText(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    return;
                }
            });
        }

        public void SendDirectMessage(ILoginSession loginSession, string targetID, string message, string stanzaNameSpace = null, string stanzaBody = null)
        {
            var targetAccountID = new AccountId(loginSession.LoginSessionId.Issuer, targetID, loginSession.LoginSessionId.Domain);
            Debug.Log(targetAccountID.Name);
            Debug.Log(targetAccountID.DisplayName);
            Debug.Log(targetAccountID.Issuer);
            Debug.Log(targetAccountID.Domain);
            loginSession.BeginSendDirectedMessage(targetAccountID, null, message, null, null, ar =>
            {
                try
                {
                    loginSession.EndSendDirectedMessage(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
                finally
                {
                    OnDirectMessageSent();
                }
            });
        }

        public void SendDirectMessage(ILoginSession login, Dictionary<string, string> attemptedDirectMessages, string targetID, string message, string stanzaNameSpace = null, string stanzaBody = null)
        {
            var targetAccountID = new AccountId(login.LoginSessionId.Issuer, targetID, login.LoginSessionId.Domain);
            login.BeginSendDirectedMessage(targetAccountID, null, message, stanzaNameSpace, stanzaBody, ar =>
            {
                try
                {
                    login.EndSendDirectedMessage(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
                finally
                {
                    OnDirectMessageSent();
                }
                attemptedDirectMessages.Add(login.DirectedMessageResult.RequestId,
                    message);
            });
        }

        [System.Obsolete] // Vivox is Updating this functionality
        private void ArchiveRequestQuery(ILoginSession login)
        {
            login.BeginAccountArchiveQuery(null, null, null, login.Key, null, 49, null, null, -1, ar =>
            {
                try
                {
                    Debug.Log(login.AccountArchiveResult.Running + "  is running");
                    login.EndAccountArchiveQuery(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }


        #endregion


        #region Message Callbacks


        private void OnDirectMessageRecieved(object sender, QueueItemAddedEventArgs<IDirectedTextMessage> directMessage)
        {
            var directedMsgs = (IReadOnlyQueue<IDirectedTextMessage>)sender;


            while (directedMsgs.Count > 0)
            {
                var msg = directedMsgs.Dequeue();
                if (msg != null)
                {
                    OnDirectMessageRecieved(directMessage.Value);
                }
            }
        }

        private void OnDirectMessageFailedCallback(object sender, QueueItemAddedEventArgs<IFailedDirectedTextMessage> failedMessage)
        {
            var failed = (IReadOnlyQueue<IFailedDirectedTextMessage>)sender;
            while (failed.Count > 0)
            {
                var msg = failed.Dequeue();
                if (msg != null)
                {
                    OnDirectMessageFailed(msg);
                }
            }
        }

        private void OnChannelMessageRecieved(object sender, QueueItemAddedEventArgs<IChannelTextMessage> channelMessage)
        {
            var messages = (IReadOnlyQueue<IChannelTextMessage>)sender;
            while (messages.Count > 0)
            {
                var msg = messages.Dequeue();
                if (msg != null)
                {
                    if (msg.ApplicationStanzaNamespace.Contains("Event"))
                    {
                        OnEventMessageRecieved(msg);
                    }
                    else
                    {
                        OnChannelMessageRecieved(msg);
                    }
                }
            }
        }


        #endregion


        [System.Obsolete] //  Vivox is Updating this functionality
        private void OnArchiveResultAdded(object sender, QueueItemAddedEventArgs<IAccountArchiveMessage> queueArchiveMsg)
        {

            var source = (IReadOnlyQueue<IAccountArchiveMessage>)sender;

            while (source.Count > 0)
            {
                Debug.Log(source.Dequeue().Message);
            }
        }
    }

}