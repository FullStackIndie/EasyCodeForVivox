using EasyCodeForVivox.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox.Editor
{
    public static class ValidateDynamicEvents
    {

        public static readonly HashSet<string> InternalAssemblyNames = new HashSet<string>()
{
        //"EasyCodeDevelopment",
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


        [MenuItem("EasyCode/Validate Dynamic Events")]
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
                    assemblyName.StartsWith("UnityEngine") || InternalAssemblyNames.Contains(assemblyName) || assemblyName.StartsWith("Mono")
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
                await Task.Run(() => ValidateChannelEvents(types, flags));
                await Task.Run(() => ValidateAudioChannelEvents(types, flags));
                await Task.Run(() => ValidateTextChannelEvents(types, flags));
                await Task.Run(() => ValidateChannelMessageEvents(types, flags));
                await Task.Run(() => ValidateDirectMessageEvents(types, flags));
                await Task.Run(() => ValidateUserEvents(types, flags));
                await Task.Run(() => ValidateUserEvents(types, flags));
                await Task.Run(() => ValidateTextToSpeechEvents(types, flags));
            }


            stopwatch.Stop();
            Debug.Log($"Validating Dynamic Events took [hour:min:sec.ms] {stopwatch.Elapsed}".Color(EasyDebug.Green).Bold());
        }

        private static void CheckType(this MethodInfo methodInfo, Type parameterType)
        {
            var parameters = methodInfo.GetParameters();

            if (parameters.Length == 0 && parameterType != null)
            {
                Debug.Log($"Located in Class {methodInfo.DeclaringType.Name.Color(EasyDebug.Yellow)} : Method {methodInfo.Name.Color(EasyDebug.Yellow).Bold()} does not have valid event parameter of type {parameterType.Name.Color(EasyDebug.Yellow)} \n" +
         $"{methodInfo.ReturnType}   {methodInfo.Name}({string.Join(", ", methodInfo.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))})".Color(EasyDebug.Orange));
                return;
            }

            if (parameters.Length > 1 && parameterType == null)
            {
                Debug.Log($"Located in Class {methodInfo.DeclaringType.Name.Color(EasyDebug.Yellow)} : Method {methodInfo.Name.Color(EasyDebug.Yellow).Bold()} should not have 0 parameters \n" +
        $"{methodInfo.ReturnType}   {methodInfo.Name}({string.Join(", ", methodInfo?.GetParameters()?.Select(p => $"{p.ParameterType.Name} {p.Name}"))})".Color(EasyDebug.Orange));
                return;
            }

            if (parameters.Length > 1)
            {
                Debug.Log($"Located in Class {methodInfo.DeclaringType.Name.Color(EasyDebug.Yellow)} : Method {methodInfo.Name.Color(EasyDebug.Yellow).Bold()} should only have event parameter of type {parameterType.Name.Color(EasyDebug.Yellow)} \n" +
        $"{methodInfo.ReturnType}   {methodInfo.Name}({string.Join(", ", methodInfo?.GetParameters()?.Select(p => $"{p.ParameterType.Name} {p.Name}"))})".Color(EasyDebug.Orange));
                return;
            }

            if (parameters.Length > 0 && parameters[0].ParameterType != parameterType)
            {
                Debug.Log($"Located in Class {methodInfo.DeclaringType.Name.Color(EasyDebug.Yellow)} : Method {methodInfo.Name.Color(EasyDebug.Yellow).Bold()} does not have valid event parameter of type {parameterType.Name.Color(EasyDebug.Yellow)} \n" +
         $"{methodInfo.ReturnType}   {methodInfo.Name}({string.Join(", ", methodInfo.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))})".Color(EasyDebug.Orange));
            }
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
                                methodInfo.CheckType(typeof(ILoginSession));
                                break;
                            case LoginStatus.LoggedIn:
                                methodInfo.CheckType(typeof(ILoginSession));
                                break;
                            case LoginStatus.LoggingOut:
                                methodInfo.CheckType(typeof(ILoginSession));
                                break;
                            case LoginStatus.LoggedOut:
                                methodInfo.CheckType(typeof(ILoginSession));
                                break;
                            case LoginStatus.LoginAdded:
                                methodInfo.CheckType(typeof(AccountId));
                                break;
                            case LoginStatus.LoginRemoved:
                                methodInfo.CheckType(typeof(AccountId));
                                break;
                            case LoginStatus.LoginValuesUpdated:
                                methodInfo.CheckType(typeof(ILoginSession));
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
                                methodInfo.CheckType(typeof(ILoginSession));
                                break;
                            case LoginStatus.LoggedIn:
                                methodInfo.CheckType(typeof(ILoginSession));
                                break;
                            case LoginStatus.LoggingOut:
                                methodInfo.CheckType(typeof(ILoginSession));
                                break;
                            case LoginStatus.LoggedOut:
                                methodInfo.CheckType(typeof(ILoginSession));
                                break;
                            case LoginStatus.LoginAdded:
                                methodInfo.CheckType(typeof(AccountId));
                                break;
                            case LoginStatus.LoginRemoved:
                                methodInfo.CheckType(typeof(AccountId));
                                break;
                            case LoginStatus.LoginValuesUpdated:
                                methodInfo.CheckType(typeof(ILoginSession));
                                break;
                        }
                    }
                }
            });
        }

        public static void ValidateChannelEvents(Type[] types, BindingFlags flags)
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
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case ChannelStatus.ChannelConnected:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case ChannelStatus.ChannelDisconnecting:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case ChannelStatus.ChannelDisconnected:
                                methodInfo.CheckType(typeof(IChannelSession));
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
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case ChannelStatus.ChannelConnected:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case ChannelStatus.ChannelDisconnecting:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case ChannelStatus.ChannelDisconnected:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                        }
                    }
                }
            });
        }

        public static void ValidateAudioDeviceEvents(Type[] types, BindingFlags flags)
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
                                methodInfo.CheckType(typeof(IAudioDevice));
                                break;
                            case AudioDeviceStatus.AudioInputDeviceRemoved:
                                methodInfo.CheckType(typeof(IAudioDevice));
                                break;
                            case AudioDeviceStatus.AudioInputDeviceUpdated:
                                methodInfo.CheckType(typeof(IAudioDevice));
                                break;
                            case AudioDeviceStatus.AudioOutputDeviceAdded:
                                methodInfo.CheckType(typeof(IAudioDevice));
                                break;
                            case AudioDeviceStatus.AudioOutputDeviceRemoved:
                                methodInfo.CheckType(typeof(IAudioDevice));
                                break;
                            case AudioDeviceStatus.AudioOutputDeviceUpdated:
                                methodInfo.CheckType(typeof(IAudioDevice));
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
                                methodInfo.CheckType(typeof(IAudioDevice));
                                break;
                            case AudioDeviceStatus.AudioInputDeviceRemoved:
                                methodInfo.CheckType(typeof(IAudioDevice));
                                break;
                            case AudioDeviceStatus.AudioInputDeviceUpdated:
                                methodInfo.CheckType(typeof(IAudioDevice));
                                break;
                            case AudioDeviceStatus.AudioOutputDeviceAdded:
                                methodInfo.CheckType(typeof(IAudioDevice));
                                break;
                            case AudioDeviceStatus.AudioOutputDeviceRemoved:
                                methodInfo.CheckType(typeof(IAudioDevice));
                                break;
                            case AudioDeviceStatus.AudioOutputDeviceUpdated:
                                methodInfo.CheckType(typeof(IAudioDevice));
                                break;
                        }
                    }
                }
            });
        }

        public static void ValidateAudioChannelEvents(Type[] types, BindingFlags flags)
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
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case AudioChannelStatus.AudioChannelConnected:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case AudioChannelStatus.AudioChannelDisconnecting:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case AudioChannelStatus.AudioChannelDisconnected:
                                methodInfo.CheckType(typeof(IChannelSession));
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
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case AudioChannelStatus.AudioChannelConnected:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case AudioChannelStatus.AudioChannelDisconnecting:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case AudioChannelStatus.AudioChannelDisconnected:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                        }
                    }
                }
            });
        }

        public static void ValidateTextChannelEvents(Type[] types, BindingFlags flags)
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
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case TextChannelStatus.TextChannelConnected:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case TextChannelStatus.TextChannelDisconnecting:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case TextChannelStatus.TextChannelDisconnected:
                                methodInfo.CheckType(typeof(IChannelSession));
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
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case TextChannelStatus.TextChannelConnected:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case TextChannelStatus.TextChannelDisconnecting:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                            case TextChannelStatus.TextChannelDisconnected:
                                methodInfo.CheckType(typeof(IChannelSession));
                                break;
                        }
                    }
                }
            });
        }

        public static void ValidateChannelMessageEvents(Type[] types, BindingFlags flags)
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
                                methodInfo.CheckType(null); // todo test
                                break;
                            case ChannelMessageStatus.ChannelMessageRecieved:
                                methodInfo.CheckType(typeof(IChannelTextMessage));
                                break;
                            case ChannelMessageStatus.EventMessageRecieved:
                                methodInfo.CheckType(typeof(IChannelTextMessage));
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
                                methodInfo.CheckType(null); // todo test
                                break;
                            case ChannelMessageStatus.ChannelMessageRecieved:
                                methodInfo.CheckType(typeof(IChannelTextMessage));
                                break;
                            case ChannelMessageStatus.EventMessageRecieved:
                                methodInfo.CheckType(typeof(IChannelTextMessage));
                                break;
                        }
                    }
                }
            });
        }

        public static void ValidateDirectMessageEvents(Type[] types, BindingFlags flags)
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
                                methodInfo.CheckType(null); // todo test
                                break;
                            case DirectMessageStatus.DirectMessageRecieved:
                                methodInfo.CheckType(typeof(IDirectedTextMessage));
                                break;
                            case DirectMessageStatus.DirectMessageFailed:
                                methodInfo.CheckType(typeof(IFailedDirectedTextMessage));
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
                                methodInfo.CheckType(null); // todo test
                                break;
                            case DirectMessageStatus.DirectMessageRecieved:
                                methodInfo.CheckType(typeof(IDirectedTextMessage));
                                break;
                            case DirectMessageStatus.DirectMessageFailed:
                                methodInfo.CheckType(typeof(IFailedDirectedTextMessage));
                                break;
                        }
                    }
                }
            });
        }

        public static void ValidateUserEvents(Type[] types, BindingFlags flags)
        {
            Parallel.ForEach(types, type =>
            {
                foreach (MethodInfo methodInfo in type.GetMethods(flags))
                {
                    var attribute = methodInfo.GetCustomAttribute<UserEventsAttribute>();
                    if (attribute != null)
                    {
                        switch (attribute.Options)
                        {
                            case UserStatus.UserLeftChannel:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.UserJoinedChannel:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.UserValuesUpdated:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.UserMuted:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.UserUnmuted:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.UserCrossMuted:
                                methodInfo.CheckType(typeof(AccountId));
                                break;
                            case UserStatus.UserCrossUnmuted:
                                methodInfo.CheckType(typeof(AccountId));
                                break;
                            case UserStatus.UserNotSpeaking:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.UserSpeaking:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.LocalUserMuted:
                                methodInfo.CheckType(null); // todo test
                                break;
                            case UserStatus.LocalUserUnmuted:
                                methodInfo.CheckType(null); // todo test
                                break;
                        }
                        continue;
                    }
                    var asyncAttribute = methodInfo.GetCustomAttribute<UserEventsAsyncAttribute>();
                    if (asyncAttribute != null)
                    {
                        switch (asyncAttribute.Options)
                        {
                            case UserStatus.UserLeftChannel:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.UserJoinedChannel:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.UserValuesUpdated:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.UserMuted:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.UserUnmuted:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.UserCrossMuted:
                                methodInfo.CheckType(typeof(AccountId));
                                break;
                            case UserStatus.UserCrossUnmuted:
                                methodInfo.CheckType(typeof(AccountId));
                                break;
                            case UserStatus.UserNotSpeaking:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.UserSpeaking:
                                methodInfo.CheckType(typeof(IParticipant));
                                break;
                            case UserStatus.LocalUserMuted:
                                methodInfo.CheckType(null); // todo test
                                break;
                            case UserStatus.LocalUserUnmuted:
                                methodInfo.CheckType(null); // todo test
                                break;
                        }
                    }
                }
            });
        }

        public static void ValidateTextToSpeechEvents(Type[] types, BindingFlags flags)
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
                                methodInfo.CheckType(typeof(ITTSMessageQueueEventArgs));
                                break;
                            case TextToSpeechStatus.TTSMessageRemoved:
                                methodInfo.CheckType(typeof(ITTSMessageQueueEventArgs));
                                break;
                            case TextToSpeechStatus.TTSMessageUpdated:
                                methodInfo.CheckType(typeof(ITTSMessageQueueEventArgs));
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
                                methodInfo.CheckType(typeof(ITTSMessageQueueEventArgs));
                                break;
                            case TextToSpeechStatus.TTSMessageRemoved:
                                methodInfo.CheckType(typeof(ITTSMessageQueueEventArgs));
                                break;
                            case TextToSpeechStatus.TTSMessageUpdated:
                                methodInfo.CheckType(typeof(ITTSMessageQueueEventArgs));
                                break;
                        }
                    }
                }
            });
        }






    }
}

#endif