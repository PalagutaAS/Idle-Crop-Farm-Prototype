using System;
using System.Collections.Generic;
using Fields;
using Infrastructure.PersistenceProgress;
using Inventor;
using SavesData;
using Tools.Interface;
using UnityEngine;

namespace Infrastructure.Services
{
    public class SaveService : BaseSaveService, ISaveService
    {
        private readonly IPersistenceProgressService _progressService;
        private readonly IInventoryChanger _inventory;
        private readonly IWallet _wallet;
        private readonly IFieldService _fieldService;
        private readonly IToolManager _toolManager;

        public SaveService(IPersistenceProgressService progressService, IInventoryChanger inventory, IWallet wallet, IFieldService fieldService, IToolManager toolManager)
        {
            _progressService = progressService;
            _inventory = inventory;
            _wallet = wallet;
            _fieldService = fieldService;
            _toolManager = toolManager;
        }

        public void SaveProgress()
        {
            var walletData = new WalletData();
            var fieldData = new FieldsData();
            var inventoryData = new InventoryData();
            var toolsData = new ToolsData();
            
            
            Array cropEnums = Enum.GetValues(typeof(CropType));
            Array toolEnums = Enum.GetValues(typeof(ToolType));

            walletData.Money[MoneyType.Coin] = _wallet.Count;
            foreach (CropType crop in cropEnums)
            {
                if (crop == CropType.None)
                    continue;
                int cropAmount = _inventory.CheckCountByType(crop);
                
                if (cropAmount > 0)
                    inventoryData.Crops[crop] = cropAmount;
                
                int fieldAmount = _fieldService.GetActiveFieldCountPerCropType().
                    TryGetValue(crop, out int cornCount) ? cornCount : 0;
                
                if (fieldAmount > 0)
                    fieldData.Fields[crop] = fieldAmount;
            }

            Dictionary<ToolType, int> toolsDict = new();
            foreach (ITool tool in _toolManager.GetAllTools())
                toolsDict.Add(tool.Type, tool.CurrentLevel);

            foreach (ToolType tool in toolEnums)
            {
                toolsData.Tools[tool] = toolsDict.TryGetValue(tool, out int toolLevel) ? toolLevel : 0;
            }

            var progress = _progressService.Progress ?? new GameProgress();
            progress.InventoryData = inventoryData;
            progress.WalletData = walletData;
            progress.FieldData = fieldData;
            progress.ToolsData = toolsData;

            string json = JsonUtility.ToJson(progress);

            PlayerPrefs.SetString(GameProgressKey, json);
            PlayerPrefs.Save();
            Debug.Log($"Progress saved: {json}");
        }
    }

    public interface ISaveService
    {
        void SaveProgress();

    }
}