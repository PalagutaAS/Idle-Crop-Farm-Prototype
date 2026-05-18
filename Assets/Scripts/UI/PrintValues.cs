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
        private Dictionary<InventoryType, PrintCount> _dictionaryPrintCount = new();
        
        [Inject]
        private void Constructor(Inventory inventory, Wallet wallet)
        {
            _dictionaryPrintCount = GetComponentsInChildren<PrintCount>()
                .ToDictionary(pc => pc.GetSupportType, pc => pc);
            IValueSource[] valueSources = {inventory, wallet};
            
            foreach (var valueSource in valueSources)
            {
                valueSource.OnChangedByTypeForUI += SendToPrint;
                foreach (var item in _dictionaryPrintCount)
                {
                    var type = item.Value.GetSupportType;
                    SendToPrint(type, valueSource.CheckCountByType(type));
                }
            }
        }

        private void SendToPrint(InventoryType type, int count)
        {
            if (_dictionaryPrintCount.ContainsKey(type))
            {
                _dictionaryPrintCount[type].Print(count);
            }
        }
    }
}