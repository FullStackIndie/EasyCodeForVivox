using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;


namespace EasyCodeForVivox.Events
{
    public static class EasyEventsAsync
    {

        private static async Task InvokeMethodsAsync(Enum eventKey)
        {
            if (!RuntimeEvents.DynamicEvents.ContainsKey(eventKey))
            {
                return;
            }
            foreach (var method in RuntimeEvents.DynamicEvents[eventKey])
            {
                var parameters = method.GetParameters();
                if (parameters.Length != 0)
                {
                    continue;
                }
                var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                await Task.Run(() =>
                {
                    Parallel.ForEach(gameObjects, gameObject =>
                    {
                        method?.Invoke(gameObject, null);
                    });
                });
            }
        }

        private static async Task InvokeMethodsAsync<T>(Enum eventKey, T value)
        {
            if (!RuntimeEvents.DynamicEvents.ContainsKey(eventKey))
            {
                return;
            }
            foreach (var method in RuntimeEvents.DynamicEvents[eventKey])
            {
                var parameters = method.GetParameters();
                if (parameters.Length != 1 || parameters[0].ParameterType != typeof(T))
                {
                    continue;
                }
                var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                await Task.Run(() =>
                {
                    Parallel.ForEach(gameObjects, gameObject =>
                    {
                        method?.Invoke(gameObject, new object[] { value });
                    });
                });
            }
        }

        private static async Task InvokeMethodsAsync<T1, T2>(Enum eventKey, T1 value1, T2 value2)
        {
            if (!RuntimeEvents.DynamicEvents.ContainsKey(eventKey))
            {
                return;
            }
            foreach (var method in RuntimeEvents.DynamicEvents[eventKey])
            {
                var parameters = method.GetParameters();
                if (parameters.Length != 2 || parameters[0].ParameterType != typeof(T1) && parameters[1].ParameterType != typeof(T2))
                {
                    continue;
                }
                var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                await Task.Run(() =>
                {
                    Parallel.ForEach(gameObjects, gameObject =>
                    {
                        method?.Invoke(gameObject, new object[] { value1, value2 });
                    });
                });
            }
        }

