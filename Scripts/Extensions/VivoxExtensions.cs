using System.Linq;
using System.Security.Cryptography;
using System.Text;
using VivoxUnity;

namespace EasyCodeForVivox.Extensions
{
    public static class VivoxExtensions
    {
        public static ChannelId GetChannelId(this ILoginSession loginSession, string channelName)
        {
            return loginSession.ChannelSessions.FirstOrDefault(c => c.Channel.Name == channelName).Channel;
        }


        public static string GetMD5Hash(this string valueToHash)
        {
            var bytes = Encoding.UTF8.GetBytes(valueToHash);
            var matchHash = new MD5CryptoServiceProvider().ComputeHash(bytes);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < matchHash.Length; i++)
            {
                stringBuilder.Append(matchHash[i].ToString("X2"));
            }
            return stringBuilder.ToString();
        }
    }
}
