using EasyCodeForVivox;
using UnityEngine;
using Zenject;

namespace Assets.EasyCodeDevelopment
{
    public class VivoxAudioChannel : MonoBehaviour
    {
        EasyAudioChannel _audioChannel;

        [Inject]
        public void Initialize(EasyAudioChannel audioChannel)
        {
            _audioChannel = audioChannel;
        }

        public void ToggleAudioInChannel()
        {
            _audioChannel.ToggleAudioInChannel(EasySession.ChannelSessions["3d"], true);
        }

    }
}
