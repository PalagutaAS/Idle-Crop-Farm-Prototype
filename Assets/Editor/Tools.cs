#if UNITY_EDITOR
using Infrastructure.DI;
using Infrastructure.Services;
using UnityEditor;
using UnityEngine;
using VContainer;
using YG;

public class Tools 
{
    [MenuItem("Tools/Clear player prefs")]
    public static void ClearPrefs()
    {
        YG2.saves.progress = "";
        YG2.SaveProgress();
        Debug.Log("Progress save reset");
    }    
    
    [MenuItem("Tools/Save player prefs")]
    public static void SavePrefs()
    {
        var scope = Object.FindObjectOfType<GameLifetimeScope>();
        if (scope == null)
        {
            Debug.LogWarning("No LifetimeScope found.");
            return;
        }
        
        scope.Container.TryResolve<ISaveService>(out var saveService);
        if (saveService == null)
        {
            Debug.LogWarning("No ISaveService found.");
            return;
        }
        saveService.SaveProgress();
    }
}
#endif
