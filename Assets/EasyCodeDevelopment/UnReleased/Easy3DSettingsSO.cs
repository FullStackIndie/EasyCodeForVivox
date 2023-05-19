using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class Easy3DSettingsSO : ScriptableObject
    {
        public int AudibleDistance { get; set; } = 32;
        public int ConversationalDistance { get; set; } = 1;
        public float AudioFadeIntensityByDistance { get; set; } = 1.0f;
        public AudioFadeModel AudioFadeModel { get; set; } = AudioFadeModel.InverseByDistance;
    }
}
