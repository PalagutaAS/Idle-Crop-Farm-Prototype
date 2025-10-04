using Fields.ScriptableObjects;
using UnityEngine;

namespace Fields
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private ConfigField _config;

        public CropType Type => _config.Type;
        public int Price => _config.Price;
    }
}
