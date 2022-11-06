using System.Collections.Generic;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface IMute
    {
        void Subscribe(ILoginSession loginSession);
        void Unsubscribe(ILoginSession loginSession);

        void LocalToggleMuteRemoteUser(string userName, IChannelSession channelSession);
        void MuteAllUsers(IChannelSession channelSession);
        void UnmuteAllUsers(IChannelSession channelSession);

        void LocalMuteSelf(VivoxUnity.Client client);
        void LocalMuteSelf<T>(VivoxUnity.Client client, T value);

        void LocalUnmuteSelf(VivoxUnity.Client client);
        void LocalUnmuteSelf<T>(VivoxUnity.Client client, T value);

        void CrossMuteUser(string userName, string channelName, string userToMute, bool mute);

        void CrossMuteUsers(string loggedInUserName, string channelName, List<string> usersToMute, bool mute);

        void ClearAllCurrentCrossMutedAccounts(string loggedInUserName);
    }
}

