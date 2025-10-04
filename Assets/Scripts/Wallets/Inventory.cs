using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wallets
{
    public class Inventory : IChangedInventory
    {
        private readonly Dictionary<CropType, int> _dictionary;
        
        public event Action<CropType, int> OnChangedByType;
        
        public Inventory()
        {
            _dictionary = new Dictionary<CropType, int>();
        }

        public bool Remove(CropType type, int count)
        {
            if (!Check(type, count)) return false;

            _dictionary[type] -= count;
            OnChangedByType?.Invoke(type, _dictionary[type]);
            return true;
        }
        
        public void Add(CropType type, int count)
        {
            if (!_dictionary.ContainsKey(type))
            {
                _dictionary.Add(type, 0);
            }

            _dictionary[type] += count;
            OnChangedByType?.Invoke(type, _dictionary[type]);
        }

        public bool Check(CropType type, int count)
        {
            if (!_dictionary.ContainsKey(type)) return false;
            
            int currentCount = _dictionary[type];
            return count <= currentCount;
        }
    }
}
