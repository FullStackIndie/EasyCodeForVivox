using System;
using VivoxUnity;

/// <summary>
/// All EasyCodeForVivox Events
/// </summary>
public class EasyEvents
{

    #region Login Events

    public static event Action<ILoginSession> LoggingIn;
    public static event Action<ILoginSession> LoggedIn;
    public static event Action<ILoginSession> LoggedOut;
    public static event Action<ILoginSession> LoggingOut;

    #endregion


    #region Channel Events

    public static event Action<IChannelSession> ChannelConnecting;
    public static event Action<IChannelSession> ChannelConnected;
    public static event Action<IChannelSession> ChannelDisconnecting;
    public static event Action<IChannelSession> ChannelDisconnected;

    public static event Action<IChannelSession> TextChannelConnecting;
    public static event Action<IChannelSession> TextChannelConnected;
    public static event Action<IChannelSession> TextChannelDisconnecting;
    public static event Action<IChannelSession> TextChannelDisconnected;

    public static event Action<IChannelSession> VoiceChannelConnecting;
    public static event Action<IChannelSession> VoiceChannelConnected;
    public static event Action<IChannelSession> VoiceChannelDisconnecting;
    public static event Action<IChannelSession> VoiceChannelDisconnected;

    #endregion


    #region Message Events

    public static event Action ChannelMesssageSent;
    public static event Action<IChannelTextMessage> ChannelMessageRecieved;
    public static event Action<IChannelTextMessage> EventMessageRecieved;

    public static event Action DirectMesssageSent;
    public static event Action<IDirectedTextMessage> DirectMessageRecieved;
    public static event Action<IFailedDirectedTextMessage> DirectMessageFailed;

    #endregion


    #region Participant/User Events

    public static event Action<IParticipant> UserJoinedChannel;
    public static event Action<IParticipant> UserLeftChannel;
    public static event Action<IParticipant> UserValuesUpdated;

    public static event Action<IParticipant> UserMuted;
    public static event Action<IParticipant> UserUnmuted;

    public static event Action<IParticipant> UserSpeaking;
    public static event Action<IParticipant> UserNotSpeaking;

    #endregion


    #region User Mute Events

    public static event Action<bool> LocalUserMuted;
    public static event Action<bool> LocalUserUnmuted;

    #endregion


    #region Text-To-Speech Events

    public static event Action<ITTSMessageQueueEventArgs> TTSMessageAdded;
    public static event Action<ITTSMessageQueueEventArgs> TTSMessageRemoved;
    public static event Action<ITTSMessageQueueEventArgs> TTSMessageUpdated;

    #endregion


    #region Subscription Events

    public static event Action<AccountId> SubscriptionAddAllowed;
    public static event Action<AccountId> SubscriptionRemoveAllowed;

    public static event Action<AccountId> SubscriptionAddBlocked;
    public static event Action<AccountId> SubscriptionRemoveBlocked;

    public static event Action<AccountId> SubscriptionAddPresence;
    public static event Action<AccountId> SubscriptionRemovePresence;

    public static event Action<ValueEventArg<AccountId, IPresenceSubscription>> SubscriptionUpdatePresence;

    public static event Action<AccountId> SubscriptionIncomingRequest;

    #endregion






    #region Login Events


    public static void OnLoggingIn(ILoginSession loginSession)
    {
        if (loginSession != null)
        {
            LoggingIn?.Invoke(loginSession);
        }
    }

    public static void OnLoggedIn(ILoginSession loginSession)
    {
        if (loginSession != null)
        {
            LoggedIn?.Invoke(loginSession);
        }
    }


    public static void OnLoggingOut(ILoginSession loginSession)
    {
        if (loginSession != null)
        {
            LoggingOut?.Invoke(loginSession);
        }

    }

    public static void OnLoggedOut(ILoginSession loginSession)
    {
        if (loginSession != null)
        {
            LoggedOut?.Invoke(loginSession);
        }
    }


    #endregion


    #region Channel Event Methods


    public static void OnChannelConnecting(IChannelSession channelSession)
    {
        ChannelConnecting?.Invoke(channelSession);
    }

    public static void OnChannelConnected(IChannelSession channelSession)
    {
        ChannelConnected?.Invoke(channelSession);
    }

    public static void OnChannelDisconnecting(IChannelSession channelSession)
    {
        ChannelDisconnecting?.Invoke(channelSession);
    }

    public static void OnChannelDisconnected(IChannelSession channelSession)
    {
        ChannelDisconnected?.Invoke(channelSession);
    }


    #endregion


    #region Text Channel Events


    public static void OnTextChannelConnecting(IChannelSession channelSession)
    {
        if (channelSession != null)
        {
            TextChannelConnecting?.Invoke(channelSession);
        }
    }

    public static void OnTextChannelConnected(IChannelSession channelSession)
    {
        if (channelSession != null)
        {
            TextChannelConnected?.Invoke(channelSession);
        }
    }

    public static void OnTextChannelDisconnecting(IChannelSession channelSession)
    {
        if (channelSession != null)
        {
            TextChannelDisconnecting?.Invoke(channelSession);
        }
    }

    public static void OnTextChannelDisconnected(IChannelSession channelSession)
    {
        if (channelSession != null)
        {
            TextChannelDisconnected?.Invoke(channelSession);
        }
    }


    #endregion


    #region Voice Channel Events


    public static void OnAudioChannelConnecting(IChannelSession channelSession)
    {
        if (channelSession != null)
        {
            VoiceChannelConnecting?.Invoke(channelSession);
        }
    }

