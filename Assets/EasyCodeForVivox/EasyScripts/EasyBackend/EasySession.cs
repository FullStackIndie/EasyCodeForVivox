using System.Collections.Generic;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasySession
    {
        public VivoxUnity.Client Client { get; set; } = new Client();

        public Dictionary<string, ILoginSession> LoginSessions = new Dictionary<string, ILoginSession>();
        public Dictionary<string, IChannelSession> ChannelSessions = new Dictionary<string, IChannelSession>();

        public bool IsClientInitialized = false;

        public bool UseDynamicEvents { get; set; }

        private int uniqueCounter = 1;

        // todo consider using epoch as unique counter in Vivox Access Token
        public int UniqueCounter
        {
            get { return uniqueCounter++; }
        }
    }
}
