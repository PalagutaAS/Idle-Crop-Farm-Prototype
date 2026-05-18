using Infrastructure.PersistenceProgress;
using Inventor;
using SavesData;
using UnityEngine;

namespace Infrastructure
{
    public class SavedLoadService : ISavedLoadService
    {
        private readonly IPersistenceProgressService _progressService;
        private readonly IInventoryChanger _inventory;
        private readonly IWallet _wallet;
        private const string GameProgressKey = "GameProgress";

        public SavedLoadService(IPersistenceProgressService progressService, IInventoryChanger inventory, IWallet wallet)
        {
            _progressService = progressService;
            _inventory = inventory;
            _wallet = wallet;
        }

        public void SaveProgress()
        {
            var walletData = new WalletData {Gold = _wallet.Count};
            var inventoryData = new InventoryData
            {
                Wheat = _inventory.CheckCountByType(InventoryType.Wheat),
                Potato = _inventory.CheckCountByType(InventoryType.Potato),
                Corn = _inventory.CheckCountByType(InventoryType.Corn)
            };

            var progress = _progressService.Progress ?? new GameProgress();
            progress.InventoryData = inventoryData;
            progress.WalletData = walletData;

            string json = JsonUtility.ToJson(progress);

            PlayerPrefs.SetString(GameProgressKey, json);
            PlayerPrefs.Save();
        }

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

    public interface ISavedLoadService
    {
        void SaveProgress();
        GameProgress LoadProgress();
    }
}