    public static void OnAudioChannelConnected(IChannelSession channelSession)
    {
        if (channelSession != null)
        {
            VoiceChannelConnected?.Invoke(channelSession);
        }
    }

    public static void OnAudioChannelDisconnecting(IChannelSession channelSession)
    {
        if (channelSession != null)
        {
            VoiceChannelDisconnecting?.Invoke(channelSession);
        }
    }

    public static void OnAudioChannelDisconnected(IChannelSession channelSession)
    {
        if (channelSession != null)
        {
            VoiceChannelDisconnected?.Invoke(channelSession);

        }
    }


    #endregion


    #region Message Events


    public static void OnChannelMessageRecieved(IChannelTextMessage channelTextMessage)
    {
        if (channelTextMessage != null)
        {
            ChannelMessageRecieved?.Invoke(channelTextMessage);
        }

    }

    public static void OnEventMessageRecieved(IChannelTextMessage channelTextMessage)
    {
        if (channelTextMessage != null)
        {
            EventMessageRecieved?.Invoke(channelTextMessage);
        }

    }

    public static void OnChannelMessageSent()
    {
        ChannelMesssageSent?.Invoke();
    }

    public static void OnDirectMessageSent()
    {
        DirectMesssageSent?.Invoke();
    }

    public static void OnDirectMessageRecieved(IDirectedTextMessage message)
    {
        if (message != null)
        {
            DirectMessageRecieved?.Invoke(message);
        }

    }

    public static void OnDirectMessageFailed(IFailedDirectedTextMessage failedMessage)
    {
        if (failedMessage != null)
        {
            DirectMessageFailed?.Invoke(failedMessage);
        }

    }


    #endregion


    #region Participant/User Events


    public static void OnUserJoinedChannel(IParticipant participant)
    {
        if (participant != null)
        {
            UserJoinedChannel?.Invoke(participant);
        }
    }

    public static void OnUserLeftChannel(IParticipant participant)
    {
        if (participant != null)
        {
            UserLeftChannel?.Invoke(participant);
        }
    }

    public static void OnUserValuesUpdated(IParticipant participant)
    {
        if (participant != null)
        {
            UserValuesUpdated?.Invoke(participant);
        }
    }



    public static void OnUserMuted(IParticipant participant)
    {
        if (participant != null)
        {
            UserMuted?.Invoke(participant);
        }
    }

    public static void OnUserUnmuted(IParticipant participant)
    {
        if (participant != null)
        {
            UserUnmuted?.Invoke(participant);
        }
    }

    public static void OnUserSpeaking(IParticipant participant)
    {
        if (participant != null)
        {
            UserSpeaking?.Invoke(participant);
        }
    }

    public static void OnUserNotSpeaking(IParticipant participant)
    {
        if (participant != null)
        {
            UserNotSpeaking?.Invoke(participant);
        }

    }


    #endregion


    #region User Mute Events

    public static void OnLocalUserMuted(bool isMuted)
    {
        LocalUserMuted?.Invoke(isMuted);
    }

    public static void OnLocalUserUnmuted(bool isMuted)
    {
        LocalUserUnmuted?.Invoke(isMuted);
    }
    #endregion


    #region Text-to-Speech Events


    public static void OnTTSMessageAdded(ITTSMessageQueueEventArgs ttsArgs)
    {
        if (ttsArgs != null)
        {
            TTSMessageAdded?.Invoke(ttsArgs);
        }
    }

    public static void OnTTSMessageRemoved(ITTSMessageQueueEventArgs ttsArgs)
    {
        if (ttsArgs != null)
        {
            TTSMessageRemoved?.Invoke(ttsArgs);
        }
    }

    public static void OnTTSMessageUpdated(ITTSMessageQueueEventArgs ttsArgs)
    {
        if (ttsArgs != null)
        {
            TTSMessageUpdated?.Invoke(ttsArgs);
        }
    }


    #endregion


    #region Subscription / Presence Events


    public static void OnAddAllowedSubscription(AccountId accountId)
    {
        if (accountId != null)
        {
            SubscriptionAddAllowed?.Invoke(accountId);
        }
    }

    public static void OnRemoveAllowedSubscription(AccountId accountId)
    {
        if (accountId != null)
        {
            SubscriptionRemoveAllowed?.Invoke(accountId);
        }
    }

    public static void OnAddPresenceSubscription(AccountId accountId)
    {
        if (accountId != null)
        {
            SubscriptionAddPresence?.Invoke(accountId);
        }
    }

    public static void OnRemovePresenceSubscription(AccountId accountId)
    {
        if (accountId != null)
        {
            SubscriptionRemovePresence?.Invoke(accountId);
        }
    }

    public static void OnUpdatePresenceSubscription(ValueEventArg<AccountId, IPresenceSubscription> presence)
    {
        if (presence != null)
        {
            SubscriptionUpdatePresence?.Invoke(presence);
        }
    }

    public static void OnAddBlockedSubscription(AccountId accountId)
    {
        if (accountId != null)
        {
            SubscriptionAddBlocked?.Invoke(accountId);
        }
    }

    public static void OnRemoveBlockedSubscription(AccountId accountId)
    {
        if (accountId != null)
        {
            SubscriptionRemoveBlocked?.Invoke(accountId);
        }
    }


    public static void OnIncomingSubscription(AccountId subRequest)
    {
        if (subRequest != null)
        {
            SubscriptionIncomingRequest?.Invoke(subRequest);
        }
    }


    #endregion


}
