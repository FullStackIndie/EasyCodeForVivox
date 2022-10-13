using System;
using System.Collections.Generic;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public static class EasySession
    {
        public static Uri APIEndpoint { get; set; }
        public static string Domain { get; set; }
        public static string Issuer { get; set; }
        public static string SecretKey { get; set; }

        public static VivoxUnity.Client Client { get; set; } = new Client();

        public static Dictionary<string, ILoginSession> LoginSessions = new Dictionary<string, ILoginSession>();
        public static Dictionary<string, IChannelSession> ChannelSessions = new Dictionary<string, IChannelSession>();

        public static bool IsClientInitialized = false;

        public static bool UseDynamicEvents { get; set; }

        private static int uniqueCounter = 1;

        // todo consider using epoch as unique counter in Vivox Access Token
        public static int UniqueCounter
        {
            get { return uniqueCounter++; }
        }

    }
}

