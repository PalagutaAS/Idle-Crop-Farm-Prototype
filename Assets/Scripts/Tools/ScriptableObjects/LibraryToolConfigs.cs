using System;
using System.Collections.Generic;
using Tools.Interface;
using UnityEngine;

namespace Tools.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Library Tool Configs", menuName = "Library Configs/Library Tool Configs")]
    public class LibraryToolConfigs : ScriptableObject, ILibraryToolConfigs
    {
        [SerializeField] private ConfigsToolByType[] _libraryToolConfigs;

        private Dictionary<ToolType, Dictionary<int, IToolConfig>> _dictionaryLibraryConfig;
        
        public IToolConfig GetConfigByLevel(ToolType type, int level)
        {
            if (_dictionaryLibraryConfig == null) OnEnable();
            
            if (!_dictionaryLibraryConfig.ContainsKey(type)) return null;
            if (!_dictionaryLibraryConfig.TryGetValue(type, out Dictionary<int, IToolConfig> configs)) return null;
            
            return configs.TryGetValue(level, out IToolConfig config) ? config : null;
        }

        public Dictionary<ToolType, Dictionary<int, IToolConfig>>.KeyCollection GetUsingTypes()
        {
            if (_dictionaryLibraryConfig == null) OnEnable();

            return _dictionaryLibraryConfig.Keys;
        }

        private void OnEnable()
        {
            try
            {
                _dictionaryLibraryConfig = new Dictionary<ToolType, Dictionary<int, IToolConfig>>();
                foreach (ConfigsToolByType libraryByType in _libraryToolConfigs)
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
                        
                        config.Level = i + 1;
                        config.Type = type;
                        config.AnimatorController = libraryByType.AnimatorController;
                        dictionary.Add(config.Level, config);
                    }

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