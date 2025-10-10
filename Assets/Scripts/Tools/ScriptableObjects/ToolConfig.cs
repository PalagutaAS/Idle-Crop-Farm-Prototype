using UnityEngine;

namespace Tools.ScriptableObjects
{
    [System.Serializable]
    public class ToolConfig : BaseConfig
    {
        [SerializeField] private GameObject _model;
        [SerializeField] private float _timeOut = 5f;
        [SerializeField] private float _radius = 2f;
        [SerializeField] private int _cost = 10;
        [SerializeField] private CropType _harvestableCrops;
        
        private int _level = 0;
        private ToolType _type = ToolType.None;
        private RuntimeAnimatorController _animatorController;

        public override RuntimeAnimatorController AnimatorController
        {
            get => _animatorController;
            set
            {
                if (_animatorController == null)
                {
                    _animatorController = value;
                }
            }
        }
        public override int Level
        {
            get => _level;
            set
            {
                if (_level == 0)
                {
                    _level = value;
                }
            }
        }
        public override ToolType Type
        {
            get => _type;
            set
            {
                if (_type.HasFlag(ToolType.None))
                {
                    _type = value;
                }
            }
        }
        public override GameObject Model => _model;
        public override float TimeOut => _timeOut;
        public override CropType HarvestableCrops => _harvestableCrops;
        public override float Radius => _radius;
        public override int Cost => _cost;
    }
}