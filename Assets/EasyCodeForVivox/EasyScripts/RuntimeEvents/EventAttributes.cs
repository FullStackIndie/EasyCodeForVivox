using System;

namespace EasyCodeForVivox
{
    /// <include file='Assets\EasyCodeForVivox\Documentation\XML Documentation\Attributes.xml' path='EasyCode/Attributes[@name="Login"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LoginEventAttribute : Attribute
    {
        public LoginStatus Options { get; set; }

        public LoginEventAttribute(LoginStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ChannelEventAttribute : Attribute
    {
        public ChannelStatus Options { get; set; }

        public ChannelEventAttribute(ChannelStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AudioChannelEventAttribute : Attribute
    {
        public AudioChannelStatus Options { get; set; }

        public AudioChannelEventAttribute(AudioChannelStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TextChannelEventAttribute : Attribute
    {
        public TextChannelStatus Options { get; set; }

        public TextChannelEventAttribute(TextChannelStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ChannelMessageEventAttribute : Attribute
    {
        public ChannelMessageStatus Options { get; set; }

        public ChannelMessageEventAttribute(ChannelMessageStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DirectMessageEventAttribute : Attribute
    {
        public DirectMessageStatus Options { get; set; }

        public DirectMessageEventAttribute(DirectMessageStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UserEventAttribute : Attribute
    {
        public UserStatus Options { get; set; }

        public UserEventAttribute(UserStatus options)
        {
            Options = options;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UserAudioEventAttribute : Attribute
    {
        public UserAudioStatus Options { get; set; }

        public UserAudioEventAttribute(UserAudioStatus options)
        {
            Options = options;
        }
    }

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
