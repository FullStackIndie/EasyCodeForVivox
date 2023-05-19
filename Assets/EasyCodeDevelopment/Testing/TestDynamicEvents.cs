using EasyCodeForVivox;
using EasyCodeForVivox.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;

public class TestDynamicEvents : MonoBehaviour
{
    [LoginEvent(LoginStatus.LoggedIn)]
    public void Start()
    {
        
    }

    [LoginEvent(LoginStatus.LoggedIn)]
    public void Started(ILoginSession loginSession, IChannelSession channelSession)
    {
        
    }
}
