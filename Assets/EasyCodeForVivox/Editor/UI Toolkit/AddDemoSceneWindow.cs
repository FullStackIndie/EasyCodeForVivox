using EasyCodeForVivox.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class AddDemoSceneWindow : EditorWindow
{
    private static string key = "EasyCodeForVivox:Setup";
    private static string[] demoSceneList = new string[]
         {
            "Assets/EasyCodeForVivox/Demo Scenes/3D Demo Scenes/3D Demo Scene.unity",
            "Assets/EasyCodeForVivox/Demo Scenes/3D Demo Scenes/Lobby.unity",
            "Assets/EasyCodeForVivox/Demo Scenes/Chat Demo Scenes/Chat Scene.unity"
         };


    private List<SceneAsset> _sceneAssets = new List<SceneAsset>();
    private Button addDemoScenesButton;
    private Button closeWindowButton;
    private Toggle dontShowToggle;

    [InitializeOnLoadMethod]
    private static void Validate()
    {
        if (DontShowWindowAgain()) { return; }
        if (!CheckIfDemoScenesExist())
        {
            ShowExample();
        }
    }

    [MenuItem("EasyCode/Add Demo Scenes To Build Settings")]
    public static void ShowExample()
    {
        if (CheckIfDemoScenesExist())
        {
            Debug.Log("Demo scenes are already in build settings".Color(EasyDebug.Yellow));
        }
        AddDemoSceneWindow window = GetWindow<AddDemoSceneWindow>();
        window.titleContent = new GUIContent("AddDemoScenes");
        window.maxSize = new Vector2(1000f, 500f);
        window.Repaint();
    }

    private void OnEnable()
    {
        for (int i = 0; i < demoSceneList.Length; i++)
        {
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(demoSceneList[i]);
            _sceneAssets.Add(sceneAsset);
        }
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/EasyCodeForVivox/Editor/UI Toolkit/AddDemoSceneWindow.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/EasyCodeForVivox/Editor/UI Toolkit/AddDemoSceneWindow.uss");

        addDemoScenesButton = root.Q<Button>("add-demo-scenes");
        closeWindowButton = root.Q<Button>("close-window");
        dontShowToggle = root.Q<Toggle>("dont-show-again");
        dontShowToggle.value = DontShowWindowAgain();

        addDemoScenesButton.clicked += AddDemoScenesToBuildSettings;
        closeWindowButton.clicked += () => { Close(); };
    }

    void AddDemoScenesToBuildSettings()
    {
        AddDemoScenes();
        if(dontShowToggle.value == true)
        {
            SaveSettings();
        }
        Close();
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

        if (allScenes == demoSceneList.Length)
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
}