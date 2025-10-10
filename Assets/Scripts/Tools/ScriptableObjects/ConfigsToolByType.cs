using Tools.Interface;
using UnityEngine;

namespace Tools.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Configs By Type", menuName = "Custom/Configs By Type")]

    public class ConfigsToolByType : ScriptableObject
    {
        [SerializeField] private ToolType _type;
        [SerializeField] private RuntimeAnimatorController _animator;
        [SerializeField] private ToolConfig[] _toolConfigs;
        
        public ToolType Type => _type;
        public RuntimeAnimatorController AnimatorController => _animator;
        public IToolConfig[] ToolConfigs => _toolConfigs;
    }
}