using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace EasyCodeForVivox
{
    public static class RuntimeEvents
    {
        public static List<MethodInfo> LoginMethods = new List<MethodInfo>();
        public static List<MethodInfo> ChannelMethods = new List<MethodInfo>();
        public static List<MethodInfo> VoiceChannelMethods = new List<MethodInfo>();
        public static List<MethodInfo> TextChannelMethods = new List<MethodInfo>();

        public static void RegisterEvents()
        {
            Task.Run(() =>
            {
                System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                foreach (Assembly assembly in assemblies)
                {
                    Type[] types = assembly.GetTypes();
                    RegisterLoginMethods(types);
                    RegisterChannelMethods(types);
                    RegisterVoiceChannelMethods(types);
                    RegisterTextChannelMethods(types);
                }

                LogRegisteredEventsCount();

                stopwatch.Stop();
                Debug.Log($"Registering Events for {nameof(RegisterEvents)} took {stopwatch.Elapsed}");
            }); ;
        }

        public static void LogRegisteredEventsCount()
        {
            Debug.Log($"Found {LoginMethods.Count} Login Event Methods");
            Debug.Log($"Found {ChannelMethods.Count} Channel Event Methods");
            Debug.Log($"Found {VoiceChannelMethods.Count} Voice Channel Event Methods");
            Debug.Log($"Found {TextChannelMethods.Count} Text Channel Event Methods");
        }

        public static void RegisterLoginMethods(Type[] types)
        {
            Debug.Log($"Searching for Login Methods to Register");
            foreach (Type type in types)
            {
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(LoginEventAttribute)))
                    {
                        LoginMethods.Add(methodInfo);
                    }
                }
            }
        }

        public static void RegisterChannelMethods(Type[] types)
        {
            Debug.Log($"Searching for Channel Methods to Register");
            foreach (Type type in types)
            {
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(ChannelEventAttribute)))
                    {
                        ChannelMethods.Add(methodInfo);
                    }
                }
            }
        }

        public static void RegisterVoiceChannelMethods(Type[] types)
        {
            Debug.Log($"Searching for Voice Channel Methods to Register");
            foreach (Type type in types)
            {
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(VoiceChannelEventAttribute)))
                    {
                        ChannelMethods.Add(methodInfo);
                    }
                }
            }
        }

        public static void RegisterTextChannelMethods(Type[] types)
        {
            Debug.Log($"Searching for Text Channel Methods to Register");
            foreach (Type type in types)
            {
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(TextChannelEventAttribute)))
                    {
                        ChannelMethods.Add(methodInfo);
                    }
                }
            }
        }

    }
}
