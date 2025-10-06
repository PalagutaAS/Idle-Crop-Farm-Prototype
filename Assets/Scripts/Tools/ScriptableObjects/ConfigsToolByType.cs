using Tools.Interface;
using UnityEditor.Animations;
using UnityEngine;

namespace Tools.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Configs By Type", menuName = "Custom/Configs By Type")]

    public class ConfigsToolByType : ScriptableObject
    {
        [SerializeField] private ToolType _type;
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private ToolConfig[] _toolConfigs;
        
        public ToolType Type => _type;
        public AnimatorController AnimatorController => _animator;
        public IToolConfig[] ToolConfigs => _toolConfigs;

    }
}