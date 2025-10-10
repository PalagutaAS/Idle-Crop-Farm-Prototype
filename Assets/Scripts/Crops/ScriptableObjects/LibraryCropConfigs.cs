using System.Linq;
using UnityEngine;

namespace Crops.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Library Crop Configs", menuName = "Library Configs/New Library Crop Configs")]

    public class LibraryCropConfigs : ScriptableObject
    {
        [SerializeField] private CropConfig[] _cropConfigs;

        public CropConfig GetConfigByType(CropType type) =>  _cropConfigs.FirstOrDefault(config => config.Type == type);
    }
}