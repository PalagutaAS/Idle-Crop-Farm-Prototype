using System.Collections.Generic;
using UnityEngine;

namespace Fields.ScriptableObjects
{
    
    [CreateAssetMenu(fileName = "New Field Configs By Type", menuName = "Fields/Configs By Type")]
    public class ConfigLibraryFieldsByType : ScriptableObject , ILibraryFieldConfig
    {
        [SerializeField] private ConfigField[] _configFields;
        
        private Dictionary<CropType, IFieldConfig> _dictionaryFieldConfig;
        
        public IFieldConfig[] ConfigFields => _configFields;

        public IFieldConfig GetConfigByType(CropType type)
        {
            if (_dictionaryFieldConfig == null) Constructor();
            
            if (!_dictionaryFieldConfig.ContainsKey(type)) return null;
            
            return _dictionaryFieldConfig[type];
        }

        private void Constructor()
        {
            foreach (var fieldConfig in _configFields)
            {
                if (_dictionaryFieldConfig.ContainsKey(fieldConfig.Type)) continue;
                
                _dictionaryFieldConfig.Add(fieldConfig.Type, fieldConfig);
            }
        }
        
    }

    public interface ILibraryFieldConfig
    {
        public IFieldConfig[] ConfigFields { get; }
        public IFieldConfig GetConfigByType(CropType type);
    }
}