using System;
using System.Collections.Generic;
using UnityEngine;

namespace SavesData
{
    [Serializable]
    public class GameProgress
    {
        public InventoryData InventoryData;
        public WalletData WalletData;
        public FieldsData FieldData;
        public ToolsData ToolsData;
    }

    [Serializable]
    public class WalletData
    {
        public SerializableDictionary<MoneyType, int> Money = new SerializableDictionary<MoneyType, int>();
    }

    [Serializable]
    public class InventoryData
    {
        public SerializableDictionary<CropType, int> Crops = new SerializableDictionary<CropType, int>();
    }
    
    [Serializable]
    public class FieldsData
    {
        public SerializableDictionary<CropType, int> Fields = new SerializableDictionary<CropType, int>();
    }    
    
    [Serializable]
    public class ToolsData
    {
        public SerializableDictionary<ToolType, int> Tools = new SerializableDictionary<ToolType, int>();
    }
    
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> _keys = new List<TKey>();
        [SerializeField] private List<TValue> _values = new List<TValue>();

        public void OnBeforeSerialize()
        {
            _keys.Clear();
            _values.Clear();
            foreach (var kvp in this)
            {
                _keys.Add(kvp.Key);
                _values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();
            for (int i = 0; i < _keys.Count; i++)
            {
                if (i < _values.Count)
                    Add(_keys[i], _values[i]);
            }
        }
    }
}