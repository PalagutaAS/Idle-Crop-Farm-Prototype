using Crops.ScriptableObjects;
using UnityEngine;

namespace Crops
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class BaseCrop : MonoBehaviour, ICrop
    {
        [SerializeField] protected CropConfig _config;
        public CropType Type => _config.Type;
        public Vector3 Position => transform.position;
        public bool IsHarvesting { get; protected set; }
        public abstract void PreparingForHarvest();
        public abstract int OnHarvest();
        public abstract void Grow();
        public abstract void Ripe();
    }
}