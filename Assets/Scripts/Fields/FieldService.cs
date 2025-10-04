using System.Collections.Generic;
using UnityEngine;

namespace Fields
{
    public class FieldService : MonoBehaviour
    {
        private Dictionary<CropType, List<Field>> _fieldsDictionary = new ();
        
        private void Awake()
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