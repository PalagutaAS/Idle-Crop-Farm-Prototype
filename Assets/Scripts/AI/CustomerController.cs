using UnityEngine;

namespace AI
{
    public class CustomerController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _stoppingDistance = 0.1f;

        [Header("Positions")]
        private Vector3 _targetPosition;
        private Animator _animator;
        
        [field: SerializeField] public Offer Offer { get; private set; }

        private bool _isMoving = false;

        public void Init(Animator animator)
        {
            _animator = animator;
        }
        

        void Update()
        {
            if (_isMoving)
            {
                MoveToTarget();
            }
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
    }
}
