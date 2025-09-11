using System.Collections.Generic;
using UnityEngine;

namespace Wallets
{
    public class Inventory
    {
        private readonly Dictionary<CropType, int> _dictionary;

        public Inventory()
        {
            _dictionary = new Dictionary<CropType, int>();
        }

        public bool Remove(CropType type, int count)
        {
            int currentCount = _dictionary[type];
            if (count > currentCount) return false;

            _dictionary[type] -= count;
            return true;
        }
        
        public void Add(CropType type, int count)
        {
            if (!_dictionary.ContainsKey(type))
            {
                _dictionary.Add(type, 0);
            }

            _dictionary[type] += count;
            Debug.Log("CROP: [" + type + "]" + _dictionary[type]);
        }
    }
}
