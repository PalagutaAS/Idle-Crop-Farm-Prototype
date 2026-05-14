using System;
using UnityEngine;

namespace AI.StateMachine
{
    public class MoveToPointState : CustomerState
    {
        [Header("Settings")]
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _stoppingDistance = 0.1f;
        
        private Animator _animator;
        private Vector3 _targetPosition;
        private bool _isMoving;
        
        public override void Enter(CustomerPayload payload)
        {
            _animator ??= GetComponentInChildren<Animator>();
            enabled = true;
            _targetPosition = payload.Target;
            _isMoving = true;
            _animator.SetBool("IsMove", _isMoving);
        }
        public override void Exit()
        {
            enabled = false;
            _isMoving = false;
            _animator.SetBool("IsMove", _isMoving);
        }
        public override void Update()
        {
            if (!_isMoving) return;
            
            MoveToTarget();
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
                Exit();
            }
        }
    }
}