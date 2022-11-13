﻿namespace EasyCodeForVivox
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
        LoginAdded,
        LoginRemoved,
        LoginValuesUpdated
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
        ChannelMessageRecieved,
        ChannelMessageSent,
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
        UserUnmuted,
        UserCrossMuted,
        UserCrossUnmuted,
        LocalUserMuted,
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
        UserValuesUpdatedAsync
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

    internal enum TextToSpeechStatusAsync
    {
        TTSMessageAddedAsync,
        TTSMessageRemovedAsync,
        TTSMessageUpdatedAsync,
    }

}
