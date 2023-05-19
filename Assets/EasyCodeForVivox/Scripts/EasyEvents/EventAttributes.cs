using System;

namespace EasyCodeForVivox.Events
{
    /// <summary>
    /// Place this on a method to subscribe to Vivox Login Events
    /// <para>
    /// Method will be called when chosen <see cref="LoginStatus"/> event happens
    /// </para>
    /// <example>
    /// <code>
    /// [LoginEvent(LoginStatus.LoggedIn)]"
    /// public void UserLoggedIn(ILoginSession loginSession)
    /// {
    ///     $"Logged In {loginSession.LoginSessionId.DisplayName}";
    /// }
    /// </code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events/login_events.html"> Login Events</see>
    ///	</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LoginEventAttribute : Attribute
    {
        public LoginStatus Options { get; set; }

        public LoginEventAttribute(LoginStatus options)
        {
            Options = options;
        }
    }



    /// <summary>
    /// Place this on a method to subscribe to Vivox Channel Events 
    /// <para>
    /// 	Method will be called when chosen <see cref="ChannelStatus"></see> event happens
    /// </para>
    /// <br>
    /// 	Example Method
    /// </br>
    /// <example>
    /// <code>
    /// [ChannelEvent(ChannelStatus.ChannelConnected)]
    /// private void OnChannelConnected(IChannelSession channelSession)
    /// {
    ///     Debug.Log($"{channelSession.Channel.Name} Has Connected : Channel Type == {channelSession.Channel.Type}");
    /// }
    /// </code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events/channel_events.html"> Channel Events</see>
    ///	</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ChannelEventAttribute : Attribute
    {
        public ChannelStatus Options { get; set; }

        public ChannelEventAttribute(ChannelStatus options)
        {
            Options = options;
        }
    }


    /// <summary>
    /// Place this on a method to subscribe to Vivox Audio Channel Events 
    /// <para>
    /// 	Method will be called when chosen <see cref="AudioChannelStatus"></see> event happens
    /// </para>
    /// <br>
    /// 	Example Method
    /// </br>
    /// <example>
    /// <code>
    /// [AudioChannelEvent(AudioChannelStatus.AudioChannelConnecting)]
    /// private void OnAudioChannelConnecting(IChannelSession channelSession)
    /// {
    /// 	Debug.Log($"{channelSession.Channel.Name} Is Connecting");
    /// }
    /// </code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events/audio_channel_events.html"> Audio Channel Events</see>
    ///	</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AudioChannelEventAttribute : Attribute
    {
        public AudioChannelStatus Options { get; set; }

        public AudioChannelEventAttribute(AudioChannelStatus options)
        {
            Options = options;
        }
    }


    /// <summary>
    /// Place this on a method to subscribe to Vivox Text Channel Events 
    /// <para>
    /// 	Method will be called when chosen <see cref="TextChannelStatus"></see> event happens
    /// </para>
    /// <br>
    /// 	Example Method
    /// </br>
    /// <example>
    /// <code>
    /// [TextChannelEvent(TextChannelStatus.TextChannelConnecting)]
    /// private void OnTextChannelConnecting(IChannelSession channelSession)
    /// {
    /// 	Debug.Log($"{channelSession.Channel.Name} Is Connecting");
    /// }
    /// </code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events/text_channel_events.html"> Text Channel Events</see>
    ///	</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TextChannelEventAttribute : Attribute
    {
        public TextChannelStatus Options { get; set; }

        public TextChannelEventAttribute(TextChannelStatus options)
        {
            Options = options;
        }
    }


    /// <summary>
    /// Place this on a method to subscribe to Vivox Channel Message Events
    /// <para>
    /// Method will be called when chosen <see cref="ChannelMessageStatus"></see> event happens
    /// </para>
    /// <br>
    /// Example Method
    /// </br>
    /// <example>
    /// <code>
    /// [ChannelMessageEvent(ChannelMessageStatus.ChannelMessageRecieved)]
    /// private void OnChannelMessageRecieved(IChannelTextMessage textMessage)
    /// {
    ///     Debug.Log($"From {textMessage.Sender.DisplayName} : {textMessage.ReceivedTime} : {textMessage.Message}");
    /// }
    /// </code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events/channel_message_events.html"> Channel Message Events</see>
    ///	</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ChannelMessageEventAttribute : Attribute
    {
        public ChannelMessageStatus Options { get; set; }

        public ChannelMessageEventAttribute(ChannelMessageStatus options)
        {
            Options = options;
        }
    }


