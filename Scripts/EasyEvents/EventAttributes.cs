using System;

namespace EasyCodeForVivox
{
    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\Attributes.xml' path='EasyCode/Attributes[@name="LoginEvent"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LoginEventAttribute : Attribute
    {
        public LoginStatus Options { get; set; }

        public LoginEventAttribute(LoginStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\Attributes.xml' path='EasyCode/Attributes[@name="ChannelEvent"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ChannelEventAttribute : Attribute
    {
        public ChannelStatus Options { get; set; }

        public ChannelEventAttribute(ChannelStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\Attributes.xml' path='EasyCode/Attributes[@name="AudioChannelEvent"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AudioChannelEventAttribute : Attribute
    {
        public AudioChannelStatus Options { get; set; }

        public AudioChannelEventAttribute(AudioChannelStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\Attributes.xml' path='EasyCode/Attributes[@name="TextChannelEvent"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TextChannelEventAttribute : Attribute
    {
        public TextChannelStatus Options { get; set; }

        public TextChannelEventAttribute(TextChannelStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\Attributes.xml' path='EasyCode/Attributes[@name="ChannelMessageEvent"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ChannelMessageEventAttribute : Attribute
    {
        public ChannelMessageStatus Options { get; set; }

        public ChannelMessageEventAttribute(ChannelMessageStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\Attributes.xml' path='EasyCode/Attributes[@name="DirectMessageEvent"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DirectMessageEventAttribute : Attribute
    {
        public DirectMessageStatus Options { get; set; }

        public DirectMessageEventAttribute(DirectMessageStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\Attributes.xml' path='EasyCode/Attributes[@name="UserEvents"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UserEventsAttribute : Attribute
    {
        public UserStatus Options { get; set; }

        public UserEventsAttribute(UserStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\Attributes.xml' path='EasyCode/Attributes[@name="AudioDeviceEvent"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AudioDeviceEventAttribute : Attribute
    {
        public AudioDeviceStatus Options { get; set; }

        public AudioDeviceEventAttribute(AudioDeviceStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\Attributes.xml' path='EasyCode/Attributes[@name="TextToSpeechEvent"]'/>
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
