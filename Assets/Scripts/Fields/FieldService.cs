using System.Collections.Generic;
using UnityEngine;

namespace Fields
{
    public class FieldService : IFieldService
    {
        private readonly IFieldCollectProvider _fieldCollectProvider;
        private Dictionary<CropType, List<IField>> _fieldsDictionary = new();

        public FieldService(IFieldCollectProvider fieldCollectProvider)
        {
            _fieldCollectProvider = fieldCollectProvider;
            CollectFields();
        }

        public bool HasInactiveField(CropType type)
        {
            if (!_fieldsDictionary.ContainsKey(type)) return false;
            
            foreach (var field in _fieldsDictionary[type])
            {
                if (!field.ActiveSelf) return true;
            }
            return false;
        }

        public void OpenField(CropType type)
        {
            if (!_fieldsDictionary.ContainsKey(type)) return;
            
            foreach (var field in _fieldsDictionary[type])
            {
                if (!field.ActiveSelf)
                {
                    field.ActiveSelf = true;
                    return;
                }
            }
        }

        public Dictionary<CropType, int> GetActiveFieldCountPerCropType()
        {
            Dictionary<CropType, int> fieldsDictionary = new();

            foreach (var keyValue in _fieldsDictionary)
            {
                if (keyValue.Value.Count == 0) continue;
                
                int i = 0;
                foreach (var field in keyValue.Value)
                {
                    i += field.ActiveSelf ? 1 : 0;
                }
                
                if (i == 0) continue;
                
                fieldsDictionary[keyValue.Key] = i;
            }

            return fieldsDictionary;
        }

        private void CollectFields()
        {
            Object[] fields = Object.FindObjectsByType(typeof(Field), FindObjectsInactive.Include, FindObjectsSortMode.None);
        
            foreach (IField field in fields)
            {
                CropType fieldType = field.Type;

                if (!_fieldsDictionary.ContainsKey(fieldType))
                    _fieldsDictionary[fieldType] = new List<IField>();

                _fieldsDictionary[fieldType].Add(field);
            }
        }
    }
}