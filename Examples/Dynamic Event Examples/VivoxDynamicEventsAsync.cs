//using Unity.Services.Authentication;
//using Unity.Services.CloudSave;
//using Unity.Services.Lobbies;
//using Unity.Services.Lobbies.Models;
using UnityEngine;

public class VivoxDynamicEventsAsync : MonoBehaviour
{
    //public async Task GetJoinedLobbies()
    //{
    //    try
    //    {
    //        var lobbyIds = await LobbyService.Instance.GetJoinedLobbiesAsync();
    //    }
    //    catch (LobbyServiceException e)
    //    {
    //        Debug.Log(e);
    //    }
    //}

    //public async Task RemovePlayerFromLobby()
    //{
    //    try
    //    {
    //        //Ensure you sign-in before calling Authentication Instance
    //        //See IAuthenticationService interface
    //        string playerId = AuthenticationService.Instance.PlayerId;
    //        await LobbyService.Instance.RemovePlayerAsync("lobbyId", playerId);
    //    }
    //    catch (LobbyServiceException e)
    //    {
    //        Debug.Log(e);
    //    }
    //}

    //public async Task LoadPlayerData()
    //{
    //    try
    //    {
    //        Dictionary<string, string> savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "key" });

    //        Debug.Log("Done: " + savedData["key"]);
    //    }
    //    catch (LobbyServiceException e)
    //    {
    //        Debug.Log(e);
    //    }
    //}

    //public async Task SavePlayerData()
    //{
    //    try
    //    {
    //        var data = new Dictionary<string, object> { { "key", "someValue" } };
    //        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.Log(e);
    //    }
    //}


    //public async Task UpdatePlayerData()
    //{
    //    try
    //    {
    //        UpdatePlayerOptions options = new UpdatePlayerOptions();

    //        options.Data = new Dictionary<string, PlayerDataObject>()
    //        {
    //            {
    //                "existing data key", new PlayerDataObject(
    //                    visibility: PlayerDataObject.VisibilityOptions.Private,
    //                    value: "updated data value")
    //            },
    //            {
    //                "new data key", new PlayerDataObject(
    //                    visibility: PlayerDataObject.VisibilityOptions.Public,
    //                    value: "new data value")
    //            }
    //        };

    //        //Ensure you sign-in before calling Authentication Instance
    //        //See IAuthenticationService interface
    //        string playerId = AuthenticationService.Instance.PlayerId;

    //        var lobby = await LobbyService.Instance.UpdatePlayerAsync("lobbyId", playerId, options);

    //    }
    //    catch (LobbyServiceException e)
    //    {
    //        Debug.Log(e);
    //    }
    //}

    //[LoginEventAsync(LoginStatus.LoggingIn)]
    //private async void OnPlayerLoggingInAsync(ILoginSession loginSession)
    //{
    //    Debug.Log($"Logging In : {loginSession.LoginSessionId.DisplayName}");
    //    await GetJoinedLobbies();
    //}

    //[LoginEventAsync(LoginStatus.LoggedIn)]
    //private async void OnPlayerLoggedInAsync(ILoginSession loginSession)
    //{
    //    Debug.Log($"Logged in : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
    //    await LoadPlayerData();
    //}

    //[LoginEventAsync(LoginStatus.LoggingOut)]
    //private async void OnPlayerLoggingOutAsync(ILoginSession loginSession)
    //{
    //    Debug.Log($"Logging out : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
    //    await RemovePlayerFromLobby();
    //}

    //[LoginEventAsync(LoginStatus.LoggedOut)]
    //private async void OnPlayerLoggedOutAsync(ILoginSession loginSession)
    //{
    //    Debug.Log($"Logged out : {loginSession.LoginSessionId.DisplayName}  : Presence = {loginSession.Presence.Status}");
    //    await SavePlayerData();
    //}

    //[LoginEventAsync(LoginStatus.LoginAdded)]
    //private async void OnLoginSessionAddedAsync(AccountId accountId)
    //{
    //    Debug.Log($"A new Login was added for player {accountId.DisplayName}");
    //    await GetJoinedLobbies();
    //}

