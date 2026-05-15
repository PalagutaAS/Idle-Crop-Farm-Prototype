using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BootstrapperLifetimeScope : LifetimeScope, ICoroutineRunner
{
    [SerializeField] private ScreenLoading _screenLoading;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(GetComponent<CoroutineRunner>()).As<ICoroutineRunner>();
        builder.RegisterComponentInNewPrefab(_screenLoading, Lifetime.Singleton);
        builder.Register<SceneLoader>(Lifetime.Singleton);
        builder.Register<LoadLevelState>(Lifetime.Singleton).AsSelf().As<IExitableState, IPayloadedState<string>>();
        builder.Register<GameLoopState>(Lifetime.Singleton).AsSelf().As<IExitableState, IState>();
        builder.Register<GameStateMachine>(Lifetime.Singleton);

        builder.RegisterEntryPoint<GameBootstrap>();
    }
}

public class GameBootstrap : IStartable
{
    private GameStateMachine StateMachine { get; }

    public GameBootstrap(GameStateMachine gameStateMachine)
    {
        StateMachine = gameStateMachine;
    }

    public void Start()
    {
        StateMachine.Enter<LoadLevelState, string>("SampleScene");
    }
}