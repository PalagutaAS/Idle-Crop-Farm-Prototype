using Tools.Interface;
using UnityEngine;

namespace Tools.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Pickaxe Config", menuName = "Custom/Pickaxe Config")]

    public class PickaxeConfig : BaseConfig
    {
        [SerializeField, Range(1, 10)] private int _level = 1;
        [SerializeField] private GameObject _model;
        [SerializeField] private float _timeOut = 5f;
        [SerializeField] private float _radius = 2f;
        [SerializeField] private int _cost = 10;
        
        private ToolType _type;
        public override ToolType Type
        {
            get => _type;
            set
            {
                if (_type == ToolType.None)
                {
                    _type = value;
                }
            }
        }
        public override GameObject Model => _model;
        public override float TimeOut => _timeOut;
        public override float Radius => _radius;
        public override int Cost => _cost;
        public override int Level => _level;
    }
}