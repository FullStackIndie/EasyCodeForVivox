namespace EasyCodeForVivox.Events
{

    /// <summary>
    /// The type of <b>Vivox Login Event</b> you want this method to be subscribed to. Works for <see cref="LoginEventAttribute"/> and <see cref="LoginEventAsyncAttribute"/>
    /// </summary>
    public enum LoginStatus
    {
        /// <summary>
        /// Event is invoked/fired when player begins logging into Vivox
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.ILoginSession.html">VivoxUnity.ILoginSession</see>
        /// </para>
        /// </summary>
        LoggingIn,
        /// <summary>
        /// Event is invoked/fired when player is successfully logged into Vivox
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.ILoginSession.html">VivoxUnity.ILoginSession</see>
        /// </para>
        /// </summary>
        LoggedIn,
        /// <summary>
        /// Event is invoked/fired when player begins logging out of Vivox
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.ILoginSession.html">VivoxUnity.ILoginSession</see>
        /// </para>
        /// </summary>
        LoggingOut,
        /// <summary>
        /// Event is invoked/fired when player is successfully logged out of Vivox
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.ILoginSession.html">VivoxUnity.ILoginSession</see>
        /// </para>
        /// </summary>
        LoggedOut,
        /// <summary>
        /// Event is invoked/fired when player is successfully logged into Vivox
        /// <para>A player can log in multiple times under different usernames. Each time a new LoginSession is created EasyCode will keep track of the newly added LoginSessions</para>
        /// <br>
        /// 	You can access all current LoginSessions with <see cref="EasySession"/>
        /// </br>
        /// <example>
        /// <code>
        ///     EasySession.LoginSessions["userName"]
        /// </code>
        /// </example>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.AccountId.html">VivoxUnity.AccountId</a></para>
        /// </summary>
        LoginAdded,
        /// <summary>
        /// Event is invoked/fired when player is successfully logged out of Vivox
        /// <para>EasyCode will keep track of LoginSessions and automatically remove the LoginSession of the logged out player</para>
        /// <br>
        /// 	You can attempt to access a current LoginSession with <see cref="EasySession"/> to see if it exists. If it does <b>loginSession</b> will not be null
        /// </br>
        /// <example>
        /// <code>
        ///     EasySession.LoginSessions.TryGetValue("userName", out ILoginSession loginSession);
        /// </code>
        /// </example>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.AccountId.html">VivoxUnity.AccountId</a></para>
        /// </summary>
        LoginRemoved,
        /// <summary>
        /// Event is invoked/fired when player LoginSession has changed such as player has changed their name
        /// <para>EasyCode will keep track of LoginSessions automatically</para>
        /// <br>
        /// 	You can attempt to access a current LoginSession with EasySession to see if it exists. If it does <b>loginSession</b> will not be null
        /// </br>
        /// <example>
        /// <code>
        /// 	EasySession.LoginSessions.TryGetValue("userName", out ILoginSession loginSession);
        /// </code>
        /// </example>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.ILoginSession.html">VivoxUnity.ILoginSession</a></para>
        /// </summary>
        LoginValuesUpdated
    }


    /// <summary>
    /// The type of <b>Vivox Channel Event</b> that you want this method to be subscribed to. Works for <see cref="ChannelEventAttribute"/> and <see cref="ChannelEventAsyncAttribute"/>.
    /// </summary>
    public enum ChannelStatus
    {
        /// <summary>
        /// Event is invoked/fired when player begins joining a Vivox Channel
        /// <para>Will be fired for Echo, Non-Positional, and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelSession.html">VivoxUnity.IChannelSession</a></para>
        /// </summary>
        ChannelConnecting,
        /// <summary>
        /// Event is invoked/fired when player has successfully joined a Vivox Channel
        /// <para>Will be fired for Echo, Non-Positional, and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelSession.html">VivoxUnity.IChannelSession</a></para>
        /// </summary>
        ChannelConnected,
        /// <summary>
        /// Event is invoked/fired when player begins disconnecting from a Vivox Channel
        /// <para>Will be fired for Echo, Non-Positional, and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelSession.html">VivoxUnity.IChannelSession</a></para>
        /// </summary>
        ChannelDisconnecting,
        /// <summary>
        /// Event is invoked/fired when player has successfully disconnected from a Vivox Channel
        /// <para>Will be fired for Echo, Non-Positional, and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelSession.html">VivoxUnity.IChannelSession</a></para>
        /// </summary>
        ChannelDisconnected,
    }



    /// <summary>
    /// The type of <b>Vivox Audio Channel Event</b> that you want this method to be subscribed to. Works for <see cref="AudioChannelEventAttribute"/> and <see cref="AudioChannelEventAsyncAttribute"/>.
    /// </summary>
    public enum AudioChannelStatus
    {
        /// <summary>
        /// Event is invoked/fired when player begins joining a Vivox Audio Channel
        /// <para>Will be fired for Echo, Non-Positional, and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelSession.html">VivoxUnity.IChannelSession</a></para>
        /// </summary>
        AudioChannelConnecting,
        /// <summary>
        /// Event is invoked/fired when player has successfully joined a Vivox Audio Channel
        /// <para>Will be fired for Echo, Non-Positional, and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelSession.html">VivoxUnity.IChannelSession</a></para>
        /// </summary>
        AudioChannelConnected,
        /// <summary>
        /// Event is invoked/fired when player begins disconnecting from a Vivox Audio Channel
        /// <para>Will be fired for Echo, Non-Positional, and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelSession.html">VivoxUnity.IChannelSession</a></para>
        /// </summary>
        AudioChannelDisconnecting,
        /// <summary>
        /// Event is invoked/fired when player has successfully disconnected from a Vivox Audio Channel
        /// <para>Will be fired for Echo, Non-Positional, and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelSession.html">VivoxUnity.IChannelSession</a></para>
        /// </summary>
        AudioChannelDisconnected,
    }


    /// <summary>
    /// The type of <b>Vivox Text Channel Event</b> that you want this method to be subscribed to. Works for <see cref="TextChannelEventAttribute"/> and <see cref="TextChannelEventAsyncAttribute"/>.
    /// </summary>
    public enum TextChannelStatus
    {
        /// <summary>
        /// Event is invoked/fired when player begins joining a Vivox Text Channel
        /// <para>Will be fired for Echo, Non-Positional, and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelSession.html">VivoxUnity.IChannelSession</a></para>
        /// </summary>
        TextChannelConnecting,
        /// <summary>
        /// Event is invoked/fired when player has successfully joined a Vivox Text Channel
        /// <para>Will be fired for Echo, Non-Positional, and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelSession.html">VivoxUnity.IChannelSession</a></para>
        /// </summary>
        TextChannelConnected,
        /// <summary>
        /// Event is invoked/fired when player begins disconnecting from a Vivox Text Channel
        /// <para>Will be fired for Echo, Non-Positional, and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelSession.html">VivoxUnity.IChannelSession</a></para>
        /// </summary>
        TextChannelDisconnecting,
        /// <summary>
        /// Event is invoked/fired when player has successfully disconnected from a Vivox Text Channel
        /// <para>Will be fired for Echo, Non-Positional, and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelSession.html">VivoxUnity.IChannelSession</a></para>
        /// </summary>
        TextChannelDisconnected,
    }



    /// <summary>
    /// The type of <b>Vivox Channel Message Event</b> that you want this method to be subscribed to. Works for <see cref="ChannelMessageEventAttribute"/> and <see cref="ChannelMessageEventAsyncAttribute"/>.
    /// </summary>
    public enum ChannelMessageStatus
    {
        /// <summary>
        /// Event is invoked/fired when player recieves a message from a connected Vivox Text Channel
        /// <para>Will be fired for Non-Positional and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelTextMessage.html">VivoxUnity.IChannelTextMessage</a></para>
        /// </summary>
        ChannelMessageRecieved,
        /// <summary>
        /// Event is invoked/fired when player sends a message from a connected Vivox Text Channel
        /// <para>Will be fired for Non-Positional and 3D Positional channels</para>
        /// <para>
        /// Method must have <b>0</b> parameters
        /// </para>
        /// </summary>
        ChannelMessageSent,
        /// <summary>
        /// Event is invoked/fired when developer wants to send a secret message in a connected Vivox Text Channel that players wont see
        /// <para>If using a networking stack like NetCodeForGameObjects it is better to send a message with NetCode than with Vivox</para>
        /// <para>Will be fired for Non-Positional and 3D Positional channels</para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <a href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IChannelTextMessage.html">VivoxUnity.IChannelTextMessage</a></para>
        /// </summary>
        EventMessageRecieved
    }


    /// <summary>
    /// The type of <b>Vivox Direct Message Event</b> that you want this method to be subscribed to. Works for <see cref="DirectMessageEventAttribute"/> and <see cref="DirectMessageEventAsyncAttribute"/>.
    /// </summary>
    public enum DirectMessageStatus
    {
        /// <summary>
        /// Event is invoked/fired when player recieves a direct message (DM) from another Vivox user who is logged in
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IDirectedTextMessage.html">VivoxUnity.IDirectedTextMessage</see>
        /// </para>
        /// </summary>
        DirectMessageSent,
        /// <summary>
        /// Event is invoked/fired when player sends a direct message (DM) to another Vivox user who is logged in
        /// <para>
        /// Method must have <b>0</b> parameters
        /// </para>
        /// </summary>
        DirectMessageRecieved,
        /// <summary>
        /// Event is invoked/fired when player sends a direct message (DM) to another Vivox user who is <b>not logged in</b>. 
        /// <para>
        /// Vivox treats the message as failed and it is up to the developer to implement retries (sending the message again) or 
        /// storing the failed message on the player's computer in a SQLite Database, PlayerPrefs, or in a txt/json file. 
        /// You can also upload to the cloud using Unity's Cloud Save, AWS S3, or Database of your choice.
        /// </para>
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IDirectedTextMessage.html">VivoxUnity.IFailedDirectedTextMessage</see>
        /// </para>
        /// </summary>
        DirectMessageFailed
    }



    /// <summary>
    /// The type of <b>Vivox User Event</b> that you want this method to be subscribed to. Works for <see cref="UserEventsAttribute"/> and <see cref="UserEventsAsyncAttribute"/>.
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// Event is invoked/fired when a player joins a Vivox Channel
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IParticipant.html">VivoxUnity.IParticipant</see>
        /// </para>
        /// </summary>
        UserJoinedChannel,
        /// <summary>
        /// Event is invoked/fired when a player leaves a Vivox Channel
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IParticipant.html">VivoxUnity.IParticipant</see>
        /// </para>
        /// </summary>
        UserLeftChannel,
        /// <summary>
        /// Event is invoked/fired when a players values get updated in a Vivox Channel such as being muted/unmuted
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IParticipant.html">VivoxUnity.IParticipant</see>
        /// </para>
        /// </summary>
        UserValuesUpdated,
        /// <summary>
        /// Event is invoked/fired when a player gets muted in a Vivox Channel
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IParticipant.html">VivoxUnity.IParticipant</see>
        /// </para>
        /// </summary>
        UserMuted,
        /// <summary>
        /// Event is invoked/fired when a player gets unmuted in a Vivox Channel
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IParticipant.html">VivoxUnity.IParticipant</see>
        /// </para>
        /// </summary>
        UserUnmuted,
        /// <summary>
        /// Event is invoked/fired when a player gets cross muted in a Vivox Channel
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.AccountId.html">VivoxUnity.AccountId</see>
        /// </para>
        /// </summary>
        UserCrossMuted,
        /// <summary>
        /// Event is invoked/fired when a player gets cross unmuted in a Vivox Channel
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.AccountId.html">VivoxUnity.AccountId</see>
        /// </para>
        /// </summary>
        UserCrossUnmuted,
        /// <summary>
        /// Event is invoked/fired when a player is speaking in a Vivox Channel
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IParticipant.html">VivoxUnity.IParticipant</see>
        /// </para>
        /// </summary>
        UserSpeaking,
        /// <summary>
        /// Event is invoked/fired when a player stops speaking in a Vivox Channel
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IParticipant.html">VivoxUnity.IParticipant</see>
        /// </para>
        /// </summary>
        UserNotSpeaking,
        /// <summary>
        /// Event is invoked/fired when the local player mutes themselves in a Vivox Channel
        /// <para>
        /// Method must have <b>0</b> parameters
        /// </para>
        /// </summary>
        LocalUserMuted,
        /// <summary>
        /// Event is invoked/fired when the local player unmutes themselves in a Vivox Channel
        /// <para>
        /// Method must have <b>0</b> parameters
        /// </para>
        /// </summary>
        LocalUserUnmuted,
    }


    /// <summary>
    /// The type of <b>Vivox Audio Device Event</b> that you want this method to be subscribed to. Works for <see cref="AudioDeviceEventAttribute"/> and <see cref="AudioDeviceEventAsyncAttribute"/>.
    /// </summary>
    public enum AudioDeviceStatus
    {
        /// <summary>
        /// Event is invoked/fired when Vivox detects a new Audio Input Device (Microphone) is connected to your pc/console/device
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IAudioDevice.html">VivoxUnity.IAudioDevice</see>
        /// </para>
        /// </summary>
        AudioInputDeviceAdded,
        /// <summary>
        /// Event is invoked/fired when Vivox detects a new Audio Input Device (Microphone) is disconnected from your pc/console/device
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IAudioDevice.html">VivoxUnity.IAudioDevice</see>
        /// </para>
        /// </summary>
        AudioInputDeviceRemoved,
        /// <summary>
        /// Event is invoked/fired when Vivox detects a new Audio Input Device (Microphone) is updated on your pc/console/device
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IAudioDevice.html">VivoxUnity.IAudioDevice</see>
        /// </para>
        /// </summary>
        AudioInputDeviceUpdated,
        /// <summary>
        /// Event is invoked/fired when Vivox detects a new Audio Output Device (Speaker/Headphones) is connected to your pc/console/device
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IAudioDevice.html">VivoxUnity.IAudioDevice</see>
        /// </para>
        /// </summary>
        AudioOutputDeviceAdded,
        /// <summary>
        /// Event is invoked/fired when Vivox detects a new Audio Output Device (Speaker/Headphones) is disconnected from your pc/console/device
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IAudioDevice.html">VivoxUnity.IAudioDevice</see>
        /// </para>
        /// </summary>
        AudioOutputDeviceRemoved,
        /// <summary>
        /// Event is invoked/fired when Vivox detects a new Audio Output Device (Speaker/Headphones) is updated on your pc/console/device
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.IAudioDevice.html">VivoxUnity.IAudioDevice</see>
        /// </para>
        /// </summary>
        AudioOutputDeviceUpdated,
    }


    /// <summary>
    /// The type of <b>Vivox Text-to-Speech Event</b> that you want this method to be subscribed to. Works for <see cref="TextToSpeechEventAttribute"/> and <see cref="TextToSpeechEventAsyncAttribute"/>.
    /// </summary>
    public enum TextToSpeechStatus
    {
        /// <summary>
        /// Event is invoked/fired when a Text-To-Speech message is added to the queue and is spoken/played
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.ITTSMessageQueueEventArgs.html">VivoxUnity.ITTSMessageQueueEventArgs</see>
        /// </para>
        /// </summary>
        TTSMessageAdded,
        /// <summary>
        /// Event is invoked/fired when a Text-To-Speech message is removed from the queue and is disposed of or canceled
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.ITTSMessageQueueEventArgs.html">VivoxUnity.ITTSMessageQueueEventArgs</see>
        /// </para>
        /// </summary>
        TTSMessageRemoved,
        /// <summary>
        /// Event is invoked/fired when a Text-To-Speech message is removed from queue and begins to play
        /// <para>
        /// Method must contain only <b>1</b> parameter of type <see href="https://docs.unity3d.com/Packages/com.unity.services.vivox@15.1/api/VivoxUnity.ITTSMessageQueueEventArgs.html">VivoxUnity.ITTSMessageQueueEventArgs</see>
        /// </para>
        /// </summary>
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
