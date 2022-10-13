using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox.Events
{
    /// <summary>
    /// All EasyCodeForVivox Events
    /// </summary>
    public static class EasyEvents
    {

        #region Login Events

        public static event Action<ILoginSession> LoggingIn;
        public static event Action<ILoginSession> LoggedIn;
        public static event Action<ILoginSession> LoggedOut;
        public static event Action<ILoginSession> LoggingOut;

        #endregion


        #region Channel Events

        public static event Action<IChannelSession> ChannelConnecting;
        public static event Action<IChannelSession> ChannelConnected;
        public static event Action<IChannelSession> ChannelDisconnecting;
        public static event Action<IChannelSession> ChannelDisconnected;

        public static event Action<IChannelSession> TextChannelConnecting;
        public static event Action<IChannelSession> TextChannelConnected;
        public static event Action<IChannelSession> TextChannelDisconnecting;
        public static event Action<IChannelSession> TextChannelDisconnected;

        public static event Action<IChannelSession> AudioChannelConnecting;
        public static event Action<IChannelSession> AudioChannelConnected;
        public static event Action<IChannelSession> AudioChannelDisconnecting;
        public static event Action<IChannelSession> AudioChannelDisconnected;

        #endregion


        #region Message Events

        public static event Action ChannelMesssageSent;
        public static event Action<IChannelTextMessage> ChannelMessageRecieved;
        public static event Action<IChannelTextMessage> EventMessageRecieved;

        public static event Action DirectMesssageSent;
        public static event Action<IDirectedTextMessage> DirectMessageRecieved;
        public static event Action<IFailedDirectedTextMessage> DirectMessageFailed;

        #endregion


        #region Participant/User Events

        public static event Action<IParticipant> UserJoinedChannel;
        public static event Action<IParticipant> UserLeftChannel;
        public static event Action<IParticipant> UserValuesUpdated;

        public static event Action<IParticipant> UserMuted;
        public static event Action<IParticipant> UserUnmuted;

        public static event Action<IParticipant> UserSpeaking;
        public static event Action<IParticipant> UserNotSpeaking;

        #endregion


        #region User Mute Events

        public static event Action LocalUserMuted;
        public static event Action LocalUserUnmuted;

        #endregion


        #region Text-To-Speech Events

        public static event Action<ITTSMessageQueueEventArgs> TTSMessageAdded;
        public static event Action<ITTSMessageQueueEventArgs> TTSMessageRemoved;
        public static event Action<ITTSMessageQueueEventArgs> TTSMessageUpdated;

        #endregion



        private static void InvokeMethods(Enum eventKey)
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
                foreach (var gameObject in gameObjects)
                {
                    method?.Invoke(gameObject, null);
                }
            }
        }

        private static void InvokeMethods<T>(Enum eventKey, T value)
        {
            if (!RuntimeEvents.DynamicEvents.ContainsKey(eventKey))
            {
                return;
            }
            foreach (var method in RuntimeEvents.DynamicEvents[eventKey])
            {
                var parameters = method.GetParameters();
                if (parameters.Length != 1 || parameters.FirstOrDefault().ParameterType != typeof(T))
                {
                    continue;
                }
                var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                foreach (var gameObject in gameObjects)
                {
                    method?.Invoke(gameObject, new object[] { value });
                }
            }
        }

        private static void InvokeMethods<T1, T2>(Enum eventKey, T1 value1, T2 value2)
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
                foreach (var gameObject in gameObjects)
                {
                    method?.Invoke(gameObject, new object[] { value1, value2 });
                }
            }
        }


        #region Login Events


        public static void OnLoggingIn(ILoginSession loginSession)
        {
            try
            {
                LoggingIn?.Invoke(loginSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(LoginStatus.LoggingIn, loginSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLoggingIn)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnLoggingIn<T>(ILoginSession loginSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(LoginStatus.LoggingIn, loginSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLoggingIn)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnLoggedIn(ILoginSession loginSession)
        {
            try
            {
                LoggedIn?.Invoke(loginSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(LoginStatus.LoggedIn, loginSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLoggedIn)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnLoggedIn<T>(ILoginSession loginSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(LoginStatus.LoggedIn, loginSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLoggedIn)}");
                Debug.LogException(ex);
                throw;
            }
        }



        public static void OnLoggingOut(ILoginSession loginSession)
        {
            try
            {
                LoggingOut?.Invoke(loginSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(LoginStatus.LoggingOut, loginSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLoggingOut)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnLoggingOut<T>(ILoginSession loginSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(LoginStatus.LoggingOut, loginSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLoggingOut)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnLoggedOut(ILoginSession loginSession)
        {
            try
            {
                LoggedOut?.Invoke(loginSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(LoginStatus.LoggedOut, loginSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLoggedOut)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnLoggedOut<T>(ILoginSession loginSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(LoginStatus.LoggedOut, loginSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLoggedOut)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Channel Event Methods


        public static void OnChannelConnecting(IChannelSession channelSession)
        {
            try
            {
                ChannelConnecting?.Invoke(channelSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(ChannelStatus.ChannelConnecting, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelConnecting)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnChannelConnecting<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(ChannelStatus.ChannelConnecting, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelConnecting)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnChannelConnected(IChannelSession channelSession)
        {
            try
            {
                ChannelConnected?.Invoke(channelSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(ChannelStatus.ChannelConnected, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelConnected)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnChannelConnected<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(ChannelStatus.ChannelConnected, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelConnected)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnChannelDisconnecting(IChannelSession channelSession)
        {
            try
            {
                ChannelDisconnecting?.Invoke(channelSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(ChannelStatus.ChannelDisconnecting, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelDisconnecting)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnChannelDisconnecting<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(ChannelStatus.ChannelDisconnecting, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelDisconnecting)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnChannelDisconnected(IChannelSession channelSession)
        {
            try
            {
                ChannelDisconnected?.Invoke(channelSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(ChannelStatus.ChannelDisconnected, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelDisconnected)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnChannelDisconnected<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(ChannelStatus.ChannelDisconnected, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelDisconnected)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Text Channel Events


        public static void OnTextChannelConnecting(IChannelSession channelSession)
        {
            try
            {
                TextChannelConnecting?.Invoke(channelSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextChannelStatus.TextChannelConnecting, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelConnecting)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnTextChannelConnecting<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextChannelStatus.TextChannelConnecting, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelConnecting)}");
                Debug.LogException(ex);
                throw;
            }
        }


        public static void OnTextChannelConnected(IChannelSession channelSession)
        {
            try
            {
                TextChannelConnected?.Invoke(channelSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextChannelStatus.TextChannelConnected, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelConnected)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnTextChannelConnected<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextChannelStatus.TextChannelConnected, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelConnected)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnTextChannelDisconnecting(IChannelSession channelSession)
        {
            try
            {
                TextChannelDisconnecting?.Invoke(channelSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextChannelStatus.TextChannelDisconnecting, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelDisconnecting)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnTextChannelDisconnecting<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextChannelStatus.TextChannelDisconnecting, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelDisconnecting)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnTextChannelDisconnected(IChannelSession channelSession)
        {
            try
            {
                TextChannelDisconnected?.Invoke(channelSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextChannelStatus.TextChannelDisconnected, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelDisconnected)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnTextChannelDisconnected<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextChannelStatus.TextChannelDisconnected, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTextChannelDisconnected)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Voice Channel Events


        public static void OnAudioChannelConnecting(IChannelSession channelSession)
        {
            try
            {
                AudioChannelConnecting?.Invoke(channelSession);
                if (EasySession.UseDynamicEvents && RuntimeEvents.DynamicEvents.ContainsKey(AudioChannelStatus.AudioChannelConnecting))
                {
                    InvokeMethods(AudioChannelStatus.AudioChannelConnecting, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelConnecting)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnAudioChannelConnecting<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents && RuntimeEvents.DynamicEvents.ContainsKey(AudioChannelStatus.AudioChannelConnecting))
                {
                    InvokeMethods(AudioChannelStatus.AudioChannelConnecting, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelConnecting)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnAudioChannelConnected(IChannelSession channelSession)
        {
            try
            {
                AudioChannelConnected?.Invoke(channelSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(AudioChannelStatus.AudioChannelConnected, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelConnected)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnAudioChannelConnected<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(AudioChannelStatus.AudioChannelConnected, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelConnected)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnAudioChannelDisconnecting(IChannelSession channelSession)
        {
            try
            {
                AudioChannelDisconnecting?.Invoke(channelSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(AudioChannelStatus.AudioChannelDisconnecting, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelDisconnecting)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnAudioChannelDisconnecting<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(AudioChannelStatus.AudioChannelDisconnecting, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelDisconnecting)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnAudioChannelDisconnected(IChannelSession channelSession)
        {
            try
            {
                AudioChannelDisconnected?.Invoke(channelSession);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(AudioChannelStatus.AudioChannelDisconnected, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelDisconnected)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnAudioChannelDisconnected<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(AudioChannelStatus.AudioChannelDisconnected, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnAudioChannelDisconnected)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Message Events


        public static void OnChannelMessageRecieved(IChannelTextMessage channelTextMessage)
        {
            try
            {
                ChannelMessageRecieved?.Invoke(channelTextMessage);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(ChannelMessageStatus.ChannelMessageRecieved, channelTextMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelMessageRecieved)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnChannelMessageRecieved<T>(IChannelTextMessage channelTextMessage, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(ChannelMessageStatus.ChannelMessageRecieved, channelTextMessage, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelMessageRecieved)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnEventMessageRecieved(IChannelTextMessage channelTextMessage)
        {
            try
            {
                EventMessageRecieved?.Invoke(channelTextMessage);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(ChannelMessageStatus.EventMessageRecieved, channelTextMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnEventMessageRecieved)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnEventMessageRecieved<T>(IChannelTextMessage channelTextMessage, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(ChannelMessageStatus.EventMessageRecieved, channelTextMessage, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnEventMessageRecieved)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnChannelMessageSent()
        {
            try
            {
                ChannelMesssageSent?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelMessageSent)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnChannelMessageSent<T>(T value) where T : class
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(ChannelMessageStatus.ChannelMessageSent, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnChannelMessageSent)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnDirectMessageSent()
        {
            try
            {
                DirectMesssageSent?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnDirectMessageSent)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnDirectMessageSent<T>(T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(DirectMessageStatus.DirectMessageSent, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnDirectMessageSent)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnDirectMessageRecieved(IDirectedTextMessage message)
        {
            try
            {
                DirectMessageRecieved?.Invoke(message);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(DirectMessageStatus.DirectMessageRecieved, message);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnDirectMessageRecieved)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnDirectMessageRecieved<T>(IDirectedTextMessage message, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(DirectMessageStatus.DirectMessageRecieved, message, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnDirectMessageRecieved)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnDirectMessageFailed(IFailedDirectedTextMessage failedMessage)
        {
            try
            {
                DirectMessageFailed?.Invoke(failedMessage);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(DirectMessageStatus.DirectMessageFailed, failedMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnDirectMessageFailed)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnDirectMessageFailed<T>(IFailedDirectedTextMessage failedMessage, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(DirectMessageStatus.DirectMessageFailed, failedMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnDirectMessageFailed)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Participant/User Events


        public static void OnUserJoinedChannel(IParticipant participant)
        {
            try
            {
                UserJoinedChannel?.Invoke(participant);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserStatus.UserJoinedChannel, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserJoinedChannel)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnUserJoinedChannel<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserStatus.UserJoinedChannel, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserJoinedChannel)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnUserLeftChannel(IParticipant participant)
        {
            try
            {
                UserLeftChannel?.Invoke(participant);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserStatus.UserLeftChannel, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserLeftChannel)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnUserLeftChannel<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserStatus.UserLeftChannel, participant, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserLeftChannel)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnUserValuesUpdated(IParticipant participant)
        {
            try
            {
                UserValuesUpdated?.Invoke(participant);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserStatus.UserValuesUpdated, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserValuesUpdated)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnUserValuesUpdated<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserStatus.UserValuesUpdated, participant, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserValuesUpdated)}");
                Debug.LogException(ex);
                throw;
            }
        }



        public static void OnUserMuted(IParticipant participant)
        {
            try
            {
                UserMuted?.Invoke(participant);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.UserMuted, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserMuted)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnUserMuted<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.UserMuted, participant, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserMuted)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnUserUnmuted(IParticipant participant)
        {
            try
            {
                UserUnmuted?.Invoke(participant);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.UserUnmuted, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserUnmuted)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnUserUnmuted<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.UserUnmuted, participant, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserUnmuted)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnUserSpeaking(IParticipant participant)
        {
            try
            {
                UserSpeaking?.Invoke(participant);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.UserSpeaking, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserSpeaking)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnUserSpeaking<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.UserSpeaking, participant, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserSpeaking)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnUserNotSpeaking(IParticipant participant)
        {
            try
            {
                UserNotSpeaking?.Invoke(participant);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.UserNotSpeaking, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserNotSpeaking)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnUserNotSpeaking<T>(IParticipant participant, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.UserNotSpeaking, participant, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserNotSpeaking)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region User Mute Events

        public static void OnLocalUserMuted()
        {
            try
            {
                LocalUserMuted?.Invoke();
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.LocalUserMuted);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLocalUserMuted)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnLocalUserMuted<T>(T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.LocalUserMuted, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLocalUserMuted)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnLocalUserUnmuted()
        {
            try
            {
                LocalUserUnmuted?.Invoke();
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.LocalUserUnmuted);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLocalUserUnmuted)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnLocalUserUnmuted<T>(T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.LocalUserUnmuted, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLocalUserUnmuted)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Text-to-Speech Events


        public static void OnTTSMessageAdded(ITTSMessageQueueEventArgs ttsArgs)
        {
            try
            {
                TTSMessageAdded?.Invoke(ttsArgs);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextToSpeechStatus.TTSMessageAdded, ttsArgs);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTTSMessageAdded)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnTTSMessageAdded<T>(ITTSMessageQueueEventArgs ttsArgs, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextToSpeechStatus.TTSMessageAdded, ttsArgs, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTTSMessageAdded)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnTTSMessageRemoved(ITTSMessageQueueEventArgs ttsArgs)
        {
            try
            {
                TTSMessageRemoved?.Invoke(ttsArgs);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextToSpeechStatus.TTSMessageRemoved, ttsArgs);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTTSMessageRemoved)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnTTSMessageRemoved<T>(ITTSMessageQueueEventArgs ttsArgs, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextToSpeechStatus.TTSMessageRemoved, ttsArgs, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTTSMessageRemoved)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnTTSMessageUpdated(ITTSMessageQueueEventArgs ttsArgs)
        {
            try
            {
                TTSMessageUpdated?.Invoke(ttsArgs);
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextToSpeechStatus.TTSMessageUpdated, ttsArgs);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTTSMessageUpdated)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public static void OnTTSMessageUpdated<T>(ITTSMessageQueueEventArgs ttsArgs, T value)
        {
            try
            {
                if (EasySession.UseDynamicEvents)
                {
                    InvokeMethods(TextToSpeechStatus.TTSMessageUpdated, ttsArgs, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnTTSMessageUpdated)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


    }

}
