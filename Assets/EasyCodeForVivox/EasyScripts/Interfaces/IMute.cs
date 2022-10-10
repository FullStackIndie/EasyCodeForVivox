using System;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface IMute
    {
        void LocalToggleMuteRemoteUser(string userName, IChannelSession channelSession);
        void MuteAllUsers(IChannelSession channelSession);
        void UnmuteAllUsers(IChannelSession channelSession);
        void LocalMuteSelf(VivoxUnity.Client client);
        void LocalUnmuteSelf(VivoxUnity.Client client);
        void CrossMuteUser(ILoginSession loginSession, bool mute);
        void CrossMuteUsers(ILoginSession loginSessionToMute, bool mute);
        void ClearAllCurrentCrossMutedAccounts(ILoginSession loginSession);
        void CrossMuteResult(IAsyncResult ar);
    }
}

