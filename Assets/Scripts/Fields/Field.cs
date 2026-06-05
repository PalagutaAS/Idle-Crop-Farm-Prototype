using Fields.ScriptableObjects;
using UnityEngine;

namespace Fields
{
    public class Field : MonoBehaviour, IField
    {
        [SerializeField] private ConfigField _config;
        public CropType Type => _config.Type;
        public int Price => _config.Price;
        public GameObject GameObj => gameObject;
        public bool ActiveSelf
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
    }

    public interface IField
    {
        public CropType Type { get; }
        public int Price  { get; }
        bool ActiveSelf { get; set; }
        public GameObject GameObj { get; }
    }
}
