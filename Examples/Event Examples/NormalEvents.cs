using System;
using UnityEngine;
using VivoxUnity;

public class NormalEvents : MonoBehaviour
{
    public event Action<ILoginSession> LoggingIn;

    void Start()
    {
        LoggingIn += PlayerLoggingIn;
    }
    private void OnApplicationQuit()
    {
        LoggingIn -= PlayerLoggingIn;
    }

    public void PlayerLoggingIn(ILoginSession loginSession)
    {
        Debug.Log($"Invoking Normal Event from {nameof(PlayerLoggingIn)}");
    }
}
