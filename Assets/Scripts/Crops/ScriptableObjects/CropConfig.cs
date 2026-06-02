using UnityEngine;

namespace Crops.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Config Crop", menuName = "Custom/Crop/New Crop Config")]
    public class CropConfig : ScriptableObject
    {
        [SerializeField] private int _count;
        [SerializeField] private Scatter _scatter;
        [SerializeField, Range(0, 1)] private float _weight;
        [SerializeField] private int _pricePerUnit;
        [SerializeField] private float _growTime;
        [SerializeField] private CropType _type;
        [SerializeField] private Sprite _itemSprite;

        public Sprite Sprite => _itemSprite;
        public int Count => _count;
        public float GrowTime => _growTime;
        public CropType Type => _type;
        public int Price => _pricePerUnit;
        public Scatter Scatter => _scatter;
        public float Weight => _weight;
    }

    [System.Serializable]
    public class Scatter
    {
        [SerializeField, Range(-10, 0)] private int _min;
        [SerializeField, Range(0, 10)] private int _max;

        public int Min => _min;
        public int Max => _max;
    }
}