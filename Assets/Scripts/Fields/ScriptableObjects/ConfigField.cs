using UnityEngine;

namespace Fields.ScriptableObjects
{    
    [CreateAssetMenu(fileName = "New Field Config", menuName = "Fields/Field Config")]
    public class ConfigField : ScriptableObject, IFieldConfig
    {
        [SerializeField] private int _price;
        [SerializeField] private CropType _typeField;
        
        public int Price => _price;
        public CropType Type => _typeField;
    }

    public interface IFieldConfig
    {
        public int Price { get; }
        public CropType Type { get; }
    }
}