using System;
using System.Collections.Generic;

namespace Inventor
{
    public class Inventory : IValueSource, IInventoryChanger, IInventory 
    {
        private readonly Dictionary<CropType, int> _dictionary;
        
        public event Action<InventoryType, int> OnChangedByType;
        
        public Inventory()
        {
            _dictionary = new Dictionary<CropType, int>();
        }

        public bool Remove(CropType type, int count)
        {
            if (!HasEnoughByCrop(type, count)) return false;

            _dictionary[type] -= count;
            OnChangedByType?.Invoke((InventoryType)type, _dictionary[type]);
            return true;
        }
        
        public void Add(CropType type, int count)
        {
            if (!_dictionary.ContainsKey(type))
            {
                _dictionary.Add(type, 0);
            }

            _dictionary[type] += count;
            OnChangedByType?.Invoke((InventoryType)type, _dictionary[type]);
        }

        public bool HasEnoughByCrop(CropType type, int count) => count <= CheckCountByType((InventoryType)type);
        public int CheckCountByType(InventoryType type) => _dictionary.ContainsKey((CropType)type) ? _dictionary[(CropType)type]: 0;
    }
}
