using System;
using System.Collections.Generic;

namespace Inventor
{
    public class Inventory : IInventoryChanger
    {
        private readonly Dictionary<CropType, int> _dictionary;
        
        public event Action<CropType, int> OnChangedByTypeForUI;
        
        public Inventory()
        {
            _dictionary = new Dictionary<CropType, int>();
        }

        public bool Remove(CropType type, int count)
        {
            if (!HasEnoughByCrop(type, count)) return false;

            _dictionary[type] -= count;
            OnChangedByTypeForUI?.Invoke(type, _dictionary[type]);
            return true;
        }
        
        public void Add(CropType type, int count)
        {
            if (!_dictionary.ContainsKey(type))
            {
                _dictionary.Add(type, 0);
            }

            _dictionary[type] += count;
            OnChangedByTypeForUI?.Invoke(type, _dictionary[type]);
        }

        public bool HasEnoughByCrop(CropType type, int count) => count <= CheckCountByType(type);
        public int CheckCountByType(CropType type) => _dictionary.ContainsKey(type) ? _dictionary[type]: 0;
    }
}
