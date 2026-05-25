using Infrastructure.PersistenceProgress;
using Infrastructure.StateMachine;
using Logging;
using Player.Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class BootstrapperLifetimeScope : LifetimeScope, ICoroutineRunner
    {
        [SerializeField] private ScreenLoading _screenLoading;
        private IContainerBuilder _builder;
    
        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;
            RegisterGlobalServices();
            RegisterStateMachine();
            
            _builder.RegisterEntryPoint<GameBootstrap>();
        }

        private void RegisterGlobalServices()
        {
            _builder.Register<DebugLogService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            _builder.Register<SavedLoadService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            _builder.RegisterInstance(GetComponent<CoroutineRunner>()).As<ICoroutineRunner>();
            _builder.RegisterComponentInNewPrefab(_screenLoading, Lifetime.Singleton);
            _builder.Register<SceneLoader>(Lifetime.Singleton);
            _builder.Register<PersistenceProgressService>(Lifetime.Singleton).AsImplementedInterfaces();
            _builder.Register<StandaloneInputService>(Lifetime.Singleton).As<IInputService>();
        }

        private void RegisterStateMachine()
        {
            _builder.Register<ResolverStateFactory>(Lifetime.Scoped).AsImplementedInterfaces();
            _builder.Register<LoadSavesState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            _builder.Register<LoadLevelState>(Lifetime.Singleton).AsSelf().As<IExitableState, IPayloadedState<string>>();
            _builder.Register<GameStateMachine>(Lifetime.Singleton).AsSelf().As<IStateSwitcher>();
        }
    }

    public class GameBootstrap : IStartable
    {
        private IStateSwitcher StateMachine { get; }

        public GameBootstrap(IStateSwitcher gameStateMachine, IObjectResolver resolver)
        {
            StateMachine = gameStateMachine;
            resolver.TryResolve<IDebugLogService>(out IDebugLogService service);
        }

        public void Start()
        {
            StateMachine.Enter<LoadSavesState>();
        }
    }
}