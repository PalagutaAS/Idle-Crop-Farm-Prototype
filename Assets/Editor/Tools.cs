using Infrastructure;
using UnityEditor;
using UnityEngine;
using VContainer;
using VContainer.Unity;

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
        var scope = Object.FindObjectOfType<LifetimeScope>();
        if (scope != null)
        {
            ISavedLoadService saveService = scope.Container.Resolve<ISavedLoadService>();
            saveService.SaveProgress();
            Debug.Log("Progress saved via ISavedLoadService.");
        }
        else
        {
            Debug.LogWarning("No LifetimeScope found. Fallback to PlayerPrefs.Save()");
            PlayerPrefs.Save();
        }
    }
}
