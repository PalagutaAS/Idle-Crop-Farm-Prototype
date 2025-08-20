using UnityEngine;

namespace Crop
{
    public abstract class Crop : MonoBehaviour
    {
        public CropType Type { get; } = CropType.None;
        public abstract int OnHarvest();
        public abstract void Grow();
        public abstract void Ripe();
    }
}