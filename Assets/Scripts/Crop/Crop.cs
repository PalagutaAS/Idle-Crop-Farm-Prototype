using UnityEngine;

namespace Crop
{
    public abstract class Crop : MonoBehaviour
    {
        public bool IsHarvestable { get; }
        public abstract int OnHarvest();
        public abstract void Grow();
        public abstract void Ripe();
    }
}