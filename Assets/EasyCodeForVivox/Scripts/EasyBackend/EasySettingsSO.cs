using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "EasySettings", menuName = "EasyCodeForVivox/Create EasySettings", order = 1)]
public class EasySettingsSO : ScriptableObject
{
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

