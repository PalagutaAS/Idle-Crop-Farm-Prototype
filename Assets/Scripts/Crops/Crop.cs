using Crops.ScriptableObjects;
using UnityEngine;

namespace Crops
{
    public abstract class Crop : MonoBehaviour
    {
        [SerializeField] protected CropConfig _config;
        public CropType Type => _config.Type;

        public bool IsHarvesting
        {
            get;
            protected set;
        }

        public abstract void PreparingForHarvest();

        public abstract int OnHarvest();

        public abstract void Grow();
        public abstract void Ripe();
        
        
    }
}