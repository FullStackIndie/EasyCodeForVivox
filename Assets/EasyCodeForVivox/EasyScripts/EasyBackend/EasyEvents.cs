using System;
using System.CodeDom;
using System.Reflection;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    /// <summary>
    /// All EasyCodeForVivox Events
    /// </summary>
    public class EasyEvents
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

        public static event Action<bool> LocalUserMuted;
        public static event Action<bool> LocalUserUnmuted;

        #endregion


        #region Text-To-Speech Events

        public static event Action<ITTSMessageQueueEventArgs> TTSMessageAdded;
        public static event Action<ITTSMessageQueueEventArgs> TTSMessageRemoved;
        public static event Action<ITTSMessageQueueEventArgs> TTSMessageUpdated;

        #endregion






        #region Login Events


        public static void OnLoggingIn(ILoginSession loginSession)
        {
            try
            {
                LoggingIn?.Invoke(loginSession);

                foreach (var method in RuntimeEvents.LoginMethods)
                {
                    var attribute = method.GetCustomAttribute<LoginEventAttribute>();
                    if (attribute != null && attribute.Options == LoginStatus.LoggingIn)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { loginSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.LoginMethods)
                {
                    var attribute = method.GetCustomAttribute<LoginEventAttribute>();
                    if (attribute != null && attribute.Options == LoginStatus.LoggedIn)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { loginSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.LoginMethods)
                {
                    var attribute = method.GetCustomAttribute<LoginEventAttribute>();
                    if (attribute != null && attribute.Options == LoginStatus.LoggingOut)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { loginSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.LoginMethods)
                {
                    var attribute = method.GetCustomAttribute<LoginEventAttribute>();
                    if (attribute != null && attribute.Options == LoginStatus.LoggedOut)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { loginSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.ChannelMethods)
                {
                    var attribute = method.GetCustomAttribute<ChannelEventAttribute>();
                    if (attribute != null && attribute.Options == ChannelStatus.ChannelConnecting)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.ChannelMethods)
                {
                    var attribute = method.GetCustomAttribute<ChannelEventAttribute>();
                    if (attribute != null && attribute.Options == ChannelStatus.ChannelConnected)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.ChannelMethods)
                {
                    var attribute = method.GetCustomAttribute<ChannelEventAttribute>();
                    if (attribute != null && attribute.Options == ChannelStatus.ChannelDisconnecting)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.ChannelMethods)
                {
                    var attribute = method.GetCustomAttribute<ChannelEventAttribute>();
                    if (attribute != null && attribute.Options == ChannelStatus.ChannelDisconnected)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.TextChannelMethods)
                {
                    var attribute = method.GetCustomAttribute<TextChannelEventAttribute>();
                    if (attribute != null && attribute.Options == TextChannelStatus.TextChannelConnecting)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.TextChannelMethods)
                {
                    var attribute = method.GetCustomAttribute<TextChannelEventAttribute>();
                    if (attribute != null && attribute.Options == TextChannelStatus.TextChannelConnected)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.TextChannelMethods)
                {
                    var attribute = method.GetCustomAttribute<TextChannelEventAttribute>();
                    if (attribute != null && attribute.Options == TextChannelStatus.TextChannelDisconnecting)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.TextChannelMethods)
                {
                    var attribute = method.GetCustomAttribute<TextChannelEventAttribute>();
                    if (attribute != null && attribute.Options == TextChannelStatus.TextChannelDisconnected)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.AudioChannelMethods)
                {
                    var attribute = method.GetCustomAttribute<AudioChannelEventAttribute>();
                    if (attribute != null && attribute.Options == AudioChannelStatus.AudioChannelConnecting)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.AudioChannelMethods)
                {
                    var attribute = method.GetCustomAttribute<AudioChannelEventAttribute>();
                    if (attribute != null && attribute.Options == AudioChannelStatus.AudioChannelConnected)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.AudioChannelMethods)
                {
                    var attribute = method.GetCustomAttribute<AudioChannelEventAttribute>();
                    if (attribute != null && attribute.Options == AudioChannelStatus.AudioChannelDisconnecting)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.AudioChannelMethods)
                {
                    var attribute = method.GetCustomAttribute<AudioChannelEventAttribute>();
                    if (attribute != null && attribute.Options == AudioChannelStatus.AudioChannelDisconnected)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelSession });
                        }
                    }
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

                foreach (var method in RuntimeEvents.ChannelMessageMethods)
                {
                    var attribute = method.GetCustomAttribute<ChannelMessageEventAttribute>();
                    if (attribute != null && attribute.Options == ChannelMessageStatus.ChannelMessageRecieved)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelTextMessage });
                        }
                    }
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

                foreach (var method in RuntimeEvents.ChannelMessageMethods)
                {
                    var attribute = method.GetCustomAttribute<ChannelMessageEventAttribute>();
                    if (attribute != null && attribute.Options == ChannelMessageStatus.EventMessageRecieved)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { channelTextMessage });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnEventMessageRecieved)}");
                Debug.LogException(ex);
                throw;
            }

        }

        public static void OnChannelMessageSent<T>(T value) where T : class
        {
            try
            {
                ChannelMesssageSent?.Invoke();

                foreach (var method in RuntimeEvents.ChannelMessageMethods)
                {
                    var attribute = method.GetCustomAttribute<ChannelMessageEventAttribute>();
                    if (attribute != null && attribute.Options == ChannelMessageStatus.ChannelMessageSent)
                    {
                        var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                        foreach (var gameObject in gameObjects)
                        {
                            method?.Invoke(gameObject, new[] { value });
                        }
                    }
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

        public static void OnDirectMessageRecieved(IDirectedTextMessage message)
        {
            try
            {
                DirectMessageRecieved?.Invoke(message);
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

        public static void OnLocalUserMuted(bool isMuted)
        {
            try
            {
                LocalUserMuted?.Invoke(isMuted);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLocalUserMuted)}");
                Debug.LogException(ex);
                throw;
            }

        }

        public static void OnLocalUserUnmuted(bool isMuted)
        {
            try
            {
                LocalUserUnmuted?.Invoke(isMuted);
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
