using UnityEngine;

namespace Crop
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
            set;
        }
        
        public abstract int OnHarvest();
        public abstract void Grow();
        public abstract void Ripe();
        
        
    }
}