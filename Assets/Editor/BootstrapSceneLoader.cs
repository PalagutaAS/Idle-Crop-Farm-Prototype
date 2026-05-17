using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class BootstrapSceneLoader
{
    private const string BootstrapScenePathKey = "BootstrapScenePath";

    static BootstrapSceneLoader()
    {
        string savedPath = EditorPrefs.GetString(BootstrapScenePathKey, "");
        if (!string.IsNullOrEmpty(savedPath))
        {
            SceneAsset bootstrapScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(savedPath);
            if (bootstrapScene != null)
            {
                EditorSceneManager.playModeStartScene = bootstrapScene;
                Debug.Log($"[BootstrapLoader] Play Mode Start Scene установлена: {savedPath}");
            }
            else
            {
                Debug.LogWarning($"[BootstrapLoader] Сцена по пути '{savedPath}' не найдена. Укажите сцену заново через меню Tools.");
            }
        }
    }

    /// <summary>
    /// Пункт меню: запоминает выделенную в Project-окне сцену как Bootstrap-сцену.
    /// </summary>
    [MenuItem("Tools/Set Bootstrap Scene")]
    private static void SetBootstrapScene()
    {
        SceneAsset selectedScene = Selection.activeObject as SceneAsset;
        if (selectedScene == null)
        {
            Debug.LogError("[BootstrapLoader] Выделите файл сцены в окне Project и повторите команду.");
            return;
        }

        string path = AssetDatabase.GetAssetPath(selectedScene);
        EditorPrefs.SetString(BootstrapScenePathKey, path);
        EditorSceneManager.playModeStartScene = selectedScene;
        Debug.Log($"[BootstrapLoader] Bootstrap-сцена установлена: {path}");
    }
}