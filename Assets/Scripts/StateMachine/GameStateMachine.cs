using System;
using System.Collections.Generic;
using VContainer;

public class GameStateMachine
{
    private readonly IObjectResolver _container;
    private readonly Dictionary<Type, IExitableState> _states;
    
    private IExitableState _activeState;
    
    
    public GameStateMachine(IObjectResolver container)
    {
        _container = container;
        _states = new();
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
            _states.Add(typeof(TState), _container.Resolve<TState>());
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