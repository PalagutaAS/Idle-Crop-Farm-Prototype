using System;
using System.Collections.Generic;
using Logging;

namespace Infrastructure.StateMachine
{
    public interface IStateSwitcher
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        void SetStateFactory(IStateFactory resolve);
    }

    public class GameStateMachine : IStateSwitcher
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IStateFactory _stateFactory;
        
        private IExitableState _activeState;
    
        public GameStateMachine(IStateFactory factory)
        {
            _states = new Dictionary<Type, IExitableState>();
            _stateFactory = factory;
            this.Log("GameStateMachine Constructor");
        }
    
        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            IPayloadedState<TPayload> state = ChangeState<TState>();;
            state.Enter(payload);
        }

        public void SetStateFactory(IStateFactory resolve)
        {
            _stateFactory = resolve;
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            if (_activeState?.GetType() == typeof(TState))
                return _activeState as TState;
        
            _activeState?.Exit();

            TState state = GetState<TState>(); 
            _activeState = state;
            
            return state;
        }

    private TState GetState<TState>() where TState : class, IExitableState
    {
        if (!_states.ContainsKey(typeof(TState)))
        {
            _states.Add(typeof(TState), _stateFactory.Create<TState>());
        }
        
        return _states[typeof(TState)] as TState;
    }
}

    public interface IExitableState
    {
        void Exit();
    }
    
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}