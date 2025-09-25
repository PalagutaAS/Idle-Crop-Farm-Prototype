using Tools.Interface;
using UnityEngine;

namespace Tools.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Pickaxe Config By Levels", menuName = "Custom/Pickaxe Config By Levels")]
    public class LibraryConfigsByLevel : ScriptableObject, ILibraryToolConfigs
    {
        [SerializeField] private PickaxeConfig[] _pickaxeConfigs;

        public IToolConfig GetConfigByLevel(int level)
        {
            if (level < 1 || level > _pickaxeConfigs.Length) return null;
            
            return _pickaxeConfigs[level - 1];
        }
    }

    public interface ILibraryToolConfigs
    {
        IToolConfig GetConfigByLevel(int level);
    }
}