using EasyCodeForVivox;
using EasyCodeForVivox.Events;
using EasyCodeForVivox.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using VivoxUnity;

public class ValidateDynamicEvents
{
    //[InitializeOnLoadMethod]
    private static async void ValidateAllDynamicEvents()
    {
        Debug.Log("Validating Dynamic Events".Color(EasyDebug.Yellow));
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        await Task.Run(() =>
        {
            foreach (var assembly in assemblies)
            {
                var assemblyName = assembly.GetName().Name;
                if (HandleDynamicEvents.InternalAssemblyNames.Contains(assemblyName))
                {
                    continue;
                }
                Type[] types = assembly.GetTypes();
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

                foreach (var type in types)
                {
                    foreach (var methodInfo in type.GetMethods(flags))
                    {
                        var attribute = methodInfo.GetCustomAttribute<LoginEventAttribute>();
                        if (attribute != null)
                        {
                            Debug.Log($" [{attribute.GetType().Name}({attribute.Options})] {methodInfo.GetBaseDefinition().Name}\n"
                                + $"{methodInfo.ReturnType} {methodInfo.Name}({string.Join(", ", methodInfo.GetParameters().Select(p => $"{p.ParameterType} {p.Name}"))})");
                        }
                    }
                }
            }
        });
    }

    private static string GetCachedEvents()
    {
        if (PlayerPrefs.HasKey("ValidatedDynamicEvents"))
        {
            Debug.Log("found key ValidatedDynamicEvents");
            Debug.Log(PlayerPrefs.GetString("ValidatedDynamicEvents"));
        }
        else
        {
            PlayerPrefs.SetString("ValidatedDynamicEvents", string.Join(", ", InternalAssemblyNames));
        }
        return "";
    }

