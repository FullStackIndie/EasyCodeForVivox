using EasyCodeForVivox;
using UnityEngine;
using VivoxUnity;

public class NormalEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EasyEvents.LoggingIn += PlayerLoggingIn;
    }
    private void OnApplicationQuit()
    {
        EasyEvents.LoggingIn -= PlayerLoggingIn;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerLoggingIn(ILoginSession loginSession)
    {
        Debug.Log($"Invoking Normal Event from {nameof(PlayerLoggingIn)}");
    }
}
