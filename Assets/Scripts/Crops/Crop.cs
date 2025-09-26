using UnityEngine;

namespace Crops
{
    public abstract class Crop : MonoBehaviour
    {
        [field: SerializeField] public CropType Type
        {
            get;
            private set;
        }

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