    public static readonly HashSet<string> InternalAssemblyNames = new HashSet<string>()
{
        "unityplastic",
        "log4net",
        "NiceIO",
        "PlayerBuildProgramLibrary.Data",
        "AndroidPlayerBuildProgram.Data",
        "WebGLPlayerBuildProgram.Data",
        "BeeBuildProgramCommon.Data",
        "ScriptCompilationBuildProgram.Data",
        "Microsoft.CSharp",
        "PlayerBuildProgramLibrary.Data",
        "ParrelSync",
        "VivoxTests",
        "ChatChannelSample.Editor",
        "ChatChannelSample",
        "Plugins.BackgroundRecompiler.Editor",
        "Bee.BeeDriver",
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


    //[MenuItem("EasyCode/Validate Dynamic Events")]
    public static async void Validate()
    {
        Debug.Log("Validating Dynamic Events".Color(EasyDebug.Yellow));
        System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (Assembly assembly in assemblies)
        {
            var assemblyName = assembly.GetName().Name;
            if (assembly.IsDynamic) { continue; }
            if (assemblyName.StartsWith("System") || assemblyName.StartsWith("Unity") || assemblyName.StartsWith("UnityEditor") ||
                assemblyName.StartsWith("UnityEngine") || InternalAssemblyNames.Contains(assemblyName.Trim()) || assemblyName.StartsWith("Mono")
                )
            {
                continue;
            }
            Debug.Log($"Searching Assembly [ {assembly.GetName().Name} ] for Dynamic Events".Color(EasyDebug.Teal));


            Type[] types = assembly.GetTypes();
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            // methods do repeat some code but instead of for looping and awaiting on 1 thread
            // this enables me to create many tasks to (hopefully) run at the same time and
            // take advantage of multiple cores/threads to speed up the execution time at startup
            await Task.Run(() => ValidateLoginEvents(types, flags));
            await Task.Run(() => RegisterChannelEvents(types, flags));
            await Task.Run(() => RegisterAudioChannelEvents(types, flags));
            await Task.Run(() => RegisterTextChannelEvents(types, flags));
            await Task.Run(() => RegisterChannelMessageEvents(types, flags));
            await Task.Run(() => RegisterDirectMessageEvents(types, flags));
            await Task.Run(() => RegisterUserEvents(types, flags));
            await Task.Run(() => RegisterUserEvents(types, flags));
            await Task.Run(() => RegisterTextToSpeechEvents(types, flags));
        }

        LogRegisteredEventsCount();

        stopwatch.Stop();
        Debug.Log($"Validating Dynamic Events took [hour:min:sec.ms] {stopwatch.Elapsed}".Color(EasyDebug.Green));
    }

    public static void LogRegisteredEventsCount()
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

    public static void ValidateLoginEvents(Type[] types, BindingFlags flags)
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
                            if (methodInfo.GetParameters()[0].ParameterType != typeof(ILoginSession))
                            {
                                Debug.Log($"Method [{type.Name}] {methodInfo.Name} does not have valid event parameter of type {typeof(ILoginSession).Name}");
                            }
                            break;
                        case LoginStatus.LoggedIn:

                            break;
                        case LoginStatus.LoggingOut:

                            break;
                        case LoginStatus.LoggedOut:

                            break;
                        case LoginStatus.LoginAdded:

                            break;
                        case LoginStatus.LoginRemoved:

                            break;
                        case LoginStatus.LoginValuesUpdated:

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

                            break;
                        case LoginStatus.LoggedIn:

                            break;
                        case LoginStatus.LoggingOut:

                            break;
                        case LoginStatus.LoggedOut:

                            break;
                        case LoginStatus.LoginAdded:

                            break;
                        case LoginStatus.LoginRemoved:

                            break;
                        case LoginStatus.LoginValuesUpdated:

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

                            break;
                        case ChannelStatus.ChannelConnected:

                            break;
                        case ChannelStatus.ChannelDisconnecting:

                            break;
                        case ChannelStatus.ChannelDisconnected:

                            break;
                    }
                    continue;
                }
                var asyncAttribute = methodInfo.GetCustomAttribute<ChannelEventAsyncAttribute>();
                if (asyncAttribute != null)
                {
                    switch (asyncAttribute.Options)
                    {
                        case ChannelStatus.ChannelConnecting:

                            break;
                        case ChannelStatus.ChannelConnected:

                            break;
                        case ChannelStatus.ChannelDisconnecting:

                            break;
                        case ChannelStatus.ChannelDisconnected:

                            break;
                    }
                }
            }
        });
    }

    public static void RegisterAudioDeviceEvents(Type[] types, BindingFlags flags)
    {
        Parallel.ForEach(types, type =>
        {
            foreach (MethodInfo methodInfo in type.GetMethods(flags))
            {
                var attribute = methodInfo.GetCustomAttribute<AudioDeviceEventAttribute>();
                if (attribute != null)
                {
                    switch (attribute.Options)
                    {
                        case AudioDeviceStatus.AudioInputDeviceAdded:

                            break;
                        case AudioDeviceStatus.AudioInputDeviceRemoved:

                            break;
                        case AudioDeviceStatus.AudioInputDeviceUpdated:

                            break;
                        case AudioDeviceStatus.AudioOutputDeviceAdded:

                            break;
                        case AudioDeviceStatus.AudioOutputDeviceRemoved:

                            break;
                        case AudioDeviceStatus.AudioOutputDeviceUpdated:

                            break;
                    }
                    continue;
                }
                var asyncAttribute = methodInfo.GetCustomAttribute<AudioDeviceEventAsyncAttribute>();
                if (asyncAttribute != null)
                {
                    switch (asyncAttribute.Options)
                    {
                        case AudioDeviceStatus.AudioInputDeviceAdded:

                            break;
                        case AudioDeviceStatus.AudioInputDeviceRemoved:

                            break;
                        case AudioDeviceStatus.AudioInputDeviceUpdated:

                            break;
                        case AudioDeviceStatus.AudioOutputDeviceAdded:

                            break;
                        case AudioDeviceStatus.AudioOutputDeviceRemoved:

                            break;
                        case AudioDeviceStatus.AudioOutputDeviceUpdated:

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

                            break;
                        case AudioChannelStatus.AudioChannelConnected:

                            break;
                        case AudioChannelStatus.AudioChannelDisconnecting:

                            break;
                        case AudioChannelStatus.AudioChannelDisconnected:

                            break;
                    }
                    continue;
                }
                var asyncAttribute = methodInfo.GetCustomAttribute<AudioChannelEventAsyncAttribute>();
                if (asyncAttribute != null)
                {
                    switch (asyncAttribute.Options)
                    {
                        case AudioChannelStatus.AudioChannelConnecting:

                            break;
                        case AudioChannelStatus.AudioChannelConnected:

                            break;
                        case AudioChannelStatus.AudioChannelDisconnecting:

                            break;
                        case AudioChannelStatus.AudioChannelDisconnected:

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

                            break;
                        case TextChannelStatus.TextChannelConnected:

                            break;
                        case TextChannelStatus.TextChannelDisconnecting:

                            break;
                        case TextChannelStatus.TextChannelDisconnected:

                            break;
                    }
                    continue;
                }
                var asyncAttribute = methodInfo.GetCustomAttribute<TextChannelEventAsyncAttribute>();
                if (asyncAttribute != null)
                {
                    switch (asyncAttribute.Options)
                    {
                        case TextChannelStatus.TextChannelConnecting:

                            break;
                        case TextChannelStatus.TextChannelConnected:

                            break;
                        case TextChannelStatus.TextChannelDisconnecting:

                            break;
                        case TextChannelStatus.TextChannelDisconnected:

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

                            break;
                        case ChannelMessageStatus.ChannelMessageRecieved:

                            break;
                        case ChannelMessageStatus.EventMessageRecieved:

                            break;
                    }
                    continue;
                }
                var asyncAttribute = methodInfo.GetCustomAttribute<ChannelMessageEventAsyncAttribute>();
                if (asyncAttribute != null)
                {
                    switch (asyncAttribute.Options)
                    {
                        case ChannelMessageStatus.ChannelMessageSent:

                            break;
                        case ChannelMessageStatus.ChannelMessageRecieved:

                            break;
                        case ChannelMessageStatus.EventMessageRecieved:

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

                            break;
                        case DirectMessageStatus.DirectMessageRecieved:

                            break;
                        case DirectMessageStatus.DirectMessageFailed:

                            break;
                    }
                    continue;
                }
                var asyncAttribute = methodInfo.GetCustomAttribute<DirectMessageEventAsyncAttribute>();
                if (asyncAttribute != null)
                {
                    switch (asyncAttribute.Options)
                    {
                        case DirectMessageStatus.DirectMessageSent:

                            break;
                        case DirectMessageStatus.DirectMessageRecieved:

                            break;
                        case DirectMessageStatus.DirectMessageFailed:

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

                            break;
                        case UserStatus.UserJoinedChannel:

                            break;
                        case UserStatus.UserValuesUpdated:

                            break;
                        case UserStatus.UserMuted:

                            break;
                        case UserStatus.UserUnmuted:

                            break;
                        case UserStatus.UserCrossMuted:

                            break;
                        case UserStatus.UserCrossUnmuted:

                            break;
                        case UserStatus.UserNotSpeaking:

                            break;
                        case UserStatus.UserSpeaking:

                            break;
                        case UserStatus.LocalUserMuted:

                            break;
                        case UserStatus.LocalUserUnmuted:

                            break;
                    }
                    continue;
                }
                var asyncAttribute = methodInfo.GetCustomAttribute<UserEventAsyncAttribute>();
                if (asyncAttribute != null)
                {
                    switch (asyncAttribute.Options)
                    {
                        case UserStatus.UserLeftChannel:

                            break;
                        case UserStatus.UserJoinedChannel:

                            break;
                        case UserStatus.UserValuesUpdated:

                            break;
                        case UserStatus.UserMuted:

                            break;
                        case UserStatus.UserUnmuted:

                            break;

                            break;
                        case UserStatus.UserCrossUnmuted:

                            break;
                        case UserStatus.UserNotSpeaking:

                            break;
                        case UserStatus.UserSpeaking:

                            break;
                        case UserStatus.LocalUserMuted:

                            break;
                        case UserStatus.LocalUserUnmuted:

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

                            break;
                        case TextToSpeechStatus.TTSMessageRemoved:

                            break;
                        case TextToSpeechStatus.TTSMessageUpdated:

                            break;
                    }
                    continue;
                }
                var asyncAttribute = methodInfo.GetCustomAttribute<TextToSpeechEventAsyncAttribute>();
                if (asyncAttribute != null)
                {
                    switch (asyncAttribute.Options)
                    {
                        case TextToSpeechStatus.TTSMessageAdded:

                            break;
                        case TextToSpeechStatus.TTSMessageRemoved:

                            break;
                        case TextToSpeechStatus.TTSMessageUpdated:

                            break;
                    }
                }
            }
        });
    }












}
