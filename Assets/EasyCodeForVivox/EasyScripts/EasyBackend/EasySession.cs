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

        public static ILoginSession mainLoginSession;
        public static Dictionary<string, IChannelSession> mainChannelSessions = new Dictionary<string, IChannelSession>();

        public static bool isClientInitialized = false;

        private static int uniqueCounter = 1;

        public static int UniqueCounter
        {
            get { return uniqueCounter++; }
        }

    }
}

