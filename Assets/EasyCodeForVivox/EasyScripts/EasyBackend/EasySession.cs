using System;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasySession : MonoBehaviour
    {
        public static Uri APIEndpoint { get; set; }
        public static string Domain { get; set; } 
        public static string Issuer { get; set; }
        public static string SecretKey { get; set; } 

        public static VivoxUnity.Client mainClient { get; set; } = new Client();

        public static ILoginSession MainLoginSession;
        public static Dictionary<string, IChannelSession> MainChannelSessions = new Dictionary<string, IChannelSession>();

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

