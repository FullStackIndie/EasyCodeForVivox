using UnityEngine;
using VivoxUnity;

public class EasyPlayerSettingsSO : ScriptableObject
{
    TransmissionMode InitialTransmissionMode { get; set; } = TransmissionMode.All;
    KeyCode PushToTalkKey { get; set; } = KeyCode.T;

}
