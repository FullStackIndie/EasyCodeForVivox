using EasyCodeForVivox.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
#endif

public class EasySetupWindow : EditorWindow
{
#if UNITY_EDITOR
    static ReorderableList reorderableList = null;
    static EasySetupWindow Window;
#endif

    static string[] demoSceneList = new string[]
        {
            "Assets/EasyCodeForVivox/Demo Scenes/3D Demo Scenes/3D Demo Scene.unity",
            "Assets/EasyCodeForVivox/Demo Scenes/3D Demo Scenes/Lobby.unity",
            "Assets/EasyCodeForVivox/Demo Scenes/Chat Demo Scenes/Chat Scene.unity"
        };

    List<SceneAsset> _sceneAssets = new List<SceneAsset>();
    public static string key = "EasyCodeForVivox:Setup";

#if UNITY_EDITOR
    [MenuItem("EasyCode/Add Demo Scenes To Build Settings")]
    public static void ShowWindow()
    {
       Window = GetWindow<EasySetupWindow>(false, title: "EasyCode Demo Scenes", focus: true);
    }

    [InitializeOnLoadMethod]
    private static void OnValidate()
    {
        if (DontShowWindowAgain()) { return; }
        if (!CheckIfDemoScenesExist())
        {
            var window = GetWindow<EasySetupWindow>(false, title: "EasyCode Demo Scenes", focus: true);
            window.maxSize = new Vector2(500f, 500f);
            window.Repaint();
            window.Show();
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < demoSceneList.Length; i++)
        {
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(demoSceneList[i]);
            _sceneAssets.Add(sceneAsset);
        }

        reorderableList = new ReorderableList(_sceneAssets, typeof(List<SceneAsset>), draggable: true, displayHeader: true, displayAddButton: true, displayRemoveButton: true);
        reorderableList.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, "Welcome to EasyCodeForVivox - Add Demo Scenes To Build Settings");
            EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
        };
        reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            rect.y += 2f;
            rect.height = EditorGUIUtility.singleLineHeight;
            GUIContent objectabel = new GUIContent($"Demo Scene {index}");
            EditorGUILayout.ObjectField(objectabel, _sceneAssets[index], typeof(SceneAsset), true);
        };
    }

    private void OnGUI()
    {
        reorderableList.DoList(new Rect(Vector2.zero, Vector2.one * 500));
        GUILayout.Space(50f);

        if (GUILayout.Button("Add Demo Scenes To Build Settings"))
        {
            AddDemoScenes();
            SaveSettings();
            Window.Close();
        }
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }

    public static void AddDemoScenes()
    {
        List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
        var path = $"{Directory.GetCurrentDirectory()}/Assets/EasyCodeForVivox/Demo Scenes/";
        DirectoryInfo directories = new DirectoryInfo(path);

        foreach (var directory in directories.GetDirectories())
        {
            FileInfo[] scenes = directory.GetFiles("*.unity");
            foreach (var scene in scenes)
            {
                var scenePath = $"Assets/EasyCodeForVivox/Demo Scenes/{directory.Name}/{scene.Name}";
                if (!EditorBuildSettings.scenes.Any(s => s.path == scenePath))
                {
                    if (!string.IsNullOrEmpty(scenePath))
                    {
                        Debug.Log($"Added {scenePath} to Build Settings".Color(EasyDebug.Green));
                        editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
                    }
                }
            }
        }

        if (editorBuildSettingsScenes.Count > 0)
        {
            EditorBuildSettings.scenes = editorBuildSettingsScenes.Distinct().ToArray();
            Debug.Log($"Added Demo Scenes to Build Settings".Color(EasyDebug.Green));
        }
    }

    public static bool CheckIfDemoScenesExist()
    {
        int allScenes = 0;
        foreach (var demoScene in demoSceneList)
        {
            if (EditorBuildSettings.scenes.Any(s => s.path.Contains(demoScene))) { allScenes++; }
        }

        if (allScenes > 0)
        {
            return true;
        }

        return false;
    }

    private static void SaveSettings()
    {
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }

    private static bool DontShowWindowAgain()
    {
        int dontShow = PlayerPrefs.GetInt(key);
        if (dontShow == 1)
        {
            return true;
        }
        return false;
    }

#endif
}