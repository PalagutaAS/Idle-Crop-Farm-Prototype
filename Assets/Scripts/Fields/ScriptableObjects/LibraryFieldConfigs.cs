using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Fields.ScriptableObjects
{
    
    [CreateAssetMenu(fileName = "New Library Field Configs", menuName = "Library Configs/New Library Field Config")]
    public class LibraryFieldConfigs : ScriptableObject, ILibraryFieldConfig, IInitializable
    {
        [SerializeField] private ConfigField[] _configFields;
        
        private Dictionary<CropType, IFieldConfig> _dictionaryFieldConfig = new();
        
        public IFieldConfig[] ConfigFields => _configFields;

        public IFieldConfig GetConfigByType(CropType type)
        {
            if (!_dictionaryFieldConfig.ContainsKey(type)) return null;
            
            return _dictionaryFieldConfig[type];
        }
        
        
        public void Initialize()
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