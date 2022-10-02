using System;

namespace EasyCodeForVivox
{
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
    public class VoiceChannelEventAttribute : Attribute
    {
        public VoiceChannelStatus Options { get; set; }

        public VoiceChannelEventAttribute(VoiceChannelStatus options)
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

}
