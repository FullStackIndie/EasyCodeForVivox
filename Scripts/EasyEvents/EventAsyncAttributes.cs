using System;

namespace EasyCodeForVivox
{
    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributesAsync.xml' path='EasyCode/AttributesAsync[@name="LoginEventAsync"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LoginEventAsyncAttribute : Attribute
    {
        public LoginStatus Options { get; set; }

        public LoginEventAsyncAttribute(LoginStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributesAsync.xml' path='EasyCode/AttributesAsync[@name="ChannelEventAsync"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ChannelEventAsyncAttribute : Attribute
    {
        public ChannelStatus Options { get; set; }

        public ChannelEventAsyncAttribute(ChannelStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributesAsync.xml' path='EasyCode/AttributesAsync[@name="AudioChannelEventAsync"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AudioChannelEventAsyncAttribute : Attribute
    {
        public AudioChannelStatus Options { get; set; }

        public AudioChannelEventAsyncAttribute(AudioChannelStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributesAsync.xml' path='EasyCode/AttributesAsync[@name="TextChannelEventAsync"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TextChannelEventAsyncAttribute : Attribute
    {
        public TextChannelStatus Options { get; set; }

        public TextChannelEventAsyncAttribute(TextChannelStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributesAsync.xml' path='EasyCode/AttributesAsync[@name="ChannelMessageEventAsync"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ChannelMessageEventAsyncAttribute : Attribute
    {
        public ChannelMessageStatus Options { get; set; }

        public ChannelMessageEventAsyncAttribute(ChannelMessageStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributesAsync.xml' path='EasyCode/AttributesAsync[@name="DirectMessageEventAsync"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DirectMessageEventAsyncAttribute : Attribute
    {
        public DirectMessageStatus Options { get; set; }

        public DirectMessageEventAsyncAttribute(DirectMessageStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributesAsync.xml' path='EasyCode/AttributesAsync[@name="UserEvents"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UserEventsAsyncAttribute : Attribute
    {
        public UserStatus Options { get; set; }

        public UserEventsAsyncAttribute(UserStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributesAsync.xml' path='EasyCode/AttributesAsync[@name="AudioDeviceEventAsync"]'/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AudioDeviceEventAsyncAttribute : Attribute
    {
        public AudioDeviceStatus Options { get; set; }

        public AudioDeviceEventAsyncAttribute(AudioDeviceStatus options)
        {
            Options = options;
        }
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributesAsync.xml' path='EasyCode/AttributesAsync[@name="TextToSpeechEventAsync"]'/>
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
