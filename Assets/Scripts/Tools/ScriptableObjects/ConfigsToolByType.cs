using UnityEngine;

namespace Tools.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Configs By Type", menuName = "Custom/Configs By Type")]

    public class ConfigsToolByType : ScriptableObject
    {
        [SerializeField] private ToolType _type;
        [SerializeField] private BaseConfig[] _toolConfigs;
        
        public ToolType Type => _type;
        public BaseConfig[] ToolConfigs => _toolConfigs;

    }
}