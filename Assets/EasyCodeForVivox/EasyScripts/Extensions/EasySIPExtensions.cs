using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivoxUnity;

namespace EasyCodeForVivox.Extensions
{
    public static class EasySIPExtensions
    {       /// <summary>
            /// Checks if this IchannelSession is the current logged in user
            /// </summary>
            /// <param name="channelSession"></param>
            /// <returns></returns>
        public static bool IsSelf(this IChannelSession channelSession)
        {
            if (channelSession.Participants.ContainsKey(GetSIP(channelSession)))
            {
                return true;
            }
            return false;
        }



        /// <summary>
        /// Gets the valid Vivox SIP address from this ILoginSession
        /// </summary>
        /// <param name="loginSession"></param>
        /// <returns></returns>
        public static string GetSIP(this ILoginSession loginSession)
        {
            var user = EasySIP.GetUserSIP(loginSession);
            return user;
        }

        /// <summary>
        /// Gets the valid Vivox SIP address from this IChannelSession
        /// </summary>
        /// <param name="loginSession"></param>
        /// <returns></returns>
        public static string GetSIP(this IChannelSession channelSession)
        {
            var participants = channelSession.Participants;
            var user = EasySIP.GetUserSIP(channelSession.Parent.Key.Issuer, channelSession.Parent.Key.DisplayName, channelSession.Parent.Key.Domain);
            if (participants.ContainsKey(user))
            {
                return user;
            }
            return "Error : Couldnt find user";
        }

        /// <summary>
        /// Gets the valid Vivox SIP address from this IParticipant
        /// </summary>
        /// <param name="loginSession"></param>
        /// <returns></returns>
        public static string GetSIP(this IParticipant participant)
        {
            var SIP = EasySIP.GetUserSIP(participant);
            return SIP;
        }




    }
}
