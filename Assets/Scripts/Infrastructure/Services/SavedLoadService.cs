using Infrastructure.PersistenceProgress;
using Inventor;
using SavesData;
using UnityEngine;
using VContainer.Unity;

namespace Infrastructure
{
    public class SavedLoadService : ISavedLoadService, IStartable
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
            // 1. Получаем текущие данные
            var inventoryData = new InventoryData
            {
                Gold = _wallet.Count,
                Wheat = _inventory.CheckCountByType(InventoryType.Wheat),
                Potato = _inventory.CheckCountByType(InventoryType.Potato),
                Corn = _inventory.CheckCountByType(InventoryType.Corn)
            };

            // 2. Заполняем GameProgress
            var progress = _progressService.Progress ?? new GameProgress();
            progress.InventoryData = inventoryData;

            // 3. Сериализуем в JSON
            string json = JsonUtility.ToJson(progress);

            // 4. Сохраняем в PlayerPrefs
            PlayerPrefs.SetString(GameProgressKey, json);
            PlayerPrefs.Save();

        }

        public GameProgress LoadProgress()
        {
            return NewGameProgress();
        }
        
        private GameProgress NewGameProgress() => new GameProgress();

        public void Start()
        {
            SaveProgress();
        }
    }

    public interface ISavedLoadService
    {
        void SaveProgress();
        GameProgress LoadProgress();
    }
}