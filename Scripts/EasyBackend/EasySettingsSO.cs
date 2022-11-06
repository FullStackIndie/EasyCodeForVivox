using EasyCodeForVivox;
using UnityEngine;
using VivoxUnity;

[CreateAssetMenu(fileName = "EasySettings", menuName = "EasyCodeForVivox/Create EasySettings", order = 1)]
public class EasySettingsSO : ScriptableObject
{
    [Tooltip("Choose which gender Vivox will use when using Text-To-Speech")]
    public VoiceGender VoiceGender = VoiceGender.female;
    [Tooltip("Hz is how many times per second you want to update Participant Properties")]
    public ParticipantPropertyUpdateFrequency VivoxParticipantPropertyUpdateFrequency = ParticipantPropertyUpdateFrequency.StateChange;
    public bool LogEasyManager;
    public bool LogEasyManagerEventCallbacks;
    public bool UseDynamicEvents;
    public bool OnlySearchAssemblyCSharp;
    public bool LogAssemblySearches;
    public bool LogAllDynamicMethods;
    public bool LogAllAudioDevices;
    public bool LogVoiceActivityDetection;
    public bool LogEasyNetCode;
    public bool LogNetCodeForGameObjects;

}

