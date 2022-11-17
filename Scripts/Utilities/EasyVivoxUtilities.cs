using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
 using UnityEngine.Android;
#elif UNITY_IOS
 using UnityEngine.iOS;
#endif


namespace EasyCodeForVivox.Utilities
{
    public static class EasyVivoxUtilities
    {

        public static void RequestAndroidMicPermission()
        {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
        }


        public static void RequestIOSMicrophoneAccess()
        {
            // todo update and research docs / have someone without IOS test it
            // Refer to Vivox Documentation on how to implement this method. Currently a work in progress.NOT SURE IF IT WORKS
            // make sure you change the info.plist refer to Vivox documentation for this to work
            // Make sure NSCameraUsageDescription and NSMicrophoneUsageDescription
            // are in the Info.plist.
#if UNITY_IOS
        Application.RequestUserAuthorization(UserAuthorization.Microphone);
#endif
        }

        public static bool FilterChannelAndUserName(string nameToFilter)
        {
            char[] allowedChars = new char[] { '0','1','2','3', '4', '5', '6', '7', '8', '9',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n','o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I','J', 'K', 'L', 'M', 'N', 'O', 'P','Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        '!', '(', ')', '+','-', '.', '=', '_', '~'};

            List<char> allowed = new List<char>(allowedChars);
            foreach (char c in nameToFilter)
            {
                if (!allowed.Contains(c))
                {
                    if (c == ' ')
                    {
                        Debug.Log($"Can't join channel, Channel name has space in it '{c}'");
                    }
                    else
                    {
                        Debug.Log($"Can't join channel, Channel name has invalid character '{c}'");
                    }
                    return false;
                }
            }
            return true;
        }


    }

}
