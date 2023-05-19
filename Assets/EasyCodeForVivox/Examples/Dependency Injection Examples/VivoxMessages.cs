using EasyCodeForVivox;
using UnityEngine;
using Zenject;

namespace EasyCodeForVivox.Examples
{
    public class VivoxMessages : MonoBehaviour
    {
        EasyMessages _messages;

        [Inject]
        private void Initialize(EasyMessages messages)
        {
            _messages = messages;
        }

        public void SendChannelMessage()
        {
            _messages.SendChannelMessage(EasySession.ChannelSessions["chat"], "my message to everyone");
            _messages.SendChannelMessage(EasySession.ChannelSessions["chat"], "my message to everyone", header: "squad", body: "1");
        }

        public void SendDirectMessage()
        {
            _messages.SendDirectMessage(EasySession.LoginSessions["userName"], "myFriendsName", "my message to everyone");
            _messages.SendDirectMessage(EasySession.LoginSessions["userName"], "myFriendsName", "my message to everyone", header: "squad", body: "1");
        }
    }
}