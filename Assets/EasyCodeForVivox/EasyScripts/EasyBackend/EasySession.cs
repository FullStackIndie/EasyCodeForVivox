using System;
using System.Collections.Generic;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasySession
    {
        // todo Implement Unity Remote Config to store sensitive information in the cloud. It's free
        // if users dont want to use Remote Config then advise users to use environment variables instead of hardcoding secrets/api keys
        // inside of the Unity Editor because hackers can decompile there game and steal the secrets/keys
        // expensive but working Unity decompiler https://devxdevelopment.com/

        public Uri APIEndpoint { get; set; }
        public string Domain { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }

        public VivoxUnity.Client Client { get; set; } = new Client();

        public Dictionary<string, ILoginSession> LoginSessions = new Dictionary<string, ILoginSession>();
        public Dictionary<string, IChannelSession> ChannelSessions = new Dictionary<string, IChannelSession>();


        public bool IsClientInitialized { get; set; }

        private int uniqueCounter = 1;
        // todo consider using epoch as unique counter in Vivox Access Token
        public int UniqueCounter
        {
            get { return uniqueCounter++; }
        }
    }
}
