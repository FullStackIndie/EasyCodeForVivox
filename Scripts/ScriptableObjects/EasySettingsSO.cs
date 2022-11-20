using EasyCodeForVivox;
using System.Collections.Generic;
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
    [Tooltip("Allows you to use [Attributes] on methods to subscribe to Vivox Events")]
    public bool UseDynamicEvents;
    [Tooltip("Forces EasyCode to use only Assembelies specified in this list. If list is empty all assemblies will be searched")]
    public List<string> OnlySearchTheseAssemblies = new List<string>();
    public bool LogAssemblySearches;
    public bool LogAllFoundDynamicMethods;
    public bool LogAllAudioDevices;
    public bool LogVoiceActivityDetection;
    public bool LogEasyNetCode;
    public bool LogNetCodeForGameObjects;

}

