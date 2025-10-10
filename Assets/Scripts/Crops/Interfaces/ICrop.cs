using UnityEngine;

namespace Crops
{
    public interface ICrop
    {
        public CropType Type { get; }
        public Vector3 Position { get; }
        public bool IsHarvesting { get; }
        public void PreparingForHarvest();
        public int OnHarvest();
        public void Grow();
        public void Ripe();
    }
}