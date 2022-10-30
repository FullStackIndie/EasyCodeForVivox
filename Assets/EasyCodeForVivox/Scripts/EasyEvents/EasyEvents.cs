using EasyCodeForVivox.Events;
using System;
using System.Linq;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasyEvents
    {
        private readonly EasySettingsSO _settings;

        public EasyEvents(EasySettingsSO settings)
        {
            _settings = settings;
        }


        #region Login Events

        public event Action<ILoginSession> LoggingIn;
        public event Action<ILoginSession> LoggedIn;
        public event Action<ILoginSession> LoggedOut;
        public event Action<ILoginSession> LoggingOut;

        #endregion


        #region Channel Events

        public event Action<IChannelSession> ChannelConnecting;
        public event Action<IChannelSession> ChannelConnected;
        public event Action<IChannelSession> ChannelDisconnecting;
        public event Action<IChannelSession> ChannelDisconnected;

        public event Action<IChannelSession> TextChannelConnecting;
        public event Action<IChannelSession> TextChannelConnected;
        public event Action<IChannelSession> TextChannelDisconnecting;
        public event Action<IChannelSession> TextChannelDisconnected;

        public event Action<IChannelSession> AudioChannelConnecting;
        public event Action<IChannelSession> AudioChannelConnected;
        public event Action<IChannelSession> AudioChannelDisconnecting;
        public event Action<IChannelSession> AudioChannelDisconnected;

        #endregion


        #region Message Events

        public event Action ChannelMesssageSent;
        public event Action<IChannelTextMessage> ChannelMessageRecieved;
        public event Action<IChannelTextMessage> EventMessageRecieved;

        public event Action DirectMesssageSent;
        public event Action<IDirectedTextMessage> DirectMessageRecieved;
        public event Action<IFailedDirectedTextMessage> DirectMessageFailed;

        #endregion


        #region Participant/User Events

        public event Action<IParticipant> UserJoinedChannel;
        public event Action<IParticipant> UserLeftChannel;
        public event Action<IParticipant> UserValuesUpdated;

        public event Action<IParticipant> UserMuted;
        public event Action<IParticipant> UserUnmuted;

        public event Action<AccountId> UserCrossMuted;
        public event Action<AccountId> UserCrossUnmuted;

        public event Action<IParticipant> UserSpeaking;
        public event Action<IParticipant> UserNotSpeaking;

        #endregion


        #region User Mute Events

        public event Action LocalUserMuted;
        public event Action LocalUserUnmuted;

        #endregion


        #region Text-To-Speech Events

        public event Action<ITTSMessageQueueEventArgs> TTSMessageAdded;
        public event Action<ITTSMessageQueueEventArgs> TTSMessageRemoved;
        public event Action<ITTSMessageQueueEventArgs> TTSMessageUpdated;

        #endregion



        private void InvokeMethods(Enum eventKey)
        {
            if (!DynamicEvents.Methods.ContainsKey(eventKey))
            {
                return;
            }
            foreach (var method in DynamicEvents.Methods[eventKey])
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

        private void InvokeMethods<T>(Enum eventKey, T value)
        {
            if (!DynamicEvents.Methods.ContainsKey(eventKey))
            {
                return;
            }
            foreach (var method in DynamicEvents.Methods[eventKey])
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

        private void InvokeMethods<T1, T2>(Enum eventKey, T1 value1, T2 value2)
        {
            if (!DynamicEvents.Methods.ContainsKey(eventKey))
            {
                return;
            }
            foreach (var method in DynamicEvents.Methods[eventKey])
            {
                var parameters = method.GetParameters();
                if (parameters.Length != 2 || parameters[0].ParameterType != typeof(T1) || parameters[1].ParameterType != typeof(T2))
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


        public void OnLoggingIn(ILoginSession loginSession)
        {
            try
            {
                LoggingIn?.Invoke(loginSession);
                if (_settings.UseDynamicEvents)
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

        public void OnLoggingIn<T>(ILoginSession loginSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnLoggedIn(ILoginSession loginSession)
        {
            try
            {
                LoggedIn?.Invoke(loginSession);
                if (_settings.UseDynamicEvents)
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

        public void OnLoggedIn<T>(ILoginSession loginSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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



        public void OnLoggingOut(ILoginSession loginSession)
        {
            try
            {
                LoggingOut?.Invoke(loginSession);
                if (_settings.UseDynamicEvents)
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

        public void OnLoggingOut<T>(ILoginSession loginSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnLoggedOut(ILoginSession loginSession)
        {
            try
            {
                LoggedOut?.Invoke(loginSession);
                if (_settings.UseDynamicEvents)
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

        public void OnLoggedOut<T>(ILoginSession loginSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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


        public void OnChannelConnecting(IChannelSession channelSession)
        {
            try
            {
                ChannelConnecting?.Invoke(channelSession);
                if (_settings.UseDynamicEvents)
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

        public void OnChannelConnecting<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnChannelConnected(IChannelSession channelSession)
        {
            try
            {
                ChannelConnected?.Invoke(channelSession);
                if (_settings.UseDynamicEvents)
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

        public void OnChannelConnected<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnChannelDisconnecting(IChannelSession channelSession)
        {
            try
            {
                ChannelDisconnecting?.Invoke(channelSession);
                if (_settings.UseDynamicEvents)
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

        public void OnChannelDisconnecting<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnChannelDisconnected(IChannelSession channelSession)
        {
            try
            {
                ChannelDisconnected?.Invoke(channelSession);
                if (_settings.UseDynamicEvents)
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

        public void OnChannelDisconnected<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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


        public void OnTextChannelConnecting(IChannelSession channelSession)
        {
            try
            {
                TextChannelConnecting?.Invoke(channelSession);
                if (_settings.UseDynamicEvents)
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

        public void OnTextChannelConnecting<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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


        public void OnTextChannelConnected(IChannelSession channelSession)
        {
            try
            {
                TextChannelConnected?.Invoke(channelSession);
                if (_settings.UseDynamicEvents)
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

        public void OnTextChannelConnected<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnTextChannelDisconnecting(IChannelSession channelSession)
        {
            try
            {
                TextChannelDisconnecting?.Invoke(channelSession);
                if (_settings.UseDynamicEvents)
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

        public void OnTextChannelDisconnecting<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnTextChannelDisconnected(IChannelSession channelSession)
        {
            try
            {
                TextChannelDisconnected?.Invoke(channelSession);
                if (_settings.UseDynamicEvents)
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

        public void OnTextChannelDisconnected<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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


        public void OnAudioChannelConnecting(IChannelSession channelSession)
        {
            try
            {
                AudioChannelConnecting?.Invoke(channelSession);
                if (_settings.UseDynamicEvents && DynamicEvents.Methods.ContainsKey(AudioChannelStatus.AudioChannelConnecting))
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

        public void OnAudioChannelConnecting<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents && DynamicEvents.Methods.ContainsKey(AudioChannelStatus.AudioChannelConnecting))
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

        public void OnAudioChannelConnected(IChannelSession channelSession)
        {
            try
            {
                AudioChannelConnected?.Invoke(channelSession);
                if (_settings.UseDynamicEvents)
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

        public void OnAudioChannelConnected<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnAudioChannelDisconnecting(IChannelSession channelSession)
        {
            try
            {
                AudioChannelDisconnecting?.Invoke(channelSession);
                if (_settings.UseDynamicEvents)
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

        public void OnAudioChannelDisconnecting<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnAudioChannelDisconnected(IChannelSession channelSession)
        {
            try
            {
                AudioChannelDisconnected?.Invoke(channelSession);
                if (_settings.UseDynamicEvents)
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

        public void OnAudioChannelDisconnected<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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


        public void OnChannelMessageRecieved(IChannelTextMessage channelTextMessage)
        {
            try
            {
                ChannelMessageRecieved?.Invoke(channelTextMessage);
                if (_settings.UseDynamicEvents)
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

        public void OnChannelMessageRecieved<T>(IChannelTextMessage channelTextMessage, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnEventMessageRecieved(IChannelTextMessage channelTextMessage)
        {
            try
            {
                EventMessageRecieved?.Invoke(channelTextMessage);
                if (_settings.UseDynamicEvents)
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

        public void OnEventMessageRecieved<T>(IChannelTextMessage channelTextMessage, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnChannelMessageSent()
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

        public void OnChannelMessageSent<T>(T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnDirectMessageSent()
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

        public void OnDirectMessageSent<T>(T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnDirectMessageRecieved(IDirectedTextMessage message)
        {
            try
            {
                DirectMessageRecieved?.Invoke(message);
                if (_settings.UseDynamicEvents)
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

        public void OnDirectMessageRecieved<T>(IDirectedTextMessage message, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnDirectMessageFailed(IFailedDirectedTextMessage failedMessage)
        {
            try
            {
                DirectMessageFailed?.Invoke(failedMessage);
                if (_settings.UseDynamicEvents)
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

        public void OnDirectMessageFailed<T>(IFailedDirectedTextMessage failedMessage, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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


        public void OnUserJoinedChannel(IParticipant participant)
        {
            try
            {
                UserJoinedChannel?.Invoke(participant);
                if (_settings.UseDynamicEvents)
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

        public void OnUserLeftChannel(IParticipant participant)
        {
            try
            {
                UserLeftChannel?.Invoke(participant);
                if (_settings.UseDynamicEvents)
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

        public void OnUserValuesUpdated(IParticipant participant)
        {
            try
            {
                UserValuesUpdated?.Invoke(participant);
                if (_settings.UseDynamicEvents)
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



        public void OnUserMuted(IParticipant participant)
        {
            try
            {
                UserMuted?.Invoke(participant);
                if (_settings.UseDynamicEvents)
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

        public void OnUserUnmuted(IParticipant participant)
        {
            try
            {
                UserUnmuted?.Invoke(participant);
                if (_settings.UseDynamicEvents)
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



        public void OnUserCrossMuted(AccountId accountId)
        {
            try
            {
                UserCrossMuted?.Invoke(accountId);
                if (_settings.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.UserCrossMuted, accountId);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserCrossMuted)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public void OnUserCrossUnmuted(AccountId accountId)
        {
            try
            {
                UserCrossUnmuted?.Invoke(accountId);
                if (_settings.UseDynamicEvents)
                {
                    InvokeMethods(UserAudioStatus.UserCrossUnmuted, accountId);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnUserCrossUnmuted)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public void OnUserSpeaking(IParticipant participant)
        {
            try
            {
                UserSpeaking?.Invoke(participant);
                if (_settings.UseDynamicEvents)
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

        public void OnUserNotSpeaking(IParticipant participant)
        {
            try
            {
                UserNotSpeaking?.Invoke(participant);
                if (_settings.UseDynamicEvents)
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


        #endregion


        #region User Mute Events

        public void OnLocalUserMuted()
        {
            try
            {
                LocalUserMuted?.Invoke();
                if (_settings.UseDynamicEvents)
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

        public void OnLocalUserMuted<T>(T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public void OnLocalUserUnmuted()
        {
            try
            {
                LocalUserUnmuted?.Invoke();
                if (_settings.UseDynamicEvents)
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

        public void OnLocalUserUnmuted<T>(T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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


        public void OnTTSMessageAdded(ITTSMessageQueueEventArgs ttsArgs)
        {
            try
            {
                TTSMessageAdded?.Invoke(ttsArgs);
                if (_settings.UseDynamicEvents)
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

        public void OnTTSMessageRemoved(ITTSMessageQueueEventArgs ttsArgs)
        {
            try
            {
                TTSMessageRemoved?.Invoke(ttsArgs);
                if (_settings.UseDynamicEvents)
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

        public void OnTTSMessageUpdated(ITTSMessageQueueEventArgs ttsArgs)
        {
            try
            {
                TTSMessageUpdated?.Invoke(ttsArgs);
                if (_settings.UseDynamicEvents)
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



        #endregion

    }
}
