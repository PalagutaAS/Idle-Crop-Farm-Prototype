using Fields;
using Infrastructure.PersistenceProgress;
using Inventor;
using SavesData;
using UnityEngine;

namespace Infrastructure.Services
{
    public class SaveService : BaseSaveService, ISaveService
    {
        private readonly IPersistenceProgressService _progressService;
        private readonly IInventoryChanger _inventory;
        private readonly IWallet _wallet;
        private readonly IFieldService _fieldService;
        
        public SaveService(IPersistenceProgressService progressService, IInventoryChanger inventory, IWallet wallet, IFieldService fieldService)
        {
            _progressService = progressService;
            _inventory = inventory;
            _wallet = wallet;
            _fieldService = fieldService;
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
            
            var activeCrops = _fieldService.GetActiveFieldCountPerCropType();

            var fieldData = new FieldsData
            {
                Corn = activeCrops.TryGetValue(CropType.Corn, out int cornCount) ? cornCount : 0,
                Potato = activeCrops.TryGetValue(CropType.Potato, out int potatoCount) ? potatoCount : 0,
                Wheat = activeCrops.TryGetValue(CropType.Wheat, out int wheatCount) ? wheatCount : 0
            };
            

            var progress = _progressService.Progress ?? new GameProgress();
            progress.InventoryData = inventoryData;
            progress.WalletData = walletData;
            progress.FieldData = fieldData;

            string json = JsonUtility.ToJson(progress);

            PlayerPrefs.SetString(GameProgressKey, json);
            PlayerPrefs.Save();
        }
    }

    public interface ISaveService
    {
        void SaveProgress();

    }
}