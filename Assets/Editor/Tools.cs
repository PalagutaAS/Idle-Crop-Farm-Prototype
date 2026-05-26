#if UNITY_EDITOR
using Infrastructure.DI;
using Infrastructure.Services;
using Infrastructure.StateMachine;
using UnityEditor;
using UnityEngine;
using VContainer;

public class Tools 
{
    [MenuItem("Tools/Clear player prefs")]
    public static void ClearPrefs()
    {
        var scope = Object.FindObjectOfType<GameLifetimeScope>();
        scope.Container.TryResolve<IResetSaveService>(out var resetSaveService);
        resetSaveService.ResetSave();
        ResetGame();
    }
    
    [MenuItem("Tools/Reset Game")]
    public static void ResetGame()
    {
        var scope = Object.FindObjectOfType<GameLifetimeScope>();
        scope.Container.TryResolve<IRestartGameService>(out var stateSwitcher);
        stateSwitcher.DoRestartGame(); 
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
