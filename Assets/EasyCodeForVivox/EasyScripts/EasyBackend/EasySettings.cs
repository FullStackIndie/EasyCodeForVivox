using UnityEngine;


[CreateAssetMenu(fileName = "EasySettings", menuName = "EasyCodeForVivox/Create EasySettings", order = 1)]
public class EasySettings : ScriptableObject
{
    public string APIEndpoint;
    public string Domain;
    public string Issuer;
    public string SecretKey;

    public bool UseDynamicEvents;
    public bool LogAssemblySearches;
    public bool LogAllDynamicMethods;
    public bool LogAllAudioDevices;
}