        public static async Task OnLoggingInAsync(ILoginSession loginSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(LoginStatusAsync.LoggingInAsync, loginSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnLoggedInAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnLoggingInAsync<T>(ILoginSession loginSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(LoginStatusAsync.LoggingInAsync, loginSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnLoggedInAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnLoggedInAsync(ILoginSession loginSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(LoginStatusAsync.LoggedInAsync, loginSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnLoggedInAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnLoggingOutAsync(ILoginSession loginSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(LoginStatusAsync.LoggingOutAsync, loginSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnLoggingOutAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnLoggedOutAsync(ILoginSession loginSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(LoginStatusAsync.LoggedOutAsync, loginSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnLoggedOutAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        #region Channel Event Methods


        public static async Task OnChannelConnectingAsync(IChannelSession channelSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelConnectingAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelConnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnChannelConnectingAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelConnectingAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelConnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnChannelConnectedAsync(IChannelSession channelSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelConnectedAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelConnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnChannelConnectedAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelConnectedAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelConnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnChannelDisconnectingAsync(IChannelSession channelSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelDisconnectingAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelDisconnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnChannelDisconnectingAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelDisconnectingAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelDisconnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnChannelDisconnectedAsync(IChannelSession channelSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelDisconnectedAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelDisconnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnChannelDisconnectedAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelDisconnectedAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelDisconnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Text Channel Events


        public static async Task OnTextChannelConnectingAsync(IChannelSession channelSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelConnectingAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelConnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnTextChannelConnectingAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelConnectingAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelConnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        public static async Task OnTextChannelConnectedAsync(IChannelSession channelSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelConnectedAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelConnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnTextChannelConnectedAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelConnectedAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelConnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnTextChannelDisconnectingAsync(IChannelSession channelSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelDisconnectingAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelDisconnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnTextChannelDisconnectingAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelDisconnectingAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelDisconnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnTextChannelDisconnectedAsync(IChannelSession channelSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelDisconnectedAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelDisconnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnTextChannelDisconnectedAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelDisconnectedAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelDisconnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Voice Channel Events


        public static async Task OnAudioChannelConnectingAsync(IChannelSession channelSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelConnectingAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelConnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnAudioChannelConnectingAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelConnectingAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelConnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnAudioChannelConnectedAsync(IChannelSession channelSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelConnectedAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelConnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnAudioChannelConnectedAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelConnectedAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelConnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnAudioChannelDisconnectingAsync(IChannelSession channelSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelDisconnectingAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelDisconnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnAudioChannelDisconnectingAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelDisconnectingAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelDisconnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnAudioChannelDisconnectedAsync(IChannelSession channelSession)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelDisconnectedAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelDisconnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnAudioChannelDisconnectedAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelDisconnectedAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelDisconnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Message Events


        public static async Task OnChannelMessageRecievedAsync(IChannelTextMessage channelTextMessage)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelMessageStatusAsync.ChannelMessageRecievedAsync, channelTextMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelMessageRecievedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnChannelMessageRecievedAsync<T>(IChannelTextMessage channelTextMessage, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelMessageStatusAsync.ChannelMessageRecievedAsync, channelTextMessage, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelMessageRecievedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnEventMessageRecievedAsync(IChannelTextMessage channelTextMessage)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelMessageStatusAsync.EventMessageRecievedAsync, channelTextMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnEventMessageRecievedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnEventMessageRecievedAsync<T>(IChannelTextMessage channelTextMessage, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelMessageStatusAsync.EventMessageRecievedAsync, channelTextMessage, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnEventMessageRecievedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnChannelMessageSentAsync()
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelMessageStatusAsync.ChannelMessageSentAsync);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelMessageSentAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnChannelMessageSentAsync<T>(T value) where T : class
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelMessageStatusAsync.ChannelMessageSentAsync, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelMessageSentAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnDirectMessageSentAsync()
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(DirectMessageStatusAsync.DirectMessageSentAsync);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnDirectMessageSentAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnDirectMessageSentAsync<T>(T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(DirectMessageStatusAsync.DirectMessageSentAsync, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnDirectMessageSentAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnDirectMessageRecievedAsync(IDirectedTextMessage message)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(DirectMessageStatusAsync.DirectMessageRecievedAsync, message);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnDirectMessageRecievedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnDirectMessageRecievedAsync<T>(IDirectedTextMessage message, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(DirectMessageStatusAsync.DirectMessageRecievedAsync, message, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnDirectMessageRecievedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnDirectMessageFailedAsync(IFailedDirectedTextMessage failedMessage)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(DirectMessageStatusAsync.DirectMessageFailedAsync, failedMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnDirectMessageFailedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnDirectMessageFailedAsync<T>(IFailedDirectedTextMessage failedMessage, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(DirectMessageStatusAsync.DirectMessageFailedAsync, failedMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnDirectMessageFailedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Participant/User Events


        public static async Task OnUserJoinedChannelAsync(IParticipant participant)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserStatusAsync.UserJoinedChannelAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserJoinedChannelAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnUserJoinedChannelAsync<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserStatusAsync.UserJoinedChannelAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserJoinedChannelAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnUserLeftChannelAsync(IParticipant participant)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserStatusAsync.UserLeftChannelAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserLeftChannelAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnUserLeftChannelAsync<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserStatusAsync.UserLeftChannelAsync, participant, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserLeftChannelAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnUserValuesUpdatedAsync(IParticipant participant)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserStatusAsync.UserValuesUpdatedAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserValuesUpdatedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnUserValuesUpdatedAsync<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserStatusAsync.UserValuesUpdatedAsync, participant, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserValuesUpdatedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }



        public static async Task OnUserMutedAsync(IParticipant participant)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserMutedAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserMutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnUserMutedAsync<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserMutedAsync, participant, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserMutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnUserUnmutedAsync(IParticipant participant)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserUnmutedAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserUnmutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnUserUnmutedAsync<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserUnmutedAsync, participant, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserUnmutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnUserSpeakingAsync(IParticipant participant)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserSpeakingAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserSpeakingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnUserSpeakingAsync<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserSpeakingAsync, participant, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserSpeakingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnUserNotSpeakingAsync(IParticipant participant)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserNotSpeakingAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserNotSpeakingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnUserNotSpeakingAsync<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserNotSpeakingAsync, participant, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserNotSpeakingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region User Mute Events

        public static async Task OnLocalUserMutedAsync(bool isMuted)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.LocalUserMutedAsync, isMuted);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLocalUserMutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnLocalUserMutedAsync<T>(bool isMuted, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.LocalUserMutedAsync, isMuted, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLocalUserMutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnLocalUserUnmutedAsync(bool isMuted)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.LocalUserUnmutedAsync, isMuted);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLocalUserUnmutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnLocalUserUnmutedAsync<T>(bool isMuted, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.LocalUserUnmutedAsync, isMuted, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLocalUserUnmutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Text-to-Speech Events


        public static async Task OnTTSMessageAddedAsync(ITTSMessageQueueEventArgs ttsArgs)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextToSpeechStatusAsync.TTSMessageAddedAsync, ttsArgs);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTTSMessageAddedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnTTSMessageAddedAsync<T>(ITTSMessageQueueEventArgs ttsArgs, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextToSpeechStatusAsync.TTSMessageAddedAsync, ttsArgs, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTTSMessageAddedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnTTSMessageRemovedAsync(ITTSMessageQueueEventArgs ttsArgs)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextToSpeechStatusAsync.TTSMessageRemovedAsync, ttsArgs);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTTSMessageRemovedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnTTSMessageRemovedAsync<T>(ITTSMessageQueueEventArgs ttsArgs, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextToSpeechStatusAsync.TTSMessageRemovedAsync, ttsArgs, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTTSMessageRemovedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnTTSMessageUpdatedAsync(ITTSMessageQueueEventArgs ttsArgs)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextToSpeechStatusAsync.TTSMessageUpdatedAsync, ttsArgs);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTTSMessageUpdatedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static async Task OnTTSMessageUpdatedAsync<T>(ITTSMessageQueueEventArgs ttsArgs, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextToSpeechStatusAsync.TTSMessageUpdatedAsync, ttsArgs, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTTSMessageUpdatedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


    }
}

