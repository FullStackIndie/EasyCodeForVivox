using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasySIP : MonoBehaviour
    {
        /// <summary>
        /// Gets valid Vivox SIP address
        /// </summary>
        /// <returns></returns>
        public static string GetUserSIP(ILoginSession loginSession)
        {
            string result = $"sip:.{loginSession.Key.Issuer}.{loginSession.Key.Name}.@{loginSession.Key.Domain}";
            return result;
        }

        /// <summary>
        /// Gets valid Vivox SIP address
        /// </summary>
        /// <returns></returns>
        public static string GetUserSIP(IParticipant participant)
        {
            string result = $"sip:.{participant.ParentChannelSession.Parent.LoginSessionId.Issuer}.{participant.ParentChannelSession.Parent.LoginSessionId.DisplayName}.@{participant.ParentChannelSession.Parent.LoginSessionId.Domain}";
            return result;
        }

        /// <summary>
        /// Gets valid Vivox SIP address
        /// </summary>
        /// <returns></returns>
        public static string GetUserSIP(string issuer, string userName, string domain)
        {
            string result = $"sip:.{issuer}.{userName}.@{domain}";
            return result;
        }

        /// <summary>
        /// Gets valid Vivox Channel SIP address
        /// </summary>
        /// <returns></returns>
        public static string GetChannelSip(IChannelSession channelSession,
            Channel3DProperties channel3DProperties = default)
        {
            string channelToken = "Channel Token Invalid";
            string issuer = channelSession.Channel.Issuer;
            string channelName = channelSession.Channel.Name;
            string domain = channelSession.Channel.Domain;
            ChannelType channelType = channelSession.Channel.Type;
            switch (channelType)
            {
                case ChannelType.Echo:
                    channelToken = $"sip:confctl-e-{issuer}.{channelName}@{domain}";
                    break;
                case ChannelType.NonPositional:
                    channelToken = $"sip:confctl-g-{issuer}.{channelName}@{domain}";
                    break;
                case ChannelType.Positional:
                    if (channel3DProperties == null)
                    {
                        channel3DProperties = new Channel3DProperties();
                    }
                    channelToken = $"sip:confctl-d-{issuer}.{channelName}{channel3DProperties}@{domain}";
                    break;
            }
            return channelToken;
        }

        /// <summary>
        /// Gets valid Vivox Channel SIP address
        /// </summary>
        /// <returns></returns>
        public static string GetChannelSIP(ChannelType channelType, string issuer, string channelName, string domain, string region, string hash,
            Channel3DProperties channel3DProperties = default)
        {
            string channelToken = "Channel Token Invalid";
            switch (channelType)
            {
                case ChannelType.Echo:
                    channelToken = $"sip:confctl-e-{issuer}.{region}.{hash}-{channelName}@{domain}";
                    break;
                case ChannelType.NonPositional:
                    channelToken = $"sip:confctl-g-{issuer}.{region}.{hash}-{channelName}@{domain}";
                    break;
                case ChannelType.Positional:
                    if (channel3DProperties == null)
                    {
                        channel3DProperties = new Channel3DProperties();
                    }
                    channelToken = $"sip:confctl-d-{issuer}.{region}.{hash}-{channelName}{channel3DProperties}@{domain}";
                    break;
            }
            return channelToken;
        }

        /// <summary>
        /// Gets valid Vivox Channel SIP address
        /// </summary>
        /// <returns></returns>
        public static string GetChannelSIP(ChannelType channelType, string issuer, string channelName, string domain,
            Channel3DProperties channel3DProperties = default)
        {
            string channelToken = "Channel Token Invalid";
            switch (channelType)
            {
                case ChannelType.Echo:
                    channelToken = $"sip:confctl-e-{issuer}.{channelName}@{domain}";
                    break;
                case ChannelType.NonPositional:
                    channelToken = $"sip:confctl-g-{issuer}.{channelName}@{domain}";
                    break;
                case ChannelType.Positional:
                    if (channel3DProperties == null)
                    {
                        channel3DProperties = new Channel3DProperties();
                    }
                    channelToken = $"sip:confctl-d-{issuer}.{channelName}{channel3DProperties}@{domain}";
                    break;
            }
            return channelToken;
        }
    }
}