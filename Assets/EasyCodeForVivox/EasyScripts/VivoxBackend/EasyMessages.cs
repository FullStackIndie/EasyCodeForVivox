using EasyCodeForVivox.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyMessages : IMessages
    {
        private readonly EasyEvents _events;
        private readonly EasyEventsAsync _eventsAsync;

        public EasyMessages(EasyEventsAsync eventsAsync, EasyEvents events)
        {
            _eventsAsync = eventsAsync;
            _events = events;
        }

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



        #region Channel - Text Methods


        public void SendChannelMessage(IChannelSession channel, string inputMsg)
        {
            if (channel.TextState == ConnectionState.Disconnected)
            {
                return;
            }
            channel.BeginSendText(inputMsg, async ar =>
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
                    _events.OnChannelMessageSent();
                    await _eventsAsync.OnChannelMessageSentAsync();
                }
            });
        }

        public void SendChannelMessage<T>(IChannelSession channel, string inputMsg, T value)
        {
            if (channel.TextState == ConnectionState.Disconnected)
            {
                return;
            }
            channel.BeginSendText(inputMsg, async ar =>
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
                    _events.OnChannelMessageSent(value);
                    await _eventsAsync.OnChannelMessageSentAsync(value);
                }
            });
        }

        public void SendChannelMessage(IChannelSession channel, string inputMsg, string stanzaNameSpace, string stanzaBody)
        {
            if (channel.TextState == ConnectionState.Disconnected)
            {
                return;
            }

            channel.BeginSendText(null, inputMsg, stanzaNameSpace, stanzaBody, async ar =>
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
                    _events.OnChannelMessageSent();
                    await _eventsAsync.OnChannelMessageSentAsync();
                }
            });
        }

        public void SendChannelMessage<T>(IChannelSession channel, string inputMsg, T value, string stanzaNameSpace, string stanzaBody)
        {
            if (channel.TextState == ConnectionState.Disconnected)
            {
                return;
            }

            channel.BeginSendText(null, inputMsg, stanzaNameSpace, stanzaBody, async ar =>
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
                    _events.OnChannelMessageSent(value);
                    await _eventsAsync.OnChannelMessageSentAsync(value);
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

            loginSession.BeginSendDirectedMessage(targetAccountID, null, message, null, null, async ar =>
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
                    _events.OnDirectMessageSent();
                    await _eventsAsync.OnDirectMessageSentAsync();
                }
            });
        }

        public void SendDirectMessage<T>(ILoginSession loginSession, string targetID, string message, T value, string stanzaNameSpace = null, string stanzaBody = null)
        {
            var targetAccountID = new AccountId(loginSession.LoginSessionId.Issuer, targetID, loginSession.LoginSessionId.Domain);

            loginSession.BeginSendDirectedMessage(targetAccountID, null, message, null, null, async ar =>
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
                    _events.OnDirectMessageSent(value);
                    await _eventsAsync.OnDirectMessageSentAsync(value);
                }
            });
        }

        public void SendDirectMessage(ILoginSession login, Dictionary<string, string> attemptedDirectMessages, string targetID, string message, string stanzaNameSpace = null, string stanzaBody = null)
        {
            var targetAccountID = new AccountId(login.LoginSessionId.Issuer, targetID, login.LoginSessionId.Domain);
            login.BeginSendDirectedMessage(targetAccountID, null, message, stanzaNameSpace, stanzaBody, async ar =>
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
                    _events.OnDirectMessageSent();
                    await _eventsAsync.OnDirectMessageSentAsync();
                }
                // todo add Coroutine that will attempt to resend failed messages
                // provide opt-in option so user can use my implementaion or there own implementaion
                attemptedDirectMessages.Add(login.DirectedMessageResult.RequestId,
                    message);
            });
        }

        public void SendDirectMessage<T>(ILoginSession login, Dictionary<string, string> attemptedDirectMessages, string targetID, string message, T value, string stanzaNameSpace = null, string stanzaBody = null)
        {
            var targetAccountID = new AccountId(login.LoginSessionId.Issuer, targetID, login.LoginSessionId.Domain);
            login.BeginSendDirectedMessage(targetAccountID, null, message, stanzaNameSpace, stanzaBody, async ar =>
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
                    _events.OnDirectMessageSent(value);
                    await _eventsAsync.OnDirectMessageSentAsync(value);
                }
                // todo add Coroutine that will attempt to resend failed messages
                // provide opt-in option so user can use my implementaion or there own implementaion
                attemptedDirectMessages.Add(login.DirectedMessageResult.RequestId,
                    message);
            });
        }

        #endregion


        #region Message Callbacks


        public async void OnDirectMessageRecieved(object sender, QueueItemAddedEventArgs<IDirectedTextMessage> directMessage)
        {
            var directedMsgs = (IReadOnlyQueue<IDirectedTextMessage>)sender;


            while (directedMsgs.Count > 0)
            {
                var msg = directedMsgs.Dequeue();
                if (msg != null)
                {
                    _events.OnDirectMessageRecieved(directMessage.Value);
                    await _eventsAsync.OnDirectMessageRecievedAsync(directMessage.Value);
                }
            }
        }

        public async void OnDirectMessageFailedCallback(object sender, QueueItemAddedEventArgs<IFailedDirectedTextMessage> failedMessage)
        {
            var failed = (IReadOnlyQueue<IFailedDirectedTextMessage>)sender;
            while (failed.Count > 0)
            {
                var msg = failed.Dequeue();
                if (msg != null)
                {
                    _events.OnDirectMessageFailed(msg);
                    await _eventsAsync.OnDirectMessageFailedAsync(msg);
                }
            }
        }

        public async void OnChannelMessageRecieved(object sender, QueueItemAddedEventArgs<IChannelTextMessage> channelMessage)
        {
            var messages = (IReadOnlyQueue<IChannelTextMessage>)sender;
            while (messages.Count > 0)
            {
                var msg = messages.Dequeue();
                if (msg != null)
                {
                    if (!string.IsNullOrEmpty(msg.ApplicationStanzaNamespace))
                    {
                        if (msg.ApplicationStanzaNamespace.Contains("Event"))
                        {
                            _events.OnEventMessageRecieved(msg);
                            await _eventsAsync.OnEventMessageRecievedAsync(msg);
                            return;
                        }
                    }
                    _events.OnChannelMessageRecieved(msg);
                    await _eventsAsync.OnChannelMessageRecievedAsync(msg);
                }
            }
        }

        #endregion



    }

}