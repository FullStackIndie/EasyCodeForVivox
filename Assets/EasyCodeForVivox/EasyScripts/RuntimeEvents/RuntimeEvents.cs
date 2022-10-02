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
    "AssetStoreTools ",
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

        public static List<MethodInfo> LoginMethods = new List<MethodInfo>();
        public static List<MethodInfo> ChannelMethods = new List<MethodInfo>();
        public static List<MethodInfo> AudioChannelMethods = new List<MethodInfo>();
        public static List<MethodInfo> TextChannelMethods = new List<MethodInfo>();

        public static List<MethodInfo> ChannelMessageMethods = new List<MethodInfo>();
        public static List<MethodInfo> DirectMessageMethods = new List<MethodInfo>();
        public static List<MethodInfo> UserEventMethods = new List<MethodInfo>();
        public static List<MethodInfo> UserAudioEventMethods = new List<MethodInfo>();
        public static List<MethodInfo> TextToSpeechMethods = new List<MethodInfo>();

        public static async Task RegisterEvents()
        {
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                var assemblyName = assembly.GetName().Name;
                //if (assembly.IsDynamic) { continue; }
                if (assemblyName.StartsWith("System") || assemblyName.StartsWith("Unity") || assemblyName.StartsWith("UnityEditor") ||
                    assemblyName.StartsWith("UnityEngine") || internalAssemblyNames.Contains(assemblyName) || assemblyName.StartsWith("Mono"))
                {
                    //continue;
                }
                Debug.Log($"Searching Assembly [ {assembly.GetName().Name} ]");

                Type[] types = assembly.GetTypes();
                await Task.Run(() => RegisterLoginMethods(types));
                await Task.Run(() => RegisterChannelMethods(types));
                await Task.Run(() => RegisterVoiceChannelMethods(types));
                await Task.Run(() => RegisterTextChannelMethods(types));
            }

            LogRegisteredEventsCount();

            stopwatch.Stop();
            Debug.Log($"Registering Events for {nameof(RegisterEvents)} took {stopwatch.Elapsed}");
        }

        public static void LogRegisteredEventsCount()
        {
            Debug.Log($"Found {LoginMethods.Count} Login Event Methods");
            Debug.Log($"Found {ChannelMethods.Count} Channel Event Methods");
            Debug.Log($"Found {AudioChannelMethods.Count} Voice Channel Event Methods");
            Debug.Log($"Found {TextChannelMethods.Count} Text Channel Event Methods");
        }

        public static void RegisterLoginMethods(Type[] types)
        {
            Debug.Log($"Searching for Login Methods to Register");
            Parallel.ForEach(types, type =>
            {
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    var attribute = methodInfo.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(LoginEventAttribute));
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(LoginEventAttribute)))
                    {
                        LoginMethods.Add(methodInfo);
                    }
                }
            });
        }

        public static void RegisterChannelMethods(Type[] types)
        {
            Debug.Log($"Searching for Channel Methods to Register");
            Parallel.ForEach(types, type =>
            {
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(ChannelEventAttribute)))
                    {
                        ChannelMethods.Add(methodInfo);
                    }
                }
            });
        }

        public static void RegisterVoiceChannelMethods(Type[] types)
        {
            Debug.Log($"Searching for Voice Channel Methods to Register");
            Parallel.ForEach(types, type =>
            {
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(AudioChannelEventAttribute)))
                    {
                        ChannelMethods.Add(methodInfo);
                    }
                }
            });
        }

        public static void RegisterTextChannelMethods(Type[] types)
        {
            Debug.Log($"Searching for Text Channel Methods to Register");
            Parallel.ForEach(types, type =>
            {
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    if (methodInfo.CustomAttributes.Any(a => a.AttributeType == typeof(TextChannelEventAttribute)))
                    {
                        ChannelMethods.Add(methodInfo);
                    }
                }
            });
        }

    }
}