    /// <summary>
    /// Place this on a method to subscribe to Vivox Direct Message Events 
    /// <para>
    /// Method will be called when chosen <see cref="DirectMessageStatus"></see> event happens
    /// </para>
    /// <br>
    /// Example Method
    /// </br>
    /// <example>
    /// <code>
    /// [DirectMessageEvent(DirectMessageStatus.DirectMessageRecieved)]
    /// private void OnDirectMessageRecieved(IDirectedTextMessage directedTextMessage)
    /// {
    ///     Debug.Log($"Recived Message From : {directedTextMessage.Sender.DisplayName} : {directedTextMessage.ReceivedTime} : {directedTextMessage.Message}");
    /// }
    /// </code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events/directed_message_events.html"> Directed Message Events</see>
    ///	</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DirectMessageEventAttribute : Attribute
    {
        public DirectMessageStatus Options { get; set; }

        public DirectMessageEventAttribute(DirectMessageStatus options)
        {
            Options = options;
        }
    }


    /// <summary>
    /// Place this on a method to subscribe to Vivox User Events 
    /// <para>
    /// Online Docs (<see href="https://fullstackindie.gitbook.io/easy-code-for-vivox/easy-code-for-vivox/how-do-i-do-this-in-easycode/subscribe-to-user-events#dynamic-events-1">Dynamic Events - User Participant Events</see>)
    /// </para>
    /// <para>
    /// Method will be called when chosen <see cref="UserStatus"></see> event happens
    /// </para>
    /// <br>
    /// Example Method
    /// </br>
    /// <example>
    /// <code>
    /// [UserEvents(UserStatus.UserMuted)]
    /// private void OnUserMuted(IParticipant participant)
    /// {
    ///     Debug.Log($"{participant.Account.DisplayName} Is Muted : (Muted For All : {participant.IsMutedForAll})");
    /// }
    /// </code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events/user_events.html"> User Events</see>
    ///	</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UserEventsAttribute : Attribute
    {
        public UserStatus Options { get; set; }

        public UserEventsAttribute(UserStatus options)
        {
            Options = options;
        }
    }


    /// <summary>
    /// Place this on a method to subscribe to Vivox Audio Device Events
    /// <para>
    /// Method will be called when chosen <see cref="AudioDeviceStatus"></see> event happens
    /// </para>
    /// <br>
    /// Example Method
    /// </br>
    /// <example>
    /// <code>
    /// [AudioDeviceEvent(AudioDeviceStatus.AudioInputDeviceAdded)]
    /// private void OnAudioInputDeviceAdded(IAudioDevice audioDevice)
    /// {
    ///     Debug.Log($"Audio Input device has been added {audioDevice?.Name}");
    /// }
    /// </code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events/audio_device_events.html"> Audio Device Events</see>
    ///	</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AudioDeviceEventAttribute : Attribute
    {
        public AudioDeviceStatus Options { get; set; }

        public AudioDeviceEventAttribute(AudioDeviceStatus options)
        {
            Options = options;
        }
    }


    /// <summary>
    /// Place this on a method to subscribe to Vivox Text-To-Speech Events
    /// <para>
    /// Method will be called when chosen <see cref="TextToSpeechStatus"></see> event happens
    /// </para>
    /// <br>
    /// Example Method
    /// </br>
    /// <example>
    /// <code>
    /// [TextToSpeechEvent(TextToSpeechStatus.TTSMessageAdded)]
    /// private void OnTTSMessageAdded(ITTSMessageQueueEventArgs ttsArgs)
    /// {
    ///     Debug.Log($"TTS Message Has Been Added : {ttsArgs.Message.Text}");
    /// }
    /// </code>
    /// </example>
    /// <para>
    /// Check out the Docs <see href="/docs/events/tts_events.html"> Text To Speech Events</see>
    ///	</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TextToSpeechEventAttribute : Attribute
    {
        public TextToSpeechStatus Options { get; set; }

        public TextToSpeechEventAttribute(TextToSpeechStatus options)
        {
            Options = options;
        }
    }

}
