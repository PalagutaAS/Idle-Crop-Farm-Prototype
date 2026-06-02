using UnityEngine;

namespace Crops.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Library Crop Configs", menuName = "Library Configs/New Library Crop Configs")]

    public class LibraryCropConfigs : ScriptableObject
    {
        [SerializeField] private CropConfig[] _cropConfigs;

        public CropConfig GetConfigByType(CropType type)
        {
            foreach (CropConfig config in _cropConfigs)
            {
                if (config.Type == type)
                    return config;
            }
            return null;
        }    
    }
}