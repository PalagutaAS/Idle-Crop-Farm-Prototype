using System.Collections.Generic;
using System.Linq;
using Inventor;
using UnityEngine;
using VContainer;
using Wallets;

namespace UI
{
    public class PrintValues : MonoBehaviour
    {
        private Dictionary<CropType, PrintCrop> _cropPrintDict = new();
        private Dictionary<MoneyType, PrintMoney> _moneyPrintDict = new();

        [Inject]
        private void Constructor(IInventory inventory, IWallet wallet)
        {
            _cropPrintDict = GetComponentsInChildren<PrintCrop>()
                .ToDictionary(pc => pc.Type, pc => pc);
            _moneyPrintDict = GetComponentsInChildren<PrintMoney>()
                .ToDictionary(pm => pm.Type, pm => pm);

            inventory.OnChangedByTypeForUI += OnCropChanged;
            wallet.OnChangedByTypeForUI += OnMoneyChanged;

            foreach (var kvp in _cropPrintDict)
            {
                int count = inventory.CheckCountByType(kvp.Key);
                kvp.Value.Print(count);
            }
            foreach (var kvp in _moneyPrintDict)
            {
                int count = wallet.CheckCountByType(kvp.Key);
                kvp.Value.Print(count);
            }
        }

        private void OnCropChanged(CropType type, int count)
        {
            if (_cropPrintDict.TryGetValue(type, out var printCrop))
                printCrop.Print(count);
        }

        private void OnMoneyChanged(MoneyType type, int count)
        {
            if (_moneyPrintDict.TryGetValue(type, out var printMoney))
                printMoney.Print(count);
        }
    }
}