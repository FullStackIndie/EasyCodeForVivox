using EasyCodeForVivox.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyMessages : IMessages
    {

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
                    EasyEventsStatic.OnChannelMessageSent();
                    if (EasySessionStatic.UseDynamicEvents)
                    {
                        await EasyEventsAsyncStatic.OnChannelMessageSentAsync();
                    }
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
                    EasyEventsStatic.OnChannelMessageSent();
                    if (EasySessionStatic.UseDynamicEvents)
                    {
                        await EasyEventsAsyncStatic.OnChannelMessageSentAsync();
                    }
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
                    EasyEventsStatic.OnDirectMessageSent();
                    if (EasySessionStatic.UseDynamicEvents)
                    {
                        await EasyEventsAsyncStatic.OnDirectMessageSentAsync();
                    }
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
                    EasyEventsStatic.OnDirectMessageSent();
                    if (EasySessionStatic.UseDynamicEvents)
                    {
                        await EasyEventsAsyncStatic.OnDirectMessageSentAsync();
                    }
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
                    EasyEventsStatic.OnDirectMessageRecieved(directMessage.Value);
                    if (EasySessionStatic.UseDynamicEvents)
                    {
                        await EasyEventsAsyncStatic.OnDirectMessageRecievedAsync(directMessage.Value);
                    }
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
                    EasyEventsStatic.OnDirectMessageFailed(msg);
                    if (EasySessionStatic.UseDynamicEvents)
                    {
                        await EasyEventsAsyncStatic.OnDirectMessageFailedAsync(msg);
                    }
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
                            EasyEventsStatic.OnEventMessageRecieved(msg);
                            if (EasySessionStatic.UseDynamicEvents)
                            {
                                await EasyEventsAsyncStatic.OnEventMessageRecievedAsync(msg);
                            }
                            return;
                        }
                    }
                    EasyEventsStatic.OnChannelMessageRecieved(msg);
                    if (EasySessionStatic.UseDynamicEvents)
                    {
                        await EasyEventsAsyncStatic.OnChannelMessageRecievedAsync(msg);
                    }
                }
            }
        }

        #endregion



    }

}