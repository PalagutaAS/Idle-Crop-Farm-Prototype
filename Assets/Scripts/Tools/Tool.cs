using System.Collections;
using Player;
using UnityEngine;

namespace Tools
{
    public abstract class Tool : MonoBehaviour
    {
        [SerializeField] protected ThirdPersonController _player;
        [SerializeField] private Transform _model;
        [SerializeField] private Transform _targetFollow;
        [SerializeField] private float _speedFollow = 1f;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] protected float _timeOut = 5f;
        [SerializeField] protected float _radius = 2f;
        [SerializeField] private float _checkInterval = 0.1f;
        [SerializeField] private float _animDuration;
        
        public bool IsCooldown { get; private set; }
        public float Radius => _radius;
        public float SpeedFollow => _speedFollow;

        private AnimatorHarvest _animatorHarvest;
        private CropFinder _cropFinder;
        private Follow _follow;
        
        private float _nextCheckTime;

        private void Awake()
        {
            _cropFinder = new(this, _layerMask);
            _follow = new(this,_model, _targetFollow);
            _animatorHarvest = new(this, _model, _animDuration);
        }

        protected void CropDetecting()
        {
            if (IsCooldown) return;
            if (Time.time < _nextCheckTime) return;
            
            _cropFinder.CheckExistingColliders(_player.transform.position);
            _nextCheckTime = Time.time + _checkInterval;
        }
        
        protected void FollowToSlot()
        {
            _follow.ToSlot();
        }

        private void CropHarvest(Crop.Crop crop)
        {
            int cropCount = crop.OnHarvest();
            _player.Inventory.Add(crop.Type, cropCount);

            StartCoroutine(CooldownCoroutine());
        }

        public void TriggerEnter(Crop.Crop component)
        {
            IsCooldown = true;
            component.IsHarvesting = true;
            _animatorHarvest.MoveTo(component.transform.position);
            StartCoroutine(DelayBeforeHarvest(_animDuration, component));
        }
        
        private IEnumerator DelayBeforeHarvest(float delay, Crop.Crop crop)
        {
            yield return new WaitForSeconds(delay);
            CropHarvest(crop);
        }
        
        private IEnumerator CooldownCoroutine()
        {
            yield return new WaitForSeconds(_timeOut);
            IsCooldown = false;
        }

        public void Upgrade()
        {
            _radius += 0.5f;
            _timeOut -= 0.5f;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_player.transform.position, _radius);
        }
    }

}