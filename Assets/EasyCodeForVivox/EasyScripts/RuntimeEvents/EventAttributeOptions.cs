namespace EasyCodeForVivox
{
    public enum LoginStatus
    {
        LoggingIn,
        LoggedIn,
        LoggingOut,
        LoggedOut,
    }

    public enum ChannelStatus
    {
        ChannelConnecting,
        ChannelConnected,
        ChannelDisconnecting,
        ChannelDisconnected,
    }
    

    public enum AudioChannelStatus
    {
        AudioChannelConnecting,
        AudioChannelConnected,
        AudioChannelDisconnecting,
        AudioChannelDisconnected,
    }
    

    public enum TextChannelStatus
    {
        TextChannelConnecting,
        TextChannelConnected,
        TextChannelDisconnecting,
        TextChannelDisconnected,
    }
    

    public enum ChannelMessageStatus
    {
        ChannelMessageSent,
        ChannelMessageRecieved,
        EventMessageRecieved
    }
    

    public enum DirectMessageStatus
    {
        DirectMessageSent,
        DirectMessageRecieved,
        DirectMessageFailed
    }
    

    public enum UserStatus
    {
        UserJoinedChannel,
        UserLeftChannel,
        UserValuesUpdated
    }
    

    public enum UserAudioStatus
    {
        UserMuted,
        LocalUserMuted,
        UserUnmuted,
        LocalUserUnmuted,
        UserSpeaking,
        UserNotSpeaking,
    }

    public enum TextToSpeechStatus
    {
        TTSMessageAdded,
        TTSMessageRemoved,
        TTSMessageUpdated,
    }


}
