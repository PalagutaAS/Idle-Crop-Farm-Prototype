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
                string json = PlayerPrefs.GetString(GameProgressKey);
                progress = JsonUtility.FromJson<GameProgress>(json);
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