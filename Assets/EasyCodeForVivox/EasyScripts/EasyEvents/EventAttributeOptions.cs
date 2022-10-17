namespace EasyCodeForVivox
{
    /// <summary>
    /// Login Event that you want this method to be called/subscribed to
    /// </summary>
    public enum LoginStatus
    {
        LoggingIn,
        LoggedIn,
        LoggingOut,
        LoggedOut,
    }

    internal enum LoginStatusAsync
    {
        LoggingInAsync,
        LoggedInAsync,
        LoggingOutAsync,
        LoggedOutAsync,
    }

    public enum ChannelStatus
    {
        ChannelConnecting,
        ChannelConnected,
        ChannelDisconnecting,
        ChannelDisconnected,
    }

    internal enum ChannelStatusAsync
    {
        ChannelConnectingAsync,
        ChannelConnectedAsync,
        ChannelDisconnectingAsync,
        ChannelDisconnectedAsync,
    }


    public enum AudioChannelStatus
    {
        AudioChannelConnecting,
        AudioChannelConnected,
        AudioChannelDisconnecting,
        AudioChannelDisconnected,
    }
    

    internal enum AudioChannelStatusAsync
    {
        AudioChannelConnectingAsync,
        AudioChannelConnectedAsync,
        AudioChannelDisconnectingAsync,
        AudioChannelDisconnectedAsync,
    }


    public enum TextChannelStatus
    {
        TextChannelConnecting,
        TextChannelConnected,
        TextChannelDisconnecting,
        TextChannelDisconnected,
    }

    internal enum TextChannelStatusAsync
    {
        TextChannelConnectingAsync,
        TextChannelConnectedAsync,
        TextChannelDisconnectingAsync,
        TextChannelDisconnectedAsync,
    }


    public enum ChannelMessageStatus
    {
        ChannelMessageSent,
        ChannelMessageRecieved,
        EventMessageRecieved
    }

    internal enum ChannelMessageStatusAsync
    {
        ChannelMessageSentAsync,
        ChannelMessageRecievedAsync,
        EventMessageRecievedAsync
    }


    public enum DirectMessageStatus
    {
        DirectMessageSent,
        DirectMessageRecieved,
        DirectMessageFailed
    }

    internal enum DirectMessageStatusAsync
    {
        DirectMessageSentAsync,
        DirectMessageRecievedAsync,
        DirectMessageFailedAsync
    }


    public enum UserStatus
    {
        UserJoinedChannel,
        UserLeftChannel,
        UserValuesUpdated
    }

    internal enum UserStatusAsync
    {
        UserJoinedChannelAsync,
        UserLeftChannelAsync,
        UserValuesUpdatedAsync
    }


    public enum UserAudioStatus
    {
        UserMuted,
        UserUnmuted,
        UserCrossMuted,
        UserCrossUnmuted,
        LocalUserMuted,
        LocalUserUnmuted,
        UserSpeaking,
        UserNotSpeaking,
    }

    internal enum UserAudioStatusAsync
    {
        UserMutedAsync,
        UserUnmutedAsync,
        UserCrossMutedAsync,
        UserCrossUnmutedAsync,
        LocalUserMutedAsync,
        LocalUserUnmutedAsync,
        UserSpeakingAsync,
        UserNotSpeakingAsync,
    }

    public enum TextToSpeechStatus
    {
        TTSMessageAdded,
        TTSMessageRemoved,
        TTSMessageUpdated,
    }

    internal enum TextToSpeechStatusAsync
    {
        TTSMessageAddedAsync,
        TTSMessageRemovedAsync,
        TTSMessageUpdatedAsync,
    }


}
