using EasyCodeForVivox.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox.Events
{
    public class EasyEventsAsync
    {
        private readonly EasySettingsSO _settings;

        public EasyEventsAsync(EasySettingsSO settings)
        {
            _settings = settings;
        }

        private async Task InvokeMethodsAsync(Enum eventKey)
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
                var tasks = gameObjects.Select(gameObject => Task.Run(() =>
                {
                    method?.Invoke(gameObject, null);
                }));
                await Task.WhenAll(tasks);
            }
        }

        private async Task InvokeMethodsAsync<T>(Enum eventKey, T value)
        {
            if (!DynamicEvents.Methods.ContainsKey(eventKey))
            {
                return;
            }
            foreach (var method in DynamicEvents.Methods[eventKey])
            {
                var parameters = method.GetParameters();
                if (parameters.Length != 1 || parameters[0].ParameterType != typeof(T))
                {
                    continue;
                }
                if (parameters[0].ParameterType.Namespace != null)
                {
                    if (parameters[0].ParameterType.Namespace.Contains("UnityEngine"))
                    {
                        Debug.Log($"Unity Object detected [ {parameters[1].ParameterType.FullName} ]. Exception may be thrown. ".Color(EasyDebug.Red) +
                       $"You cannot access/modify GameObjects or UI elements or use GetComponent<> with async Dynamic events because it is called on a different thread. ".Color(EasyDebug.Yellow) +
                       $"Use Synchronous events instead to access/modify Gameobjects/UI and their children. ".Color(EasyDebug.Yellow) +
                       $"There is an exception to this rule. Check out this post about the Gotchas [ https://app.gitbook.com/s/-MWHF2U_tfZEUaf_IsL-/~/changes/LdbVopiHUr6d1lvPFcA5/dynamic-events/dynamic-events/gotchas#dynamic-event-gotchas ] ".Color(EasyDebug.Cyan));
                    }
                }
                var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                var tasks = gameObjects.Select(gameObject => Task.Run(() =>
                {
                    method?.Invoke(gameObject, new object[] { value });
                }));
                await Task.WhenAll(tasks);
            }
        }


        private async Task InvokeMethodsAsync<T1, T2>(Enum eventKey, T1 value1, T2 value2)
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
                    //Debug.Log($"Parameters did not match : [ {parameters[0].ParameterType} : {parameters[1].ParameterType} ]");
                    continue;
                }
                if (parameters[1].ParameterType.Namespace != null)
                {
                    if (parameters[1].ParameterType.Namespace.Contains("UnityEngine"))
                    {
                        Debug.Log($"Unity Object detected [ {parameters[1].ParameterType.FullName} ]. Exception may be thrown. ".Color(EasyDebug.Red) +
                       $"You cannot access/modify GameObjects or UI elements or use GetComponent<> with async Dynamic events because it is called on a different thread. ".Color(EasyDebug.Yellow) +
                       $"Use Synchronous events instead to access/modify Gameobjects/UI and their children. ".Color(EasyDebug.Yellow) +
                       $"There is an exception to this rule. Check out this post about the Gotchas [ https://app.gitbook.com/s/-MWHF2U_tfZEUaf_IsL-/~/changes/LdbVopiHUr6d1lvPFcA5/dynamic-events/dynamic-events/gotchas#dynamic-event-gotchas ] ".Color(EasyDebug.Cyan));
                    }
                }
                var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                var tasks = gameObjects.Select(gameObject => Task.Run(() =>
                {
                    method?.Invoke(gameObject, new object[] { value1, value2 });
                }));
                await Task.WhenAll(tasks);
            }
        }


        public async Task OnLoggingInAsync(ILoginSession loginSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(LoginStatusAsync.LoggingInAsync, loginSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnLoggingInAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnLoggingInAsync<T>(ILoginSession loginSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(LoginStatusAsync.LoggingInAsync, loginSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnLoggingInAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnLoggedInAsync(ILoginSession loginSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public async Task OnLoggedInAsync<T>(ILoginSession loginSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(LoginStatusAsync.LoggedInAsync, loginSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnLoggedInAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnLoggingOutAsync(ILoginSession loginSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public async Task OnLoggingOutAsync<T>(ILoginSession loginSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(LoginStatusAsync.LoggingOutAsync, loginSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnLoggingOutAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnLoggedOutAsync(ILoginSession loginSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
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

        public async Task OnLoggedOutAsync<T>(ILoginSession loginSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(LoginStatusAsync.LoggedOutAsync, loginSession, value);
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


        public async Task OnChannelConnectingAsync(IChannelSession channelSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelConnectingAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnChannelConnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnChannelConnectingAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelConnectingAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnChannelConnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnChannelConnectedAsync(IChannelSession channelSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelConnectedAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnChannelConnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnChannelConnectedAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelConnectedAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnChannelConnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnChannelDisconnectingAsync(IChannelSession channelSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelDisconnectingAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnChannelDisconnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnChannelDisconnectingAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelDisconnectingAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnChannelDisconnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnChannelDisconnectedAsync(IChannelSession channelSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelDisconnectedAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnChannelDisconnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnChannelDisconnectedAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelStatusAsync.ChannelDisconnectedAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnChannelDisconnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Text Channel Events


        public async Task OnTextChannelConnectingAsync(IChannelSession channelSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelConnectingAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnTextChannelConnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnTextChannelConnectingAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelConnectingAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnTextChannelConnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        public async Task OnTextChannelConnectedAsync(IChannelSession channelSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelConnectedAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnTextChannelConnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnTextChannelConnectedAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelConnectedAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnTextChannelConnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnTextChannelDisconnectingAsync(IChannelSession channelSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelDisconnectingAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnTextChannelDisconnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnTextChannelDisconnectingAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelDisconnectingAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnTextChannelDisconnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnTextChannelDisconnectedAsync(IChannelSession channelSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelDisconnectedAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnTextChannelDisconnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnTextChannelDisconnectedAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextChannelStatusAsync.TextChannelDisconnectedAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnTextChannelDisconnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Voice Channel Events


        public async Task OnAudioChannelConnectingAsync(IChannelSession channelSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelConnectingAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnAudioChannelConnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnAudioChannelConnectingAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelConnectingAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnAudioChannelConnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnAudioChannelConnectedAsync(IChannelSession channelSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelConnectedAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnAudioChannelConnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnAudioChannelConnectedAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelConnectedAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnAudioChannelConnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnAudioChannelDisconnectingAsync(IChannelSession channelSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelDisconnectingAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnAudioChannelDisconnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnAudioChannelDisconnectingAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelDisconnectingAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnAudioChannelDisconnectingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnAudioChannelDisconnectedAsync(IChannelSession channelSession)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelDisconnectedAsync, channelSession);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnAudioChannelDisconnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnAudioChannelDisconnectedAsync<T>(IChannelSession channelSession, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(AudioChannelStatusAsync.AudioChannelDisconnectedAsync, channelSession, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnAudioChannelDisconnectedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Message Events


        public async Task OnChannelMessageRecievedAsync(IChannelTextMessage channelTextMessage)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelMessageStatusAsync.ChannelMessageRecievedAsync, channelTextMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnChannelMessageRecievedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnChannelMessageRecievedAsync<T>(IChannelTextMessage channelTextMessage, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelMessageStatusAsync.ChannelMessageRecievedAsync, channelTextMessage, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnChannelMessageRecievedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnEventMessageRecievedAsync(IChannelTextMessage channelTextMessage)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelMessageStatusAsync.EventMessageRecievedAsync, channelTextMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnEventMessageRecievedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnEventMessageRecievedAsync<T>(IChannelTextMessage channelTextMessage, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelMessageStatusAsync.EventMessageRecievedAsync, channelTextMessage, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnEventMessageRecievedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnChannelMessageSentAsync()
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelMessageStatusAsync.ChannelMessageSentAsync);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnChannelMessageSentAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnChannelMessageSentAsync<T>(T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(ChannelMessageStatusAsync.ChannelMessageSentAsync, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnChannelMessageSentAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnDirectMessageSentAsync()
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(DirectMessageStatusAsync.DirectMessageSentAsync);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnDirectMessageSentAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnDirectMessageSentAsync<T>(T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(DirectMessageStatusAsync.DirectMessageSentAsync, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnDirectMessageSentAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnDirectMessageRecievedAsync(IDirectedTextMessage message)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(DirectMessageStatusAsync.DirectMessageRecievedAsync, message);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnDirectMessageRecievedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnDirectMessageRecievedAsync<T>(IDirectedTextMessage message, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(DirectMessageStatusAsync.DirectMessageRecievedAsync, message, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnDirectMessageRecievedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnDirectMessageFailedAsync(IFailedDirectedTextMessage failedMessage)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(DirectMessageStatusAsync.DirectMessageFailedAsync, failedMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnDirectMessageFailedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnDirectMessageFailedAsync<T>(IFailedDirectedTextMessage failedMessage, T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(DirectMessageStatusAsync.DirectMessageFailedAsync, failedMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnDirectMessageFailedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Participant/User Events


        public async Task OnUserJoinedChannelAsync(IParticipant participant)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserStatusAsync.UserJoinedChannelAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnUserJoinedChannelAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnUserLeftChannelAsync(IParticipant participant)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserStatusAsync.UserLeftChannelAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnUserLeftChannelAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnUserValuesUpdatedAsync(IParticipant participant)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserStatusAsync.UserValuesUpdatedAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnUserValuesUpdatedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }



        public async Task OnUserMutedAsync(IParticipant participant)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserMutedAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnUserMutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        public async Task OnUserUnmutedAsync(IParticipant participant)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserUnmutedAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnUserUnmutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnUserCrossMutedAsync(AccountId accountId)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserCrossMutedAsync, accountId);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnUserCrossMutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        public async Task OnUserCrossUnmutedAsync(AccountId accountId)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserCrossUnmutedAsync, accountId);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnUserCrossUnmutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnUserSpeakingAsync(IParticipant participant)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserSpeakingAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnUserSpeakingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnUserNotSpeakingAsync(IParticipant participant)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.UserNotSpeakingAsync, participant);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnUserNotSpeakingAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }



        #endregion


        #region User Mute Events

        public async Task OnLocalUserMutedAsync()
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.LocalUserMutedAsync);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnLocalUserMutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnLocalUserMutedAsync<T>(T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.LocalUserMutedAsync, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnLocalUserMutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnLocalUserUnmutedAsync()
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.LocalUserUnmutedAsync);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnLocalUserUnmutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnLocalUserUnmutedAsync<T>(T value)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(UserAudioStatusAsync.LocalUserUnmutedAsync, value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnLocalUserUnmutedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion


        #region Text-to-Speech Events


        public async Task OnTTSMessageAddedAsync(ITTSMessageQueueEventArgs ttsArgs)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextToSpeechStatusAsync.TTSMessageAddedAsync, ttsArgs);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnTTSMessageAddedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnTTSMessageRemovedAsync(ITTSMessageQueueEventArgs ttsArgs)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextToSpeechStatusAsync.TTSMessageRemovedAsync, ttsArgs);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnTTSMessageRemovedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }

        public async Task OnTTSMessageUpdatedAsync(ITTSMessageQueueEventArgs ttsArgs)
        {
            try
            {
                if (_settings.UseDynamicEvents)
                {
                    await InvokeMethodsAsync(TextToSpeechStatusAsync.TTSMessageUpdatedAsync, ttsArgs);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error Invoking events for {nameof(EasyEventsAsync)}.{nameof(OnTTSMessageUpdatedAsync)}");
                Debug.LogException(ex);
                throw;
            }
        }


        #endregion
    }
}
