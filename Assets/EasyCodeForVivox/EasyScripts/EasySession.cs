using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;

public class EasySession : MonoBehaviour
{
    public static Uri APIEndpoint { get; set; } = new Uri("https://mt1s.www.vivox.com/api2");
    public static string Domain { get; set; } = "mt1s.vivox.com";
    public static string Issuer { get; set; } = "johnmu0739-vi31-dev";
    public static string SecretKey { get; set; } = "luck408";

    //public static VATAuthenticate.VATAuthenticate authenticate = new VATAuthenticate.VATAuthenticate();
    public static VivoxUnity.Client mainClient { get; set; } = new Client();

    public static ILoginSession mainLoginSession;
    public static Dictionary<string, IChannelSession> mainChannelSessions = new Dictionary<string, IChannelSession>();

    public static bool isClientInitialized = false;

    private static int uniqueCounter = 1;

    public static int UniqueCounter
    {
        get { return uniqueCounter++; }
    }


}
