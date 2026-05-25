using SavesData;
using UnityEngine;

namespace Infrastructure
{
    public class SavedLoadService : BaseSaveService, ISavedLoadService
    {
        public GameProgress LoadProgress()
        {
            GameProgress progress = null;
            if (PlayerPrefs.HasKey(GameProgressKey))
            {
                string jsonProgress = PlayerPrefs.GetString(GameProgressKey);
                Debug.Log($"Successfully load save: {jsonProgress}");
                progress = JsonUtility.FromJson<GameProgress>(jsonProgress);
            }

            return progress;
        }
    }

    public abstract class BaseSaveService
    {
        public const string GameProgressKey = "GameProgress";
    }

    public interface ISavedLoadService
    {
        GameProgress LoadProgress();
    }
}