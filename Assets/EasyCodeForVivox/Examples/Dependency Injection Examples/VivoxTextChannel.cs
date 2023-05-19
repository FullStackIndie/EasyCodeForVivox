using EasyCodeForVivox;
using UnityEngine;
using Zenject;

namespace EasyCodeForVivox.Examples
{
    public class VivoxTextChannel : MonoBehaviour
    {
        EasyTextChannel _textChannel;

        [Inject]
        public void Initialize(EasyTextChannel textChannel)
        {
            _textChannel = textChannel;
        }

        public void ToggleTextInChannel()
        {
            _textChannel.ToggleTextInChannel(EasySession.ChannelSessions["chat"], true);
        }

    }
}
