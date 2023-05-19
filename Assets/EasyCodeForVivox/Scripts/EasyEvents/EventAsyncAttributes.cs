using System;

namespace EasyCodeForVivox.Events
{
    /// <summary>
    ///	Place this on an async void or async Task method to subscribe to Vivox Login Events asynchronously
    /// <para>
    ///	<b>Do not modify any GameObjects, UI, or anything that relies/runs on Unity's main thread</b>
    ///	</para>
    ///	<para>
    ///	Method will be called when chosen <see cref="LoginStatus"/> event happens
    ///	</para>
    ///	<example>
    ///	<code>
    /// [LoginEventAsync(LoginStatus.LoggingIn)]
    /// private async void OnPlayerLoggingInAsync(ILoginSession loginSession)
    /// {
    ///     Debug.Log($"Logging In : {loginSession.LoginSessionId.DisplayName}");
    ///     await GetJoinedLobbies();
    /// }
    ///	</code>
    ///	</example>   
    ///	<para>
    /// Check out the Docs <see href="/docs/events_async/login_events_async.html"> Dynamic Async Events - Login Events</see>
    ///	</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LoginEventAsyncAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the status for the Login event.
        /// </summary>
        public LoginStatus Options { get; set; }

        /// <summary>
        /// EasyCode uses Reflection to find methods that contain <see cref="LoginEventAsyncAttribute"/> and subscribes these methods to Login event. EasyCode then invokes these methods dynamically with the specified options.
        /// </summary>
        /// <param name="options">The Login event status you want to subscribe to.</param>
        public LoginEventAsyncAttribute(LoginStatus options)
        {
            Options = options;
        }
    }


    /// <summary>
    /// Place this on an async void or async Task method to subscribe to Vivox Channel Events asynchronously
    /// <para>
    /// <b>Do not modify any GameObjects, UI, or anything that relies/runs on Unity's main thread</b>
    /// </para>
    /// <para>
    /// Method will be called when chosen <see cref="ChannelStatus"/> event happens
    /// </para>
    /// <example>
    /// <code>
    /// [ChannelEventAsync(ChannelStatus.Connected)]
    /// private async void OnChannelConnectedAsync(IChannelSession channelSession)
    /// {
    ///     Debug.Log($"{channelSession.Channel.Name} Is Connecting");
	///     await LoadPlayerData();
    /// }
    /// </code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events_async/events_async/channel_events_async.html"> Dynamic Async Events - Channel Events</see>
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ChannelEventAsyncAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the status for the Channel event.
        /// </summary>
        public ChannelStatus Options { get; set; }

        /// <summary>
        /// EasyCode uses Reflection to find methods that contain <see cref="ChannelEventAsyncAttribute"/> and subscribes these methods to Channel event. EasyCode then invokes these methods dynamically with the specified options.
        /// </summary>
        /// <param name="options">The Channel event status you want to subscribe to.</param>
        public ChannelEventAsyncAttribute(ChannelStatus options)
        {
            Options = options;
        }
    }


    /// <summary>
    ///	Place this on a async void or async Task  method to subscribe to Vivox Audio Channel Events asynchronously
    ///	<para>
    ///		<b>Do not modify any GameObjects, UI, or anything that relies/runs on Unity's main thread</b>
    ///	</para>
    ///	<para>
    ///		Method will be called when chosen <see cref="AudioChannelStatus"></see> event happens
    ///	</para>
    ///	<example>
    ///		<code>
    ///	[AudioChannelEventAsync(AudioChannelStatus.AudioChannelConnecting)]
    ///	private async void OnAudioChannelConnectingAsync(IChannelSession channelSession)
    ///	{
    ///		Debug.Log($"{channelSession.Channel.Name} Is Connecting");
    ///		await LoadPlayerData();
    ///	}
    ///		</code>
    ///	</example>
    /// <para>
    /// Check out the Docs <see href="/docs/events_async/events_async/audio_channel_events_async.html"> Dynamic Async Events - Audio Channel Events</see>
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AudioChannelEventAsyncAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the status for the Audio Channel event.
        /// </summary>
        public AudioChannelStatus Options { get; set; }

        /// <summary>
        /// EasyCode uses Reflection to find methods that contain <see cref="AudioChannelEventAsyncAttribute"/> and invokes these methods dynamically with the specified options.
        /// </summary>
        /// <param name="options">The status for the Audio Channel event.</param>
        public AudioChannelEventAsyncAttribute(AudioChannelStatus options)
        {
            Options = options;
        }
    }


    /// <summary>
    /// Place this on a async void or async Task  method to subscribe to Vivox Text Channel Events asynchronously
    /// <para>
    /// 	<b>Do not modify any GameObjects, UI, or anything that relies/runs on Unity's main thread</b>
    /// </para>
    /// <para>
    /// 	Method will be called when chosen <see cref="TextChannelStatus"></see> event happens
    /// </para>
    /// <example>
    /// 	<code>
    /// [TextChannelEventAsync(TextChannelStatus.TextChannelConnecting)]
    /// private async void OnTextChannelConnectingAsync(IChannelSession channelSession)
    /// {
    /// 	Debug.Log($"{channelSession.Channel.Name} Is Connecting");
    /// 	await LoadPlayerData();
    /// }
    /// 	</code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events_async/events_async/text_channel_events_async.html"> Dynamic Async Events - Text Channel Events</see>
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TextChannelEventAsyncAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the status for the Text Channel event.
        /// </summary>
        public TextChannelStatus Options { get; set; }

        /// <summary>
        /// EasyCode uses Reflection to find methods that contain <see cref="TextChannelEventAsyncAttribute"/> and invokes these methods dynamically with the specified options.
        /// </summary>
        /// <param name="options">The status for the Text Channel event.</param>
        public TextChannelEventAsyncAttribute(TextChannelStatus options)
        {
            Options = options;
        }
    }


    /// <summary>
    /// Place this on a async void or async Task  method to subscribe to Vivox Channel Message Events asynchronously
    /// <para>
    /// 	<b>Do not modify any GameObjects, UI, or anything that relies/runs on Unity's main thread</b>
    /// </para>
    /// <para>
    /// 	Method will be called when chosen <see cref="ChannelMessageStatus"></see> event happens
    /// </para>
    /// <example>
    /// 	<code>
    /// [ChannelMessageEventAsync(ChannelMessageStatus.ChannelMessageRecieved)]
    /// private async void OnChannelMessageRecievedAsync(IChannelTextMessage textMessage)
    /// {
    /// 	Debug.Log($"From {textMessage.Sender.DisplayName} : {textMessage.ReceivedTime} : {textMessage.Message}");
    /// 	await SavePlayerData();
    /// }
    /// 	</code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events_async/events_async/channel_message_events_async.html"> Dynamic Async Events - Channel Message Events</see>
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ChannelMessageEventAsyncAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the status for the Channel Message event.
        /// </summary>
        public ChannelMessageStatus Options { get; set; }

        /// <summary>
        /// EasyCode uses Reflection to find methods that contain <see cref="ChannelMessageEventAsyncAttribute"/> and invokes these methods dynamically with the specified options.
        /// </summary>
        /// <param name="options">The status for the Channel Message event.</param>
        public ChannelMessageEventAsyncAttribute(ChannelMessageStatus options)
        {
            Options = options;
        }
    }

    /// <summary>
    /// Place this on a async void or async Task  method to subscribe to Vivox Direct Message Events asynchronously
    /// <para>
    /// 	<b>Do not modify any GameObjects, UI, or anything that relies/runs on Unity's main thread</b>
    /// </para>
    /// <para>
    /// 	Method will be called when chosen <see cref="DirectMessageStatus"></see> event happens
    /// </para>
    /// <example>
    /// 	<code>
    /// [DirectMessageEventAsync(DirectMessageStatus.DirectMessageRecieved)]
    /// private async void OnDirectMessageRecievedAsync(IDirectedTextMessage directedTextMessage)
    /// {
    /// 	Debug.Log($"Recived Message From : {directedTextMessage.Sender.DisplayName} : {directedTextMessage.ReceivedTime} : {directedTextMessage.Message}");
    /// 	await SavePlayerData();
    /// }
    /// 	</code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events_async/events_async/direct_message_events_async.html"> Dynamic Async Events - Direct Message Events</see>
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DirectMessageEventAsyncAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the status for the Direct Message event.
        /// </summary>
        public DirectMessageStatus Options { get; set; }

        /// <summary>
        /// EasyCode uses Reflection to find methods that contain <see cref="DirectMessageEventAsyncAttribute"/> and invokes these methods dynamically with the specified options.
        /// </summary>
        /// <param name="options">The status for the Direct Message event.</param>
        public DirectMessageEventAsyncAttribute(DirectMessageStatus options)
        {
            Options = options;
        }
    }

    /// <summary>
    /// 	Place this on a async void or async Task  method to subscribe to Vivox User Events asynchronously
    /// 	<para>
    /// 		<b>Do not modify any GameObjects, UI, or anything that relies/runs on Unity's main thread</b>
    /// 	</para>
    /// 	<para>
    /// 		Method will be called when chosen <see cref="UserStatus"></see> event happens
    /// 	</para>
    /// 	<example>
    /// 		<code>
    /// [UserEventsAsync(UserStatus.LocalUserMuted)]
    /// private async void OnLocalUserMutedAsync()
    /// {
    /// 	Debug.Log("Local User is Muted");
    /// 	await SavePlayerData();
    /// }
    /// 		</code>
    /// 	</example>
    /// <para>
    /// Check out the Docs <see href="/docs/events_async/events_async/user_events_async.html"> Dynamic Async Events - User Events</see>
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UserEventsAsyncAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the status for the User event.
        /// </summary>
        public UserStatus Options { get; set; }

        /// <summary>
        /// EasyCode uses Reflection to find methods that contain <see cref="UserEventsAsyncAttribute"/> and invokes these methods dynamically with the specified options.
        /// </summary>
        /// <param name="options">The status for the User event.</param>
        public UserEventsAsyncAttribute(UserStatus options)
        {
            Options = options;
        }
    }

    /// <summary>
    /// Place this on a async void or async Task  method to subscribe to Vivox Audio Device Events asynchronously
    /// <para>
    /// 	<b>Do not modify any GameObjects, UI, or anything that relies/runs on Unity's main thread</b>
    /// </para>
    /// <para>
    /// 	Method will be called when chosen <see cref="AudioDeviceStatus"></see> event happens
    /// </para>
    /// <example>
    /// 	<code>
    /// [AudioDeviceEventAsync(AudioDeviceStatus.AudioInputDeviceAdded)]
    /// private async void OnAudioInputDeviceAddedAsync(IAudioDevice audioDevice)
    /// {
    /// 	Debug.Log($"Audio Input device has been added {audioDevice?.Name}");
    /// 	await SavePlayerData();
    /// }
    /// 	</code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events_async/events_async/audio_device_events_async.html"> Dynamic Async Events - Audio Device Events</see>
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AudioDeviceEventAsyncAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the status for the Audio Device event.
        /// </summary>
        public AudioDeviceStatus Options { get; set; }

        /// <summary>
        /// EasyCode uses Reflection to find methods that contain <see cref="AudioDeviceEventAsyncAttribute"/> and invokes these methods dynamically with the specified options.
        /// </summary>
        /// <param name="options">The status for the Audio Device event.</param>
        public AudioDeviceEventAsyncAttribute(AudioDeviceStatus options)
        {
            Options = options;
        }
    }


    /// <summary>
    /// Place this on a async void or async Task  method to subscribe to Vivox Text-To-Speech Events asynchronously
    /// <para>
    /// 	<b>Do not modify any GameObjects, UI, or anything that relies/runs on Unity's main thread</b>
    /// </para>
    /// <para>
    /// 	Method will be called when chosen <see cref="TextToSpeechStatus"></see> event happens
    /// </para>
    /// <example>
    /// 	<code>
    /// [TextToSpeechEventAsync(TextToSpeechStatus.TTSMessageAdded)]
    /// private async void  OnTTSMessageAddedAsync(ITTSMessageQueueEventArgs ttsArgs)
    /// {
    /// 	Debug.Log($"TTS Message Has Been Added : {ttsArgs.Message.Text}");
    /// 	await SavePlayerData();
    /// }
    /// 	</code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events_async/events_async/tts_events_async.html"> Dynamic Async Events - Text To Speech Events</see>
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TextToSpeechEventAsyncAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the status for the Text-to-Speech event.
        /// </summary>
        public TextToSpeechStatus Options { get; set; }

        /// <summary>
        /// EasyCode uses Reflection to find methods with that contain <see cref="TextToSpeechEventAsyncAttribute"/> and invokes these methods dynamically with the specified options.
        /// </summary>
        /// <param name="options">The status for the Text-to-Speech event.</param>
        public TextToSpeechEventAsyncAttribute(TextToSpeechStatus options)
        {
            Options = options;
        }
    }


}
