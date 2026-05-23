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

            Dictionary<ToolType, int> toolsDict = new();
            foreach (ITool tool in _toolManager.GetAllTools())
                toolsDict.Add(tool.Type, tool.CurrentLevel);

            var toolsData = new ToolsData
            {
                Shovel = toolsDict.ContainsKey(ToolType.Shovel) ? toolsDict[ToolType.Shovel] : 0,
                Scythe = toolsDict.ContainsKey(ToolType.Scythe) ? toolsDict[ToolType.Scythe] : 0,
            };
            
            var progress = _progressService.Progress ?? new GameProgress();
            progress.InventoryData = inventoryData;
            progress.WalletData = walletData;
            progress.FieldData = fieldData;
            progress.ToolsData = toolsData;

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