using System;
using UnityEngine;

namespace AI
{
    public enum CustomerState
    {
        None,
        Trade,
        GoAway,
    }
    
    public class CustomerController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _stoppingDistance = 0.1f;

        private CustomerState _state;
        private Vector3 _targetPosition;
        private Animator _animator;
        private Offer _offer;
        public Offer Offer => _offer;
        public bool isInit { get; private set; }
        public CustomerState State => _state;
        

        private bool _isMoving = false;
        
        public event Action<CustomerController> OnCustomerGoToExit;
        
        public void Init(Animator animator)
        {
            _animator = animator;
            isInit = true;
        }

        void Update()
        {
            if (_isMoving && isInit)
            {
                MoveToTarget();
            }
        }

        public void SetOffer(Offer newOffer)
        {
            _offer = newOffer;
        }

        public void StartMovementTo(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            _isMoving = true;
            _animator.SetBool("IsMove", _isMoving);
        }

        public void StopMovement()
        {
            _isMoving = false;
            _animator.SetBool("IsMove", _isMoving);
        }

        private void MoveToTarget()
        {
            Vector3 direction = _targetPosition - transform.position;
            direction.y = 0; 
            
            if (direction.magnitude > _stoppingDistance)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

                transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
            }
            else
            {
                StopMovement();
            }
        }

        public void ChangeState(CustomerState state)
        {
            if (state == CustomerState.GoAway)
            {
                OnCustomerGoToExit?.Invoke(this);
            }
            _state = state;
        }
        
    }
}