    //[LoginEventAsync(LoginStatus.LoginRemoved)]
    //private async void OnLoginSessionRemovedAsync(AccountId accountId)
    //{
    //    Debug.Log($"A new Login was removed for player {accountId.DisplayName}");
    //    await RemovePlayerFromLobby();
    //}

    //[LoginEventAsync(LoginStatus.LoginValuesUpdated)]
    //private async void OnLoginSessionValuesUpdatedAsync(ILoginSession loginSession)
    //{
    //    Debug.Log($"LoginSession has been updated for player {loginSession.LoginSessionId.DisplayName}");
    //    await UpdatePlayerData();
    //}



    //[ChannelEventAsync(ChannelStatus.ChannelConnecting)]
    //private async void OnChannelConnectingAsync(IChannelSession channelSession)
    //{
    //    Debug.Log($"{channelSession.Channel.Name} Is Connecting");
    //    await LoadPlayerData();
    //}

    //[ChannelEventAsync(ChannelStatus.ChannelConnected)]
    //private async void OnChannelConnectedAsync(IChannelSession channelSession)
    //{
    //    Debug.Log($"{channelSession.Channel.Name} Has Connected : Channel Type == {channelSession.Channel.Type}");
    //    await GetJoinedLobbies();
    //}

    //[ChannelEventAsync(ChannelStatus.ChannelDisconnecting)]
    //private async void OnChannelDisconnectingAsync(IChannelSession channelSession)
    //{
    //    Debug.Log($"{channelSession.Channel.Name} Is Disconnecting");
    //    await SavePlayerData();
    //}

    //[ChannelEventAsync(ChannelStatus.ChannelDisconnected)]
    //private async void OnChannelDisconnectedAsync(IChannelSession channelSession)
    //{
    //    Debug.Log($"{channelSession.Channel.Name} Has Disconnected");
    //    await RemovePlayerFromLobby();
    //}



    //[AudioChannelEventAsync(AudioChannelStatus.AudioChannelConnecting)]
    //private async void OnAudioChannelConnectingAsync(IChannelSession channelSession)
    //{
    //    Debug.Log($"{channelSession.Channel.Name} Is Connecting");
    //    await LoadPlayerData();
    //}

    //[AudioChannelEventAsync(AudioChannelStatus.AudioChannelConnected)]
    //private async void OnAudioChannelConnectedAsync(IChannelSession channelSession)
    //{
    //    Debug.Log($"{channelSession.Channel.Name} Has Connected : Channel Type == {channelSession.Channel.Type}");
    //    await GetJoinedLobbies();
    //}

    //[AudioChannelEventAsync(AudioChannelStatus.AudioChannelDisconnecting)]
    //private async void OnAudioChannelDisconnectingAsync(IChannelSession channelSession)
    //{
    //    Debug.Log($"{channelSession.Channel.Name} Is Disconnecting");
    //    await SavePlayerData();
    //}

    //[AudioChannelEventAsync(AudioChannelStatus.AudioChannelDisconnected)]
    //private async void OnAudioChannelDisconnectedAsync(IChannelSession channelSession)
    //{
    //    Debug.Log($"{channelSession.Channel.Name} Has Disconnected");
    //    await RemovePlayerFromLobby();
    //}




    //[TextChannelEventAsync(TextChannelStatus.TextChannelConnecting)]
    //private async void OnTextChannelConnectingAsync(IChannelSession channelSession)
    //{
    //    Debug.Log($"{channelSession.Channel.Name} Is Connecting");
    //    await LoadPlayerData();
    //}

    //[TextChannelEventAsync(TextChannelStatus.TextChannelConnected)]
    //private async void OnTextChannelConnectedAsync(IChannelSession channelSession)
    //{
    //    Debug.Log($"{channelSession.Channel.Name} Has Connected : Channel Type == {channelSession.Channel.Type}");
    //    await GetJoinedLobbies();
    //}
    //[TextChannelEventAsync(TextChannelStatus.TextChannelDisconnecting)]
    //private async void OnTextChannelDisconnectingAsync(IChannelSession channelSession)
    //{
    //    Debug.Log($"{channelSession.Channel.Name} Is Disconnecting");
    //    await SavePlayerData();
    //}

