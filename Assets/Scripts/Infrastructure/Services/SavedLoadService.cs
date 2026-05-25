using SavesData;
using UnityEngine;
using YG;

namespace Infrastructure
{
    public class SavedLoadService : ISavedLoadService
    {
        public GameProgress LoadProgress()
        {
            GameProgress progress = null;
            string jsonProgress = YG2.saves.progress;
            if (jsonProgress != "")
            {
                progress = JsonUtility.FromJson<GameProgress>(jsonProgress);
                Debug.Log($"Successfully load save: {jsonProgress}");
            }

            return progress;
        }
    }

    public interface ISavedLoadService
    {
        GameProgress LoadProgress();
    }
}