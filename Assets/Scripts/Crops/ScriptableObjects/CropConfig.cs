using UnityEngine;

namespace Crops.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Config Crop", menuName = "Custom/Crop/New Crop Config")]
    public class CropConfig : ScriptableObject
    {
        [SerializeField] private int _count;
        [SerializeField] private float _growTime;
        [SerializeField] private GameObject _model;
        [SerializeField] private CropType _type;
        
        public int Count => _count;
        public float GrowTime => _growTime;
        public GameObject Model => _model;
        public CropType Type => _type;
    }
}