    //[TextChannelEventAsync(TextChannelStatus.TextChannelDisconnected)]
    //private async void OnTextChannelDisconnectedAsync(IChannelSession channelSession)
    //{
    //    Debug.Log($"{channelSession.Channel.Name} Has Disconnected");
    //    await RemovePlayerFromLobby();
    //}



    //[ChannelMessageEventAsync(ChannelMessageStatus.ChannelMessageRecieved)]
    //private async void OnChannelMessageRecievedAsync(IChannelTextMessage textMessage)
    //{
    //    Debug.Log($"From {textMessage.Sender.DisplayName} : {textMessage.ReceivedTime} : {textMessage.Message}");
    //    await SavePlayerData();
    //}

    //[ChannelMessageEventAsync(ChannelMessageStatus.EventMessageRecieved)]
    //private async void OnEventMessageRecievedAsync(IChannelTextMessage textMessage)
    //{
    //    Debug.Log($"Event Message From {textMessage.Sender.DisplayName} : {textMessage.ReceivedTime} : {textMessage.ApplicationStanzaNamespace} : {textMessage.ApplicationStanzaBody} : {textMessage.Message}");
    //    await SavePlayerData();
    //}

    //[DirectMessageEventAsync(DirectMessageStatus.DirectMessageRecieved)]
    //private async void OnDirectMessageRecievedAsync(IDirectedTextMessage directedTextMessage)
    //{
    //    Debug.Log($"Recived Message From : {directedTextMessage.Sender.DisplayName} : {directedTextMessage.ReceivedTime} : {directedTextMessage.Message}");
    //    await SavePlayerData();
    //}

    //[DirectMessageEventAsync(DirectMessageStatus.DirectMessageFailed)]
    //private async void OnDirectMessageFailedAsync(IFailedDirectedTextMessage failedMessage)
    //{
    //    Debug.Log($"Failed To Send Message From : {failedMessage.Sender}");
    //    await SavePlayerData();
    //}







    //[UserEventsAsync(UserStatus.LocalUserMuted)]
    //private async void OnLocalUserMutedAsync()
    //{
    //    Debug.Log("Local User is Muted");
    //    await SavePlayerData();
    //}

    //[UserEventsAsync(UserStatus.LocalUserUnmuted)]
    //private async void OnLocalUserUnmutedAsync()
    //{
    //    Debug.Log("Local User is Unmuted");
    //    await SavePlayerData();
    //}

    //[UserEventsAsync(UserStatus.UserCrossMuted)]
    //private async void OnCrossMutedAsync(AccountId accountId)
    //{
    //    Debug.Log($"Player {accountId.DisplayName} has been Cross Muted");
    //    await SavePlayerData();
    //}

    //[UserEventsAsync(UserStatus.UserCrossUnmuted)]
    //private async void OnCrossUnmutedAsync(AccountId accountId)
    //{
    //    Debug.Log($"Player {accountId.DisplayName} has been Cross Unmuted");
    //    await SavePlayerData();
    //}

    //[UserEventsAsync(UserStatus.UserMuted)]
    //private async void OnUserMutedAsync(IParticipant participant)
    //{
    //    Debug.Log($"{participant.Account.DisplayName} Is Muted : (Muted For All : {participant.IsMutedForAll})");
    //    await SavePlayerData();
    //}

    //[UserEventsAsync(UserStatus.UserUnmuted)]
    //private async void OnUserUnmutedAsync(IParticipant participant)
    //{
    //    Debug.Log($"{participant.Account.DisplayName} Is Unmuted : (Muted For All : {participant.IsMutedForAll})");
    //    await SavePlayerData();
    //}

    //[UserEventsAsync(UserStatus.UserSpeaking)]
    //private async void OnUserSpeakingAsync(IParticipant participant)
    //{
    //    Debug.Log($"{participant.Account.DisplayName} Is Speaking : Audio Energy {participant.AudioEnergy}");
    //    await SavePlayerData();
    //}

    //[UserEventsAsync(UserStatus.UserNotSpeaking)]
    //private async void OnUserNotSpeakingAsync(IParticipant participant)
    //{
    //    Debug.Log($"{participant.Account.DisplayName} Is Not Speaking");
    //    await SavePlayerData();
    //}




