using System.Collections;
using Crops;
using Player.Interface;
using Tools.Interface;
using UnityEngine;

namespace Tools
{
    public class Tool : MonoBehaviour, ITool
    {
        [SerializeField] private Transform _parentModel;
        
        [SerializeField] private float _speedFollow = 1f;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _checkInterval = 0.1f;
        [SerializeField] private float _animDuration;

        private IToolConfig _toolConfig;

        public bool IsCooldown { get; private set; }
        public float Radius => _toolConfig.Radius;
        public float SpeedFollow => _speedFollow;
        public int CurrentLevel => _toolConfig.Level;
        public ToolType Type => _toolConfig.Type;

        private AnimatorHarvest _animatorHarvest;
        private CropFinder _cropFinder;
        private Follow _follow;
        private ChildModelChanger _modelChanger;
        private ISlot _slot;
        private float _nextCheckTime;
        private IPlayer _player;

        private void Construct()
        {
            _cropFinder = new CropFinder(this, _layerMask, _toolConfig.HarvestableCrops);
            _follow = new Follow(this, _parentModel, _slot.Transform);
            _animatorHarvest = new AnimatorHarvest(_parentModel, _animDuration, _toolConfig.AnimatorController);
            _modelChanger = new ChildModelChanger(_parentModel, _toolConfig.Model);
        }
        
        private void Update()
        {
            FollowToSlot();
            CropDetecting();
        }

        protected void CropDetecting()
        {
            if (IsCooldown) return;
            if (Time.time < _nextCheckTime) return;
            
            _cropFinder.CheckExistingColliders(_player.Transform.position);
            _nextCheckTime = Time.time + _checkInterval;
        }
        
        protected void FollowToSlot()
        {
            _follow.ToSlot();
        }

        private void CropHarvest(BaseCrop baseCrop)
        {
            int cropCount = baseCrop.OnHarvest();
            _player.Inventory.Add(baseCrop.Type, cropCount);

            StartCoroutine(CooldownCoroutine());
        }

        public void TriggerEnter(BaseCrop component)
        {
            IsCooldown = true;
            component.PreparingForHarvest();
            _animatorHarvest.MoveTo(component.transform.position);
            StartCoroutine(DelayBeforeHarvest(_animDuration, component));
        }
        
        private IEnumerator DelayBeforeHarvest(float delay, BaseCrop baseCrop)
        {
            yield return new WaitForSeconds(delay);
            CropHarvest(baseCrop);
        }
        
        private IEnumerator CooldownCoroutine()
        {
            yield return new WaitForSeconds(_toolConfig.TimeOut);
            IsCooldown = false;
        }
        
        public void Initialize(IPlayer player, ISlot slot, IToolConfig config)
        {
            _player = player;
            _slot = slot;
            _toolConfig = config;
            _parentModel.transform.position = _slot.Transform.position;
            Construct();
        }
        
        public void Upgrade(IToolConfig config)
        {
            _toolConfig = config;
            _modelChanger?.ChangeModel(_toolConfig.Model);
        }
    }

}