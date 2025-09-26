using System;
using System.Collections.Generic;
using Tools.Interface;
using UnityEngine;

namespace Tools.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Library Configs", menuName = "Custom/Library Configs")]
    public class LibraryConfigsByLevel : ScriptableObject, ILibraryToolConfigs
    {
        [SerializeField] private ConfigsToolByType[] _libraryConfigs;

        private Dictionary<ToolType, Dictionary<int, IToolConfig>> _dictionaryLibraryConfig;
        
        public IToolConfig GetConfigByLevel(ToolType type, int level)
        {
            if (_dictionaryLibraryConfig == null) Regroup();
            
            if (!_dictionaryLibraryConfig.ContainsKey(type)) return null;
            if (!_dictionaryLibraryConfig.TryGetValue(type, out Dictionary<int, IToolConfig> configs)) return null;
            
            return configs.TryGetValue(level, out IToolConfig config) ? config : null;
        }

        public Dictionary<ToolType, Dictionary<int, IToolConfig>>.KeyCollection GetUsingTypes()
        {
            return _dictionaryLibraryConfig.Keys;
        }

        private void Regroup()
        {
            try
            {
                _dictionaryLibraryConfig = new Dictionary<ToolType, Dictionary<int, IToolConfig>>();
                foreach (ConfigsToolByType libraryByType in _libraryConfigs)
                {
                    ToolType type = libraryByType.Type;
                    
                    if (_dictionaryLibraryConfig.ContainsKey(type)) 
                        throw new InvalidOperationException(($"There are not enough configs for {typeof(IToolConfig)}"));
                    if (libraryByType.ToolConfigs == null || libraryByType.ToolConfigs.Length == 0)
                        throw new InvalidOperationException($"No tool configs found for type: {type}");
                    
                    Dictionary<int, IToolConfig> dictionary = new Dictionary<int, IToolConfig>();

                    for (int i = 0; i < libraryByType.ToolConfigs.Length; i++)
                    {
                        IToolConfig config = libraryByType.ToolConfigs[i];
                        
                        if (config == null)
                            throw new InvalidOperationException($"Null config found at index {i} for tool type: {type}");
                        if (dictionary.ContainsKey(config.Level))
                            throw new InvalidOperationException($"Duplicate level {config.Level} found for tool type: {type}");
                        
                        config.Type = type;
                        dictionary.Add(config.Level, config);
                    }
                    
                    if (!dictionary.ContainsKey(1))
                        throw new InvalidOperationException($"Tool type {type} must have a config with level 1");

                    _dictionaryLibraryConfig.Add(type, dictionary);
                }
                
                if (_dictionaryLibraryConfig.Count == 0)
                    throw new InvalidOperationException("No valid tool configurations were processed");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error in Regroup method: {ex.Message}");
                throw;
            }
        }
    }

    public interface ILibraryToolConfigs
    {
        IToolConfig GetConfigByLevel(ToolType type,int level);
        Dictionary<ToolType, Dictionary<int, IToolConfig>>.KeyCollection GetUsingTypes();
    }
}