using System;

namespace EasyCodeForVivox
{
    /// <include file='Assets\EasyCodeForVivox\Documentation\XML Documentation\Attributes.xml' path='EasyCode/Attributes[@name="Login"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LoginEventAsyncAttribute : Attribute
    {
        public LoginStatus Options { get; set; }

        public LoginEventAsyncAttribute(LoginStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ChannelEventAsyncAttribute : Attribute
    {
        public ChannelStatus Options { get; set; }

        public ChannelEventAsyncAttribute(ChannelStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AudioChannelEventAsyncAttribute : Attribute
    {
        public AudioChannelStatus Options { get; set; }

        public AudioChannelEventAsyncAttribute(AudioChannelStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TextChannelEventAsyncAttribute : Attribute
    {
        public TextChannelStatus Options { get; set; }

        public TextChannelEventAsyncAttribute(TextChannelStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ChannelMessageEventAsyncAttribute : Attribute
    {
        public ChannelMessageStatus Options { get; set; }

        public ChannelMessageEventAsyncAttribute(ChannelMessageStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DirectMessageEventAsyncAttribute : Attribute
    {
        public DirectMessageStatus Options { get; set; }

        public DirectMessageEventAsyncAttribute(DirectMessageStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UserEventAsyncAttribute : Attribute
    {
        public UserStatus Options { get; set; }

        public UserEventAsyncAttribute(UserStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UserAudioEventAsyncAttribute : Attribute
    {
        public UserAudioStatus Options { get; set; }

        public UserAudioEventAsyncAttribute(UserAudioStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TextToSpeechEventAsyncAttribute : Attribute
    {
        public TextToSpeechStatus Options { get; set; }

        public TextToSpeechEventAsyncAttribute(TextToSpeechStatus options)
        {
            Options = options;
        }
    }

}
