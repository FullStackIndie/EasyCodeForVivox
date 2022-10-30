using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace EasyCodeForVivox.Events
{
    public static class DynamicEvents
    {
        private static readonly HashSet<string> internalAssemblyNames = new HashSet<string>()
{
    "Zenject-Editor",
    "Zenject-ReflectionBaking-Editor",
    "Zenject",
    "Zenject-TestFramework",
    "Zenject.ReflectionBaking.Mono.Cecil",
    "Zenject.ReflectionBaking.Mono.Cecil.Mdb",
    "Zenject.ReflectionBaking.Mono.Cecil.Pdb",
    "Zenject.ReflectionBaking.Mono.Cecil.Rocks",
    "Zenject-usage",
    "VivoxAccessToken",
    "VivoxUnity",
    "AssetStoreTools",
    "mscorlib",
    "Mono.Security",
    "System",
    "System.Core",
    "System.Security.Cryptography.Algorithms",
    "System.Net.Http",
    "System.Data",
    "System.Runtime.Serialization",
    "System.Xml.Linq",
    "System.Numerics",
    "System.Xml",
    "System.Configuration",
    "ExCSS.Unity",
    "Unity.Cecil",
    "Unity.CompilationPipeline.Common",
    "Unity.SerializationLogic",
    "Unity.TestTools.CodeCoverage.Editor",
    "Unity.ScriptableBuildPipeline.Editor",
    "Unity.Addressables.Editor",
    "Unity.ScriptableBuildPipeline",
    "Unity.CollabProxy.Editor",
    "Unity.Timeline.Editor",
    "Unity.PerformanceTesting.Tests.Runtime",
    "Unity.Settings.Editor",
    "Unity.PerformanceTesting",
    "Unity.PerformanceTesting.Editor",
    "Unity.Rider.Editor",
    "Unity.ResourceManager",
    "Unity.TestTools.CodeCoverage.Editor.OpenCover.Mono.Reflection",
    "Unity.PerformanceTesting.Tests.Editor",
    "Unity.TextMeshPro",
    "Unity.Timeline",
    "Unity.Addressables",
    "Unity.TestTools.CodeCoverage.Editor.OpenCover.Model",
    "Unity.VisualStudio.Editor",
    "Unity.TextMeshPro.Editor",
    "Unity.VSCode.Editor",
    "UnityEditor",
    "UnityEditor.UI",
    "UnityEditor.TestRunner",
    "UnityEditor.CacheServer",
    "UnityEditor.WindowsStandalone.Extensions",
    "UnityEditor.Graphs",
    "UnityEditor.UnityConnectModule",
    "UnityEditor.UIServiceModule",
    "UnityEditor.UIElementsSamplesModule",
    "UnityEditor.UIElementsModule",
    "UnityEditor.SceneTemplateModule",
    "UnityEditor.PackageManagerUIModule",
    "UnityEditor.GraphViewModule",
    "UnityEditor.CoreModule",
    "UnityEngine",
    "UnityEngine.UI",
    "UnityEngine.XRModule",
    "UnityEngine.WindModule",
    "UnityEngine.VirtualTexturingModule",
    "UnityEngine.TestRunner",
    "UnityEngine.VideoModule",
    "UnityEngine.VehiclesModule",
    "UnityEngine.VRModule",
    "UnityEngine.VFXModule",
    "UnityEngine.UnityWebRequestWWWModule",
    "UnityEngine.UnityWebRequestTextureModule",
    "UnityEngine.UnityWebRequestAudioModule",
    "UnityEngine.UnityWebRequestAssetBundleModule",
    "UnityEngine.UnityWebRequestModule",
    "UnityEngine.UnityTestProtocolModule",
    "UnityEngine.UnityCurlModule",
    "UnityEngine.UnityConnectModule",
    "UnityEngine.UnityAnalyticsModule",
    "UnityEngine.UmbraModule",
    "UnityEngine.UNETModule",
    "UnityEngine.UIElementsNativeModule",
    "UnityEngine.UIElementsModule",
    "UnityEngine.UIModule",
    "UnityEngine.TilemapModule",
    "UnityEngine.TextRenderingModule",
    "UnityEngine.TextCoreModule",
    "UnityEngine.TerrainPhysicsModule",
    "UnityEngine.TerrainModule",
    "UnityEngine.TLSModule",
    "UnityEngine.SubsystemsModule",
    "UnityEngine.SubstanceModule",
    "UnityEngine.StreamingModule",
    "UnityEngine.SpriteShapeModule",
    "UnityEngine.SpriteMaskModule",
    "UnityEngine.SharedInternalsModule",
    "UnityEngine.ScreenCaptureModule",
    "UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule",
    "UnityEngine.ProfilerModule",
    "UnityEngine.Physics2DModule",
    "UnityEngine.PhysicsModule",
    "UnityEngine.PerformanceReportingModule",
    "UnityEngine.ParticleSystemModule",
    "UnityEngine.LocalizationModule",
    "UnityEngine.JSONSerializeModule",
    "UnityEngine.InputLegacyModule",
    "UnityEngine.InputModule",
    "UnityEngine.ImageConversionModule",
    "UnityEngine.IMGUIModule",
    "UnityEngine.HotReloadModule",
    "UnityEngine.GridModule",
    "UnityEngine.GameCenterModule",
    "UnityEngine.GIModule",
    "UnityEngine.DirectorModule",
    "UnityEngine.DSPGraphModule",
    "UnityEngine.CrashReportingModule",
    "UnityEngine.CoreModule",
    "UnityEngine.ClusterRendererModule",
    "UnityEngine.ClusterInputModule",
    "UnityEngine.ClothModule",
    "UnityEngine.AudioModule",
    "UnityEngine.AssetBundleModule",
    "UnityEngine.AnimationModule",
    "UnityEngine.AndroidJNIModule",
    "UnityEngine.AccessibilityModule",
    "UnityEngine.ARModule",
    "UnityEngine.AIModule",
    "SyntaxTree.VisualStudio.Unity.Bridge",
    "nunit.framework",
    "Newtonsoft.Json",
    "ReportGeneratorMerged",
    "Unrelated",
    "netstandard",
    "SyntaxTree.VisualStudio.Unity.Messaging"
};

        public static Dictionary<Enum, List<MethodInfo>> Methods = new Dictionary<Enum, List<MethodInfo>>();

        public static async Task RegisterEvents(bool onlySearchAssemblyCSharp = true, bool logAssemblySearches = true, bool logAllDynamicMethods = false)
        {
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            if (onlySearchAssemblyCSharp)
            {
                assemblies = assemblies.Where(x => x.GetName().Name == "Assembly-CSharp").ToArray();
            }

            foreach (Assembly assembly in assemblies)
            {
                var assemblyName = assembly.GetName().Name;
                if (assembly.IsDynamic) { continue; }
                if (assemblyName.StartsWith("System") || assemblyName.StartsWith("Unity") || assemblyName.StartsWith("UnityEditor") ||
                    assemblyName.StartsWith("UnityEngine") || internalAssemblyNames.Contains(assemblyName) || assemblyName.StartsWith("Mono")
                    )
                {
                    continue;
                }
                if (logAssemblySearches)
                {
                    Debug.Log($"Searching Assembly [ {assembly.GetName().Name} ] for Dynamic Events".Color(EasyDebug.Teal));
                }

                Type[] types = assembly.GetTypes();
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

                // methods do repeat some code but instead of for looping and awaiting on 1 thread
                // this enables me to create many tasks to hopefully run at the same time and
                // take advantage of multiple cores/threads to speed up the execution time at startup
                await Task.Run(() => RegisterLoginEvents(types, flags));
                await Task.Run(() => RegisterChannelEvents(types, flags));
                await Task.Run(() => RegisterAudioChannelEvents(types, flags));
                await Task.Run(() => RegisterTextChannelEvents(types, flags));
                await Task.Run(() => RegisterChannelMessageEvents(types, flags));
                await Task.Run(() => RegisterDirectMessageEvents(types, flags));
                await Task.Run(() => RegisterUserEvents(types, flags));
                await Task.Run(() => RegisterUserAudioEvents(types, flags));
                await Task.Run(() => RegisterTextToSpeechEvents(types, flags));
            }

            LogRegisteredEventsCount(logAllDynamicMethods);

            stopwatch.Stop();
            Debug.Log($"Registering Dynamic Events took [hour:min:sec.ms] {stopwatch.Elapsed}".Color(EasyDebug.Teal));
        }

        public static void LogRegisteredEventsCount(bool logAllDynamicMethods)
        {
            if (logAllDynamicMethods)
            {
                foreach (var events in Methods)
                {
                    foreach (var method in events.Value)
                    {
                        Debug.Log($"Dynamic Event = {events.Key} : MethodName = {method.Name} : From Class {method.DeclaringType}");
                    }
                }

                var loginEvents = Methods.Where(e => e.Key.ToString().StartsWith("Logg")).Select(m => m.Value.Count);
                Debug.Log($"Found {loginEvents.Count()} Login Event Methods".Color(EasyDebug.Lightblue));

                var channelEvents = Methods.Where(e => e.Key.ToString().StartsWith("Channel")).Select(m => m.Value.Count);
                Debug.Log($"Found {channelEvents.Count()} Channel Event Methods".Color(EasyDebug.Lightblue));

                var audioChannelEvents = Methods.Where(e => e.Key.ToString().StartsWith("Audio")).Select(m => m.Value.Count);
                Debug.Log($"Found {audioChannelEvents.Count()} Audio Channel Event Methods".Color(EasyDebug.Lightblue));

                var textChannelEvents = Methods.Where(e => e.Key.ToString().StartsWith("Text")).Select(m => m.Value.Count);
                Debug.Log($"Found {textChannelEvents.Count()} Text Channel Event Methods".Color(EasyDebug.Lightblue));

                var channelMessageEvents = Methods.Where(e => e.Key.ToString().StartsWith("ChannelMessage") || e.Key.ToString().StartsWith("EventMessage")).Select(m => m.Value.Count);
                Debug.Log($"Found {channelMessageEvents.Count()} Channel Message Event Methods".Color(EasyDebug.Lightblue));

                var directMessageEvents = Methods.Where(e => e.Key.ToString().StartsWith("Direct")).Select(m => m.Value.Count);
                Debug.Log($"Found {directMessageEvents.Count()} Direct Message Event Methods".Color(EasyDebug.Lightblue));

                var userEvents = Methods.Where(e => e.Key.ToString().Contains("User") || e.Key.ToString().Contains("LocalUser")).Select(m => m.Value.Count);
                Debug.Log($"Found {userEvents.Count()} User Events/User Audio Event Methods".Color(EasyDebug.Lightblue));

                var ttsEvents = Methods.Where(e => e.Key.ToString().Contains("TTS")).Select(m => m.Value.Count);
                Debug.Log($"Found {ttsEvents.Count()} Text To Speech Event Methods".Color(EasyDebug.Lightblue));
            }
        }

        public static void AddDynamicEvent(Enum value, MethodInfo methodInfo)
        {
            if (!Methods.ContainsKey(value))
            {
                Methods.Add(value, new List<MethodInfo>());
            }
            Methods[value].Add(methodInfo);
        }

        public static void RegisterLoginEvents(Type[] types, BindingFlags flags)
        {
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    var attribute = methodInfo.GetCustomAttribute<LoginEventAttribute>();
                    if (attribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case LoginStatus.LoggingIn:
                                AddDynamicEvent(LoginStatus.LoggingIn, methodInfo);
                                break;
                            case LoginStatus.LoggedIn:
                                AddDynamicEvent(LoginStatus.LoggedIn, methodInfo);
                                break;
                            case LoginStatus.LoggingOut:
                                AddDynamicEvent(LoginStatus.LoggingOut, methodInfo);
                                break;
                            case LoginStatus.LoggedOut:
                                AddDynamicEvent(LoginStatus.LoggedOut, methodInfo);
                                break;
                        }
                        continue;
                    }
                    var asyncAttribute = methodInfo.GetCustomAttribute<LoginEventAsyncAttribute>();
                    if (asyncAttribute != null)
                    {
                        switch (asyncAttribute.Options)
                        {
                            case LoginStatus.LoggingIn:
                                AddDynamicEvent(LoginStatusAsync.LoggingInAsync, methodInfo);
                                break;
                            case LoginStatus.LoggedIn:
                                AddDynamicEvent(LoginStatusAsync.LoggedInAsync, methodInfo);
                                break;
                            case LoginStatus.LoggingOut:
                                AddDynamicEvent(LoginStatusAsync.LoggingOutAsync, methodInfo);
                                break;
                            case LoginStatus.LoggedOut:
                                AddDynamicEvent(LoginStatusAsync.LoggedOutAsync, methodInfo);
                                break;
                        }
                    }
                }
            });
        }

        public static void RegisterChannelEvents(Type[] types, BindingFlags flags)
        {
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    var attribute = methodInfo.GetCustomAttribute<ChannelEventAttribute>();
                    if (attribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case ChannelStatus.ChannelConnecting:
                                AddDynamicEvent(ChannelStatus.ChannelConnecting, methodInfo);
                                break;
                            case ChannelStatus.ChannelConnected:
                                AddDynamicEvent(ChannelStatus.ChannelConnected, methodInfo);
                                break;
                            case ChannelStatus.ChannelDisconnecting:
                                AddDynamicEvent(ChannelStatus.ChannelDisconnecting, methodInfo);
                                break;
                            case ChannelStatus.ChannelDisconnected:
                                AddDynamicEvent(ChannelStatus.ChannelDisconnected, methodInfo);
                                break;
                        }
                        continue;
                    }
                    var asyncAttribute = methodInfo.GetCustomAttribute<ChannelEventAsyncAttribute>();
                    if (asyncAttribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case ChannelStatus.ChannelConnecting:
                                AddDynamicEvent(ChannelStatusAsync.ChannelConnectingAsync, methodInfo);
                                break;
                            case ChannelStatus.ChannelConnected:
                                AddDynamicEvent(ChannelStatusAsync.ChannelConnectedAsync, methodInfo);
                                break;
                            case ChannelStatus.ChannelDisconnecting:
                                AddDynamicEvent(ChannelStatusAsync.ChannelDisconnectingAsync, methodInfo);
                                break;
                            case ChannelStatus.ChannelDisconnected:
                                AddDynamicEvent(ChannelStatusAsync.ChannelDisconnectedAsync, methodInfo);
                                break;
                        }
                    }
                }
            });
        }

        public static void RegisterAudioChannelEvents(Type[] types, BindingFlags flags)
        {
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    var attribute = methodInfo.GetCustomAttribute<AudioChannelEventAttribute>();
                    if (attribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case AudioChannelStatus.AudioChannelConnecting:
                                AddDynamicEvent(AudioChannelStatus.AudioChannelConnecting, methodInfo);
                                break;
                            case AudioChannelStatus.AudioChannelConnected:
                                AddDynamicEvent(AudioChannelStatus.AudioChannelConnected, methodInfo);
                                break;
                            case AudioChannelStatus.AudioChannelDisconnecting:
                                AddDynamicEvent(AudioChannelStatus.AudioChannelDisconnecting, methodInfo);
                                break;
                            case AudioChannelStatus.AudioChannelDisconnected:
                                AddDynamicEvent(AudioChannelStatus.AudioChannelDisconnected, methodInfo);
                                break;
                        }
                        continue;
                    }
                    var asyncAttribute = methodInfo.GetCustomAttribute<AudioChannelEventAsyncAttribute>();
                    if (asyncAttribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case AudioChannelStatus.AudioChannelConnecting:
                                AddDynamicEvent(AudioChannelStatusAsync.AudioChannelConnectingAsync, methodInfo);
                                break;
                            case AudioChannelStatus.AudioChannelConnected:
                                AddDynamicEvent(AudioChannelStatusAsync.AudioChannelConnectedAsync, methodInfo);
                                break;
                            case AudioChannelStatus.AudioChannelDisconnecting:
                                AddDynamicEvent(AudioChannelStatusAsync.AudioChannelDisconnectingAsync, methodInfo);
                                break;
                            case AudioChannelStatus.AudioChannelDisconnected:
                                AddDynamicEvent(AudioChannelStatusAsync.AudioChannelDisconnectedAsync, methodInfo);
                                break;
                        }
                    }
                }
            });
        }

        public static void RegisterTextChannelEvents(Type[] types, BindingFlags flags)
        {
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    var attribute = methodInfo.GetCustomAttribute<TextChannelEventAttribute>();
                    if (attribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case TextChannelStatus.TextChannelConnecting:
                                AddDynamicEvent(TextChannelStatus.TextChannelConnecting, methodInfo);
                                break;
                            case TextChannelStatus.TextChannelConnected:
                                AddDynamicEvent(TextChannelStatus.TextChannelConnected, methodInfo);
                                break;
                            case TextChannelStatus.TextChannelDisconnecting:
                                AddDynamicEvent(TextChannelStatus.TextChannelDisconnecting, methodInfo);
                                break;
                            case TextChannelStatus.TextChannelDisconnected:
                                AddDynamicEvent(TextChannelStatus.TextChannelDisconnected, methodInfo);
                                break;
                        }
                        continue;
                    }
                    var asyncAttribute = methodInfo.GetCustomAttribute<TextChannelEventAsyncAttribute>();
                    if (asyncAttribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case TextChannelStatus.TextChannelConnecting:
                                AddDynamicEvent(TextChannelStatusAsync.TextChannelConnectingAsync, methodInfo);
                                break;
                            case TextChannelStatus.TextChannelConnected:
                                AddDynamicEvent(TextChannelStatusAsync.TextChannelConnectedAsync, methodInfo);
                                break;
                            case TextChannelStatus.TextChannelDisconnecting:
                                AddDynamicEvent(TextChannelStatusAsync.TextChannelDisconnectingAsync, methodInfo);
                                break;
                            case TextChannelStatus.TextChannelDisconnected:
                                AddDynamicEvent(TextChannelStatusAsync.TextChannelDisconnectedAsync, methodInfo);
                                break;
                        }
                    }
                }
            });
        }

        public static void RegisterChannelMessageEvents(Type[] types, BindingFlags flags)
        {
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    var attribute = methodInfo.GetCustomAttribute<ChannelMessageEventAttribute>();
                    if (attribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case ChannelMessageStatus.ChannelMessageSent:
                                AddDynamicEvent(ChannelMessageStatus.ChannelMessageSent, methodInfo);
                                break;
                            case ChannelMessageStatus.ChannelMessageRecieved:
                                AddDynamicEvent(ChannelMessageStatus.ChannelMessageRecieved, methodInfo);
                                break;
                            case ChannelMessageStatus.EventMessageRecieved:
                                AddDynamicEvent(ChannelMessageStatus.EventMessageRecieved, methodInfo);
                                break;
                        }
                        continue;
                    }
                    var asyncAttribute = methodInfo.GetCustomAttribute<ChannelMessageEventAsyncAttribute>();
                    if (asyncAttribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case ChannelMessageStatus.ChannelMessageSent:
                                AddDynamicEvent(ChannelMessageStatusAsync.ChannelMessageSentAsync, methodInfo);
                                break;
                            case ChannelMessageStatus.ChannelMessageRecieved:
                                AddDynamicEvent(ChannelMessageStatusAsync.ChannelMessageRecievedAsync, methodInfo);
                                break;
                            case ChannelMessageStatus.EventMessageRecieved:
                                AddDynamicEvent(ChannelMessageStatusAsync.EventMessageRecievedAsync, methodInfo);
                                break;
                        }
                    }
                }
            });
        }

        public static void RegisterDirectMessageEvents(Type[] types, BindingFlags flags)
        {
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    var attribute = methodInfo.GetCustomAttribute<DirectMessageEventAttribute>();
                    if (attribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case DirectMessageStatus.DirectMessageSent:
                                AddDynamicEvent(DirectMessageStatus.DirectMessageSent, methodInfo);
                                break;
                            case DirectMessageStatus.DirectMessageRecieved:
                                AddDynamicEvent(DirectMessageStatus.DirectMessageRecieved, methodInfo);
                                break;
                            case DirectMessageStatus.DirectMessageFailed:
                                AddDynamicEvent(DirectMessageStatus.DirectMessageFailed, methodInfo);
                                break;
                        }
                        continue;
                    }
                    var asyncAttribute = methodInfo.GetCustomAttribute<DirectMessageEventAsyncAttribute>();
                    if (asyncAttribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case DirectMessageStatus.DirectMessageSent:
                                AddDynamicEvent(DirectMessageStatusAsync.DirectMessageSentAsync, methodInfo);
                                break;
                            case DirectMessageStatus.DirectMessageRecieved:
                                AddDynamicEvent(DirectMessageStatusAsync.DirectMessageRecievedAsync, methodInfo);
                                break;
                            case DirectMessageStatus.DirectMessageFailed:
                                AddDynamicEvent(DirectMessageStatusAsync.DirectMessageFailedAsync, methodInfo);
                                break;
                        }
                    }
                }
            });
        }

        public static void RegisterUserEvents(Type[] types, BindingFlags flags)
        {
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    var attribute = methodInfo.GetCustomAttribute<UserEventAttribute>();
                    if (attribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case UserStatus.UserLeftChannel:
                                AddDynamicEvent(UserStatus.UserLeftChannel, methodInfo);
                                break;
                            case UserStatus.UserJoinedChannel:
                                AddDynamicEvent(UserStatus.UserJoinedChannel, methodInfo);
                                break;
                            case UserStatus.UserValuesUpdated:
                                AddDynamicEvent(UserStatus.UserValuesUpdated, methodInfo);
                                break;
                        }
                        continue;
                    }
                    var asyncAttribute = methodInfo.GetCustomAttribute<UserEventAsyncAttribute>();
                    if (asyncAttribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case UserStatus.UserLeftChannel:
                                AddDynamicEvent(UserStatusAsync.UserLeftChannelAsync, methodInfo);
                                break;
                            case UserStatus.UserJoinedChannel:
                                AddDynamicEvent(UserStatusAsync.UserJoinedChannelAsync, methodInfo);
                                break;
                            case UserStatus.UserValuesUpdated:
                                AddDynamicEvent(UserStatusAsync.UserValuesUpdatedAsync, methodInfo);
                                break;
                        }
                    }
                }
            });
        }

        public static void RegisterUserAudioEvents(Type[] types, BindingFlags flags)
        {
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    var attribute = methodInfo.GetCustomAttribute<UserAudioEventAttribute>();
                    if (attribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case UserAudioStatus.UserMuted:
                                AddDynamicEvent(UserAudioStatus.UserMuted, methodInfo);
                                break;
                            case UserAudioStatus.UserUnmuted:
                                AddDynamicEvent(UserAudioStatus.UserUnmuted, methodInfo);
                                break;
                            case UserAudioStatus.UserCrossMuted:
                                AddDynamicEvent(UserAudioStatus.UserCrossMuted, methodInfo);
                                break;
                            case UserAudioStatus.UserCrossUnmuted:
                                AddDynamicEvent(UserAudioStatus.UserCrossUnmuted, methodInfo);
                                break;
                            case UserAudioStatus.UserNotSpeaking:
                                AddDynamicEvent(UserAudioStatus.UserNotSpeaking, methodInfo);
                                break;
                            case UserAudioStatus.UserSpeaking:
                                AddDynamicEvent(UserAudioStatus.UserSpeaking, methodInfo);
                                break;
                            case UserAudioStatus.LocalUserMuted:
                                AddDynamicEvent(UserAudioStatus.LocalUserMuted, methodInfo);
                                break;
                            case UserAudioStatus.LocalUserUnmuted:
                                AddDynamicEvent(UserAudioStatus.LocalUserUnmuted, methodInfo);
                                break;
                        }
                        continue;
                    }
                    var asyncAttribute = methodInfo.GetCustomAttribute<UserAudioEventAsyncAttribute>();
                    if (asyncAttribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case UserAudioStatus.UserMuted:
                                AddDynamicEvent(UserAudioStatusAsync.UserMutedAsync, methodInfo);
                                break;
                            case UserAudioStatus.UserUnmuted:
                                AddDynamicEvent(UserAudioStatusAsync.UserUnmutedAsync, methodInfo);
                                break;
                            case UserAudioStatus.UserCrossMuted:
                                AddDynamicEvent(UserAudioStatusAsync.UserCrossMutedAsync, methodInfo);
                                break;
                            case UserAudioStatus.UserCrossUnmuted:
                                AddDynamicEvent(UserAudioStatusAsync.UserCrossUnmutedAsync, methodInfo);
                                break;
                            case UserAudioStatus.UserNotSpeaking:
                                AddDynamicEvent(UserAudioStatusAsync.UserNotSpeakingAsync, methodInfo);
                                break;
                            case UserAudioStatus.UserSpeaking:
                                AddDynamicEvent(UserAudioStatusAsync.UserSpeakingAsync, methodInfo);
                                break;
                            case UserAudioStatus.LocalUserMuted:
                                AddDynamicEvent(UserAudioStatusAsync.LocalUserMutedAsync, methodInfo);
                                break;
                            case UserAudioStatus.LocalUserUnmuted:
                                AddDynamicEvent(UserAudioStatusAsync.LocalUserUnmutedAsync, methodInfo);
                                break;
                        }
                    }
                }
            });
        }

        public static void RegisterTextToSpeechEvents(Type[] types, BindingFlags flags)
        {
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    var attribute = methodInfo.GetCustomAttribute<TextToSpeechEventAttribute>();
                    if (attribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case TextToSpeechStatus.TTSMessageAdded:
                                AddDynamicEvent(TextToSpeechStatus.TTSMessageAdded, methodInfo);
                                break;
                            case TextToSpeechStatus.TTSMessageRemoved:
                                AddDynamicEvent(TextToSpeechStatus.TTSMessageRemoved, methodInfo);
                                break;
                            case TextToSpeechStatus.TTSMessageUpdated:
                                AddDynamicEvent(TextToSpeechStatus.TTSMessageUpdated, methodInfo);
                                break;
                        }
                        continue;
                    }
                    var asyncAttribute = methodInfo.GetCustomAttribute<UserAudioEventAsyncAttribute>();
                    if (asyncAttribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case TextToSpeechStatus.TTSMessageAdded:
                                AddDynamicEvent(TextToSpeechStatusAsync.TTSMessageAddedAsync, methodInfo);
                                break;
                            case TextToSpeechStatus.TTSMessageRemoved:
                                AddDynamicEvent(TextToSpeechStatusAsync.TTSMessageRemovedAsync, methodInfo);
                                break;
                            case TextToSpeechStatus.TTSMessageUpdated:
                                AddDynamicEvent(TextToSpeechStatusAsync.TTSMessageUpdatedAsync, methodInfo);
                                break;
                        }
                    }
                }
            });
        }











    }
}