    //[UserEventsAsync(UserStatus.UserJoinedChannel)]
    //private async void OnUserJoinedChannelAsync(IParticipant participant)
    //{
    //    Debug.Log($"{participant.Account.DisplayName} Has Joined The Channel");
    //    await LoadPlayerData();
    //}

    //[UserEventsAsync(UserStatus.UserLeftChannel)]
    //private async void OnUserLeftChannelAsync(IParticipant participant)
    //{
    //    Debug.Log($"{participant.Account.DisplayName} Has Left The Channel");
    //    await RemovePlayerFromLobby();
    //}

    //[UserEventsAsync(UserStatus.UserValuesUpdated)]
    //private async void OnUserValuesUpdatedAsync(IParticipant participant)
    //{
    //    Debug.Log($"{participant.Account.DisplayName} Has updated itself in the channel");
    //    await SavePlayerData();
    //}




    //[AudioDeviceEventAsync(AudioDeviceStatus.AudioInputDeviceAdded)]
    //private async void OnAudioInputDeviceAddedAsync(IAudioDevice audioDevice)
    //{
    //    Debug.Log($"Audio Input device has been added {audioDevice?.Name}");
    //    await SavePlayerData();
    //}

    //[AudioDeviceEventAsync(AudioDeviceStatus.AudioInputDeviceRemoved)]
    //private async void OnAudioInputDeviceRemovedAsync(IAudioDevice audioDevice)
    //{
    //    Debug.Log($"Audio Input device has been removed {audioDevice?.Name}");
    //    await SavePlayerData();
    //}

    //[AudioDeviceEventAsync(AudioDeviceStatus.AudioInputDeviceUpdated)]
    //private async void OnAudioInputDeviceUpdatedAsync(IAudioDevice audioDevice)
    //{
    //    Debug.Log($"Audio Input Device has been changed to {audioDevice?.Name}");
    //    await SavePlayerData();
    //}

    //[AudioDeviceEventAsync(AudioDeviceStatus.AudioOutputDeviceAdded)]
    //private async void OnAudioOutputDeviceAddedAsync(IAudioDevice audioDevice)
    //{
    //    Debug.Log($"Audio Output device has been added {audioDevice?.Name}");
    //    await SavePlayerData();
    //}

    //[AudioDeviceEventAsync(AudioDeviceStatus.AudioOutputDeviceRemoved)]
    //private async void OnAudioOutputDeviceRemovedAsync(IAudioDevice audioDevice)
    //{
    //    Debug.Log($"Audio Output device has been removed {audioDevice?.Name}");
    //    await SavePlayerData();
    //}

    //[AudioDeviceEventAsync(AudioDeviceStatus.AudioOutputDeviceUpdated)]
    //private async void OnAudioOutputDeviceUpdatedAsync(IAudioDevice audioDevice)
    //{
    //    Debug.Log($"Audio Output Device has been changed to {audioDevice?.Name}");
    //    await SavePlayerData();
    //}



    //[TextToSpeechEventAsync(TextToSpeechStatus.TTSMessageAdded)]
    //private async void OnTTSMessageAddedAsync(ITTSMessageQueueEventArgs ttsArgs)
    //{
    //    Debug.Log($"TTS Message Has Been Added : {ttsArgs.Message.Text}");
    //    await SavePlayerData();
    //}

    //[TextToSpeechEventAsync(TextToSpeechStatus.TTSMessageRemoved)]
    //private async void OnTTSMessageRemovedAsync(ITTSMessageQueueEventArgs ttsArgs)
    //{
    //    Debug.Log($"TTS Message Has Been Removed : {ttsArgs.Message.Text}");
    //    await SavePlayerData();
    //}

    //[TextToSpeechEventAsync(TextToSpeechStatus.TTSMessageUpdated)]
    //private async void OnTTSMessageUpdatedAsync(ITTSMessageQueueEventArgs ttsArgs)
    //{
    //    Debug.Log($"TTS Message Has Been Updated : {ttsArgs.Message.Text}");
    //    await SavePlayerData();
    //}



}
