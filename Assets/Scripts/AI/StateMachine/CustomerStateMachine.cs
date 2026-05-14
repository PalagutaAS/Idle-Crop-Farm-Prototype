using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.StateMachine
{
    public class CustomerStateMachine : MonoBehaviour
    {
        [SerializeField] private CustomerState[] _states;
        private Dictionary<Type, ICustomerState> _dictionaryStates = new ();

        private ICustomerState _currentState;
        private CustomerPayload _payload;

        private void OnEnable()
        {
            if (_currentState != null)
            {
                _currentState.Exit();
                _currentState = null;
            }
        }
        
        private void Awake()
        {
            foreach (var state in _states)
            {
                _dictionaryStates.Add(state.GetType(), state);
                state.enabled = false;
                state.Initialize(this);
            }
        }

        public void ChangeState<TState>(CustomerPayload payload) where TState : CustomerState
        {
            _currentState?.Exit();
            _currentState = _dictionaryStates[typeof(TState)];
            _currentState.Enter(payload);
        } 
    }

    public struct CustomerPayload
    {
        public Vector3 Target;
        public Action OnCompleted;

        public CustomerPayload(Vector3 targetPosition, Action onCompleted = null)
        {
            Target = targetPosition;
            this.OnCompleted = onCompleted;
        }
    }

    internal interface ICustomerState
    {
        void Enter(CustomerPayload payload);
        void Exit();
        void Update();
    }

    public abstract class CustomerState : MonoBehaviour, ICustomerState
    {
        protected CustomerStateMachine _csm;
        public virtual void Enter(CustomerPayload payload) { }

        public virtual void Exit() { }
        public virtual void Update() { }

        public void Initialize(CustomerStateMachine customerStateMachine)
        {
            _csm = customerStateMachine;
        }
    }
}