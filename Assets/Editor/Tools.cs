using Infrastructure.DI;
using Infrastructure.Services;
using UnityEditor;
using UnityEngine;
using VContainer;

public class Tools 
{
    [MenuItem("Tools/Clear player prefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Progress save reset");
    }    
    
    [MenuItem("Tools/Save player prefs")]
    public static void SavePrefs()
    {
        var scope = Object.FindObjectOfType<GameLifetimeScope>();
        if (scope != null)
        {
            scope.Container.TryResolve<ISaveService>(out var saveService);
            if (saveService == null)
            {
                Debug.LogWarning("No ISaveService found.");
                return;
            }
            saveService.SaveProgress();
        }
        else
        {
            Debug.LogWarning("No LifetimeScope found. Fallback to PlayerPrefs.Save()");
            PlayerPrefs.Save();
        }
    }
}
