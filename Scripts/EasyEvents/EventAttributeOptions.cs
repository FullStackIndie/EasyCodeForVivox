namespace EasyCodeForVivox
{
    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptions.xml' path='EasyCode/Options[@name="LoginStatus"]'/>
    public enum LoginStatus
    {
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="LoggingIn"]'/>
        LoggingIn,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="LoggedIn"]'/>
        LoggedIn,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="LoggingOut"]'/>
        LoggingOut,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="LoggedOut"]'/>
        LoggedOut,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="LoginAdded"]'/>
        LoginAdded,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="LoginRemoved"]'/>
        LoginRemoved,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="LoginValuesUpdated"]'/>
        LoginValuesUpdated
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptions.xml' path='EasyCode/Options[@name="ChannelStatus"]'/>
    public enum ChannelStatus
    {
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="ChannelConnecting"]'/>
        ChannelConnecting,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="ChannelConnected"]'/>
        ChannelConnected,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="ChannelDisconnecting"]'/>
        ChannelDisconnecting,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="ChannelDisconnected"]'/>
        ChannelDisconnected,
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptions.xml' path='EasyCode/Options[@name="AudioChannelStatus"]'/>
    public enum AudioChannelStatus
    {        
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="AudioChannelConnecting"]'/>
        AudioChannelConnecting,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="AudioChannelConnected"]'/>
        AudioChannelConnected,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="AudioChannelDisconnecting"]'/>
        AudioChannelDisconnecting,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="AudioChannelDisconnected"]'/>
        AudioChannelDisconnected,
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptions.xml' path='EasyCode/Options[@name="TextChannelStatus"]'/>
    public enum TextChannelStatus
    {
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="TextChannelConnecting"]'/>
        TextChannelConnecting,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="TextChannelConnected"]'/>
        TextChannelConnected,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="TextChannelDisconnecting"]'/>
        TextChannelDisconnecting,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="TextChannelDisconnected"]'/>
        TextChannelDisconnected,
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptions.xml' path='EasyCode/Options[@name="ChannelMessageStatus"]'/>
    public enum ChannelMessageStatus
    {
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="ChannelMessageRecieved"]'/>
        ChannelMessageRecieved,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="ChannelMessageSent"]'/>
        ChannelMessageSent,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="EventMessageRecieved"]'/>
        EventMessageRecieved
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptions.xml' path='EasyCode/Options[@name="DirectMessageStatus"]'/>
    public enum DirectMessageStatus
    {
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="DirectMessageSent"]'/>
        DirectMessageSent,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="DirectMessageRecieved"]'/>
        DirectMessageRecieved,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="DirectMessageFailed"]'/>
        DirectMessageFailed
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptions.xml' path='EasyCode/Options[@name="UserStatus"]'/>
    public enum UserStatus
    {
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="UserJoinedChannel"]'/>
        UserJoinedChannel,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="UserLeftChannel"]'/>
        UserLeftChannel,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="UserValuesUpdated"]'/>
        UserValuesUpdated,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="UserMuted"]'/>
        UserMuted,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="UserUnmuted"]'/>
        UserUnmuted,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="UserCrossMuted"]'/>
        UserCrossMuted,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="UserCrossUnmuted"]'/>
        UserCrossUnmuted,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="UserSpeaking"]'/>
        UserSpeaking,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="UserNotSpeaking"]'/>
        UserNotSpeaking,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="LocalUserMuted"]'/>
        LocalUserMuted,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="LocalUserUnmuted"]'/>
        LocalUserUnmuted,
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptions.xml' path='EasyCode/Options[@name="AudioDeviceStatus"]'/>
    public enum AudioDeviceStatus
    {
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="AudioInputDeviceAdded"]'/>
        AudioInputDeviceAdded,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="AudioInputDeviceRemoved"]'/>
        AudioInputDeviceRemoved,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="AudioInputDeviceUpdated"]'/>
        AudioInputDeviceUpdated,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="AudioOutputDeviceAdded"]'/>
        AudioOutputDeviceAdded,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="AudioOutputDeviceRemoved"]'/>
        AudioOutputDeviceRemoved,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="AudioOutputDeviceUpdated"]'/>
        AudioOutputDeviceUpdated,
    }

    /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptions.xml' path='EasyCode/Options[@name="TextToSpeechStatus"]'/>
    public enum TextToSpeechStatus
    {
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="TTSMessageAdded"]'/>
        TTSMessageAdded,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="TTSMessageRemoved"]'/>
        TTSMessageRemoved,
        /// <include file='Assets\EasyCodeForVivox\Documentation\XML API Documentation\AttributeOptionEvents.xml' path='EasyCode/Events[@name="TTSMessageUpdated"]'/>
        TTSMessageUpdated,
    }

}


namespace EasyCodeForVivox.Events.Internal
{

    internal enum LoginStatusAsync
    {
        LoggingInAsync,
        LoggedInAsync,
        LoggingOutAsync,
        LoggedOutAsync,
        LoginAddedAsync,
        LoginRemovedAsync,
        LoginValuesUpdatedAsync
    }

    internal enum ChannelStatusAsync
    {
        ChannelConnectingAsync,
        ChannelConnectedAsync,
        ChannelDisconnectingAsync,
        ChannelDisconnectedAsync,
    }

    internal enum AudioChannelStatusAsync
    {
        AudioChannelConnectingAsync,
        AudioChannelConnectedAsync,
        AudioChannelDisconnectingAsync,
        AudioChannelDisconnectedAsync,
    }

    internal enum TextChannelStatusAsync
    {
        TextChannelConnectingAsync,
        TextChannelConnectedAsync,
        TextChannelDisconnectingAsync,
        TextChannelDisconnectedAsync,
    }

    internal enum ChannelMessageStatusAsync
    {
        ChannelMessageRecievedAsync,
        ChannelMessageSentAsync,
        EventMessageRecievedAsync
    }

    internal enum DirectMessageStatusAsync
    {
        DirectMessageSentAsync,
        DirectMessageRecievedAsync,
        DirectMessageFailedAsync
    }

    internal enum UserStatusAsync
    {
        UserJoinedChannelAsync,
        UserLeftChannelAsync,
        UserValuesUpdatedAsync,
        UserMutedAsync,
        UserUnmutedAsync,
        UserCrossMutedAsync,
        UserCrossUnmutedAsync,
        UserSpeakingAsync,
        UserNotSpeakingAsync,
        LocalUserMutedAsync,
        LocalUserUnmutedAsync,
    }

    internal enum AudioDeviceStatusAsync
    {
        AudioInputDeviceAddedAsync,
        AudioInputDeviceRemovedAsync,
        AudioInputDeviceUpdatedAsync,
        AudioOutputDeviceAddedAsync,
        AudioOutputDeviceRemovedAsync,
        AudioOutputDeviceUpdatedAsync,
    }

    internal enum TextToSpeechStatusAsync
    {
        TTSMessageAddedAsync,
        TTSMessageRemovedAsync,
        TTSMessageUpdatedAsync,
    }

}
