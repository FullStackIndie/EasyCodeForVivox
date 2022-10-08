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
        private static readonly HashSet<string> internalAssemblyNames = new HashSet<string>()
{
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

        public static List<MethodInfo> LoginEvents = new List<MethodInfo>();
        public static List<MethodInfo> LoginEventsAsync = new List<MethodInfo>();

        public static List<MethodInfo> ChannelEvents = new List<MethodInfo>();
        public static List<MethodInfo> ChannelEventsAsync = new List<MethodInfo>();

        public static List<MethodInfo> AudioChannelEvents = new List<MethodInfo>();
        public static List<MethodInfo> AudioChannelEventsAsync = new List<MethodInfo>();

        public static List<MethodInfo> TextChannelEvents = new List<MethodInfo>();
        public static List<MethodInfo> TextChannelEventsAsync = new List<MethodInfo>();

        public static List<MethodInfo> ChannelMessageEvents = new List<MethodInfo>();
        public static List<MethodInfo> ChannelMessageEventsAsync = new List<MethodInfo>();

        public static List<MethodInfo> DirectMessageEvents = new List<MethodInfo>();
        public static List<MethodInfo> DirectMessageEventsAsync = new List<MethodInfo>();

        public static List<MethodInfo> UserEvents = new List<MethodInfo>();
        public static List<MethodInfo> UserEventsAsync = new List<MethodInfo>();

        public static List<MethodInfo> UserAudioEvents = new List<MethodInfo>();
        public static List<MethodInfo> UserAudioEventsAsync = new List<MethodInfo>();

        public static List<MethodInfo> TextToSpeechEvents = new List<MethodInfo>();
        public static List<MethodInfo> TextToSpeechEventsAsync = new List<MethodInfo>();




        public static async Task RegisterEvents()
        {
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                var assemblyName = assembly.GetName().Name;
                if (assembly.IsDynamic) { continue; }
                if (assemblyName.StartsWith("System") || assemblyName.StartsWith("Unity") || assemblyName.StartsWith("UnityEditor") ||
                    assemblyName.StartsWith("UnityEngine") || internalAssemblyNames.Contains(assemblyName) || assemblyName.StartsWith("Mono"))
                {
                    continue;
                }
                Debug.Log($"Searching Assembly [ {assembly.GetName().Name} ]");

                Type[] types = assembly.GetTypes();
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

                // methods do repeat some code but instead of for looping and awaiting on 1 thread
                // this enables me to create many tasks to hopefully take advantage of multiple cores/threads to speed up the execution time at startup
                await Task.Run(() => RegisterLoginEvents(types, flags));
                await Task.Run(() => RegisterChannelEvents(types, flags));
                await Task.Run(() => RegisterVoiceChannelEvents(types, flags));
                await Task.Run(() => RegisterTextChannelEvents(types, flags));
                await Task.Run(() => RegisterChannelMessageEvents(types, flags));
                await Task.Run(() => RegisterDirectMessageEvents(types, flags));
                await Task.Run(() => RegisterUserEvents(types, flags));
                await Task.Run(() => RegisterUserAudioEvents(types, flags));
                await Task.Run(() => RegisterTextToSpeechEvents(types, flags));
            }

            LogRegisteredEventsCount();

            stopwatch.Stop();
            Debug.Log($"Registering Events took {stopwatch.Elapsed}");
        }

        public static void LogRegisteredEventsCount()
        {
            Debug.Log($"Found {LoginEvents.Count} Login Event Methods");
            Debug.Log($"Found {LoginEventsAsync.Count} Login Async Event Methods");
            Debug.Log($"Found {ChannelEvents.Count} Channel Event Methods");
            Debug.Log($"Found {AudioChannelEvents.Count} Voice Channel Event Methods");
            Debug.Log($"Found {TextChannelEvents.Count} Text Channel Event Methods");
        }

        public static void RegisterLoginEvents(Type[] types, BindingFlags flags)
        {
            Debug.Log($"Searching for Login Events to Register");
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    var attribute = methodInfo.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(LoginEventAttribute));
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(LoginEventAttribute)))
                    {
                        LoginEvents.Add(methodInfo);
                    }
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(LoginEventAsyncAttribute)))
                    {
                        LoginEventsAsync.Add(methodInfo);
                    }
                }
            });
        }

        public static void RegisterChannelEvents(Type[] types, BindingFlags flags)
        {
            Debug.Log($"Searching for Channel Events to Register");
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(ChannelEventAttribute)))
                    {
                        ChannelEvents.Add(methodInfo);
                    }
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(ChannelEventAsyncAttribute)))
                    {
                        ChannelEventsAsync.Add(methodInfo);
                    }
                }
            });
        }

        public static void RegisterVoiceChannelEvents(Type[] types, BindingFlags flags)
        {
            Debug.Log($"Searching for Voice Channel Events to Register");
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(AudioChannelEventAttribute)))
                    {
                        ChannelEvents.Add(methodInfo);
                    }
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(AudioChannelEventAsyncAttribute)))
                    {
                        ChannelEventsAsync.Add(methodInfo);
                    }
                }
            });
        }

        public static void RegisterTextChannelEvents(Type[] types, BindingFlags flags)
        {
            Debug.Log($"Searching for Text Channel Events to Register");
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(TextChannelEventAttribute)))
                    {
                        ChannelEvents.Add(methodInfo);
                    }
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(TextChannelEventAsyncAttribute)))
                    {
                        ChannelEventsAsync.Add(methodInfo);
                    }
                }
            });
        }

        public static void RegisterChannelMessageEvents(Type[] types, BindingFlags flags)
        {
            Debug.Log($"Searching for Channel Message Events to Register");
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(ChannelMessageEventAttribute)))
                    {
                        ChannelMessageEvents.Add(methodInfo);
                    }
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(ChannelMessageEventAsyncAttribute)))
                    {
                        ChannelMessageEventsAsync.Add(methodInfo);
                    }
                }
            });
        }

        public static void RegisterDirectMessageEvents(Type[] types, BindingFlags flags)
        {
            Debug.Log($"Searching for Direct Message Events to Register");
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(DirectMessageEventAttribute)))
                    {
                        DirectMessageEvents.Add(methodInfo);
                    }
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(DirectMessageEventAsyncAttribute)))
                    {
                        DirectMessageEventsAsync.Add(methodInfo);
                    }
                }
            });
        }

        public static void RegisterUserEvents(Type[] types, BindingFlags flags)
        {
            Debug.Log($"Searching for User Events to Register");
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(UserEventAttribute)))
                    {
                        UserEvents.Add(methodInfo);
                    }
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(UserEventAsyncAttribute)))
                    {
                        UserEventsAsync.Add(methodInfo);
                    }
                }
            });
        }

        public static void RegisterUserAudioEvents(Type[] types, BindingFlags flags)
        {
            Debug.Log($"Searching for User Audio Events to Register");
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(UserAudioEventAttribute)))
                    {
                        UserAudioEvents.Add(methodInfo);
                    }
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(UserAudioEventAsyncAttribute)))
                    {
                        UserAudioEventsAsync.Add(methodInfo);
                    }
                }
            });
        }

        public static void RegisterTextToSpeechEvents(Type[] types, BindingFlags flags)
        {
            Debug.Log($"Searching for Text-To-Speech Events to Register");
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(TextToSpeechEventAttribute)))
                    {
                        TextToSpeechEvents.Add(methodInfo);
                    }
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(TextToSpeechEventAsyncAttribute)))
                    {
                        TextToSpeechEventsAsync.Add(methodInfo);
                    }
                }
            });
        }











    }
}
