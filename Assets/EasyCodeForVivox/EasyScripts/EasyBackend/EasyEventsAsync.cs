using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;


namespace EasyCodeForVivox.Events
{
    public static class EasyEventsAsync
    {

        private static async Task InvokeMethodsAsync(this List<MethodInfo> methods)
        {
            foreach (var method in methods)
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

        private static async Task InvokeMethodsAsync<T>(this List<MethodInfo> methods, T value)
        {
            foreach (var method in methods)
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

        private static async Task InvokeMethodsAsync<T1, T2>(this List<MethodInfo> methods, T1 value1, T2 value2)
        {
            foreach (var method in methods)
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
                    await RuntimeEvents.DynamicEvents[LoginStatusAsync.LoggingInAsync].InvokeMethodsAsync(loginSession);
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
                    await RuntimeEvents.DynamicEvents[LoginStatusAsync.LoggingInAsync].InvokeMethodsAsync(loginSession, value);
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
                    await RuntimeEvents.DynamicEvents[LoginStatusAsync.LoggedInAsync].InvokeMethodsAsync(loginSession);
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
                    await RuntimeEvents.DynamicEvents[LoginStatusAsync.LoggingOutAsync].InvokeMethodsAsync(loginSession);
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
                    await RuntimeEvents.DynamicEvents[LoginStatusAsync.LoggedOutAsync].InvokeMethodsAsync(loginSession);
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
                    await RuntimeEvents.DynamicEvents[ChannelStatusAsync.ChannelConnectingAsync].InvokeMethodsAsync(channelSession);
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
                    await RuntimeEvents.DynamicEvents[ChannelStatusAsync.ChannelConnectingAsync].InvokeMethodsAsync(channelSession, value);
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
                    await RuntimeEvents.DynamicEvents[ChannelStatusAsync.ChannelConnectedAsync].InvokeMethodsAsync(channelSession);
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
                    await RuntimeEvents.DynamicEvents[ChannelStatusAsync.ChannelConnectedAsync].InvokeMethodsAsync(channelSession, value);
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
                    await RuntimeEvents.DynamicEvents[ChannelStatusAsync.ChannelDisconnectingAsync].InvokeMethodsAsync(channelSession);
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
                    await RuntimeEvents.DynamicEvents[ChannelStatusAsync.ChannelDisconnectingAsync].InvokeMethodsAsync(channelSession, value);
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
                    await RuntimeEvents.DynamicEvents[ChannelStatusAsync.ChannelDisconnectedAsync].InvokeMethodsAsync(channelSession);
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
                    await RuntimeEvents.DynamicEvents[ChannelStatusAsync.ChannelDisconnectedAsync].InvokeMethodsAsync(channelSession, value);
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
                    await RuntimeEvents.DynamicEvents[TextChannelStatusAsync.TextChannelConnectingAsync].InvokeMethodsAsync(channelSession);
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
                    await RuntimeEvents.DynamicEvents[TextChannelStatusAsync.TextChannelConnectingAsync].InvokeMethodsAsync(channelSession, value);
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
                    await RuntimeEvents.DynamicEvents[TextChannelStatusAsync.TextChannelConnectedAsync].InvokeMethodsAsync(channelSession);
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
                    await RuntimeEvents.DynamicEvents[TextChannelStatusAsync.TextChannelConnectedAsync].InvokeMethodsAsync(channelSession, value);
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
                    await RuntimeEvents.DynamicEvents[TextChannelStatusAsync.TextChannelDisconnectingAsync].InvokeMethodsAsync(channelSession);
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
                    await RuntimeEvents.DynamicEvents[TextChannelStatusAsync.TextChannelDisconnectingAsync].InvokeMethodsAsync(channelSession, value);
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
                    await RuntimeEvents.DynamicEvents[TextChannelStatusAsync.TextChannelDisconnectedAsync].InvokeMethodsAsync(channelSession);
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
                    await RuntimeEvents.DynamicEvents[TextChannelStatusAsync.TextChannelDisconnectedAsync].InvokeMethodsAsync(channelSession, value);
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
                    await RuntimeEvents.DynamicEvents[AudioChannelStatusAsync.AudioChannelConnectingAsync].InvokeMethodsAsync(channelSession);
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
                    await RuntimeEvents.DynamicEvents[AudioChannelStatusAsync.AudioChannelConnectingAsync].InvokeMethodsAsync(channelSession, value);
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
                    await RuntimeEvents.DynamicEvents[AudioChannelStatusAsync.AudioChannelConnectedAsync].InvokeMethodsAsync(channelSession);
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
                    await RuntimeEvents.DynamicEvents[AudioChannelStatusAsync.AudioChannelConnectedAsync].InvokeMethodsAsync(channelSession, value);
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
                    await RuntimeEvents.DynamicEvents[AudioChannelStatusAsync.AudioChannelDisconnectingAsync].InvokeMethodsAsync(channelSession);
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
                    await RuntimeEvents.DynamicEvents[AudioChannelStatusAsync.AudioChannelDisconnectingAsync].InvokeMethodsAsync(channelSession, value);
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
                    await RuntimeEvents.DynamicEvents[AudioChannelStatusAsync.AudioChannelDisconnectedAsync].InvokeMethodsAsync(channelSession);
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
                    await RuntimeEvents.DynamicEvents[AudioChannelStatusAsync.AudioChannelDisconnectedAsync].InvokeMethodsAsync(channelSession, value);
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
                    await RuntimeEvents.DynamicEvents[ChannelMessageStatusAsync.ChannelMessageRecievedAsync].InvokeMethodsAsync(channelTextMessage);
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
                    await RuntimeEvents.DynamicEvents[ChannelMessageStatusAsync.ChannelMessageRecievedAsync].InvokeMethodsAsync(channelTextMessage, value);
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
                    await RuntimeEvents.DynamicEvents[ChannelMessageStatusAsync.EventMessageRecievedAsync].InvokeMethodsAsync(channelTextMessage);
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
                    await RuntimeEvents.DynamicEvents[ChannelMessageStatusAsync.EventMessageRecievedAsync].InvokeMethodsAsync(channelTextMessage, value);
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
                    await RuntimeEvents.DynamicEvents[ChannelMessageStatusAsync.ChannelMessageSentAsync].InvokeMethodsAsync();
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
                    await RuntimeEvents.DynamicEvents[ChannelMessageStatusAsync.ChannelMessageSentAsync].InvokeMethodsAsync(value);
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
                    await RuntimeEvents.DynamicEvents[DirectMessageStatusAsync.DirectMessageSentAsync].InvokeMethodsAsync();
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
                    await RuntimeEvents.DynamicEvents[DirectMessageStatusAsync.DirectMessageSentAsync].InvokeMethodsAsync(value);
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
                    await RuntimeEvents.DynamicEvents[DirectMessageStatusAsync.DirectMessageRecievedAsync].InvokeMethodsAsync(message);
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
                    await RuntimeEvents.DynamicEvents[DirectMessageStatusAsync.DirectMessageRecievedAsync].InvokeMethodsAsync(message, value);
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
                    await RuntimeEvents.DynamicEvents[DirectMessageStatusAsync.DirectMessageFailedAsync].InvokeMethodsAsync(failedMessage);
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
                    await RuntimeEvents.DynamicEvents[DirectMessageStatusAsync.DirectMessageFailedAsync].InvokeMethodsAsync(failedMessage);
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
                    await RuntimeEvents.DynamicEvents[UserStatusAsync.UserJoinedChannelAsync].InvokeMethodsAsync(participant);
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
                    await RuntimeEvents.DynamicEvents[UserStatusAsync.UserJoinedChannelAsync].InvokeMethodsAsync(participant);
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
                    await RuntimeEvents.DynamicEvents[UserStatusAsync.UserLeftChannelAsync].InvokeMethodsAsync(participant);
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
                    await RuntimeEvents.DynamicEvents[UserStatusAsync.UserLeftChannelAsync].InvokeMethodsAsync(participant, value);
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
                    await RuntimeEvents.DynamicEvents[UserStatusAsync.UserValuesUpdatedAsync].InvokeMethodsAsync(participant);
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
                    await RuntimeEvents.DynamicEvents[UserStatusAsync.UserValuesUpdatedAsync].InvokeMethodsAsync(participant, value);
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
                    await RuntimeEvents.DynamicEvents[UserAudioStatusAsync.UserMutedAsync].InvokeMethodsAsync(participant);
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
                    await RuntimeEvents.DynamicEvents[UserAudioStatusAsync.UserMutedAsync].InvokeMethodsAsync(participant, value);
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
                    await RuntimeEvents.DynamicEvents[UserAudioStatusAsync.UserUnmutedAsync].InvokeMethodsAsync(participant);
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
                    await RuntimeEvents.DynamicEvents[UserAudioStatusAsync.UserUnmutedAsync].InvokeMethodsAsync(participant, value);
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
                    await RuntimeEvents.DynamicEvents[UserAudioStatusAsync.UserSpeakingAsync].InvokeMethodsAsync(participant);
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
                    await RuntimeEvents.DynamicEvents[UserAudioStatusAsync.UserSpeakingAsync].InvokeMethodsAsync(participant, value);
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
                    await RuntimeEvents.DynamicEvents[UserAudioStatusAsync.UserNotSpeakingAsync].InvokeMethodsAsync(participant);
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
                    await RuntimeEvents.DynamicEvents[UserAudioStatusAsync.UserNotSpeakingAsync].InvokeMethodsAsync(participant, value);
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
                    await RuntimeEvents.DynamicEvents[UserAudioStatusAsync.LocalUserMutedAsync].InvokeMethodsAsync(isMuted);
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
                    await RuntimeEvents.DynamicEvents[UserAudioStatusAsync.LocalUserMutedAsync].InvokeMethodsAsync(isMuted, value);
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
                    await RuntimeEvents.DynamicEvents[UserAudioStatusAsync.LocalUserUnmutedAsync].InvokeMethodsAsync(isMuted);
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
                    await RuntimeEvents.DynamicEvents[UserAudioStatusAsync.LocalUserUnmutedAsync].InvokeMethodsAsync(isMuted, value);
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
                    await RuntimeEvents.DynamicEvents[TextToSpeechStatusAsync.TTSMessageAddedAsync].InvokeMethodsAsync(ttsArgs);
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
                    await RuntimeEvents.DynamicEvents[TextToSpeechStatusAsync.TTSMessageAddedAsync].InvokeMethodsAsync(ttsArgs, value);
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
                    await RuntimeEvents.DynamicEvents[TextToSpeechStatusAsync.TTSMessageRemovedAsync].InvokeMethodsAsync(ttsArgs);
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
                    await RuntimeEvents.DynamicEvents[TextToSpeechStatusAsync.TTSMessageRemovedAsync].InvokeMethodsAsync(ttsArgs, value);
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
                    await RuntimeEvents.DynamicEvents[TextToSpeechStatusAsync.TTSMessageUpdatedAsync].InvokeMethodsAsync(ttsArgs);
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
                    await RuntimeEvents.DynamicEvents[TextToSpeechStatusAsync.TTSMessageUpdatedAsync].InvokeMethodsAsync(ttsArgs, value);
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

