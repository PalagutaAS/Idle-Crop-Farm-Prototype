using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

namespace Fields
{
    public class FieldService : MonoBehaviour, IFieldService, IInitializable
    {
        private Dictionary<CropType, List<Field>> _fieldsDictionary = new ();
        
        public void Initialize()
        {
             CollectFields();
        }

        public bool HasInactiveField(CropType type)
        {
            if (!_fieldsDictionary.ContainsKey(type)) return false;
            
            foreach (var field in _fieldsDictionary[type])
            {
                if (!field.gameObject.activeSelf) return true;
            }
            return false;
        }

        public void OpenField(CropType type)
        {
            if (!_fieldsDictionary.ContainsKey(type)) return;
            
            foreach (var field in _fieldsDictionary[type])
            {
                if (!field.gameObject.activeSelf)
                {
                    field.gameObject.SetActive(true);
                    return;
                }
            }
        }

        public bool HasActiveField(CropType type)
        {
            if (!_fieldsDictionary.ContainsKey(type)) return false;
            
            foreach (var field in _fieldsDictionary[type])
            {
                if (field.gameObject.activeSelf) return true;
            }
            return false;
        }
        
        
        public Dictionary<CropType, int> GetActiveCropType()
        {
            Dictionary<CropType, int> fieldsDictionary = new();

            foreach (var keyValue in _fieldsDictionary)
            {
                if (keyValue.Value.Count == 0) continue;
                
                int i = 0;
                foreach (var field in keyValue.Value)
                {
                    i += field.gameObject.activeSelf ? 1 : 0;
                }
                
                if (i == 0) continue;
                
                fieldsDictionary[keyValue.Key] = i;
            }

            return fieldsDictionary;
        }

        private void CollectFields()
        {
            Field[] fields = GetComponentsInChildren<Field>(true);
        
            foreach (Field field in fields)
            {
                CropType fieldType = field.Type;

                if (!_fieldsDictionary.ContainsKey(fieldType))
                    _fieldsDictionary[fieldType] = new List<Field>();

                _fieldsDictionary[fieldType].Add(field);
            }
        }
    }
}