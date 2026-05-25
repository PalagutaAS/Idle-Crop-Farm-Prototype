using SavesData;
using UnityEngine;
using YG;

namespace Infrastructure.PersistenceProgress
{
    public class PersistenceProgressService : IPersistenceProgressService
    {
        public GameProgress Progress { get; set; }
        
        public void SaveCloudYG()
        {
            YG2.saves.progress = JsonUtility.ToJson(Progress);
            Debug.Log($"Progress saved: {YG2.saves.progress}");
            YG2.SaveProgress();
        }
        
        /// <summary>
        /// ToDO move clear to new class
        /// </summary>
        public void ResetCloudYG()
        {
            YG2.saves.progress = "";
            YG2.SaveProgress();
            Debug.Log("Progress save reset");
        }
    }

    public interface IPersistenceProgressService
    {
        public GameProgress Progress { get; set; }
        public void SaveCloudYG();
        public void ResetCloudYG();
    }
}