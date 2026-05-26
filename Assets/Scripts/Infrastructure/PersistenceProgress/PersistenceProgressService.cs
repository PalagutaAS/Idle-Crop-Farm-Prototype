using Logging;
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
            string jsonSaves = (Progress == null) ? "NULL" : JsonUtility.ToJson(Progress);
            YG2.saves.progress = JsonUtility.ToJson(Progress);
            this.Log($"Progress saved: {jsonSaves}");
            YG2.SaveProgress();
        }
    }

    public interface IPersistenceProgressService
    {
        public GameProgress Progress { get; set; }
        public void SaveCloudYG();
    }
}