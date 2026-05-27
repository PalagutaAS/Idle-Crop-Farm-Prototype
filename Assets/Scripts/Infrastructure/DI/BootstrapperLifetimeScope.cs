using Infrastructure.PersistenceProgress;
using Infrastructure.StateMachine;
using Logging;
using Player.Input;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class BootstrapperLifetimeScope : LifetimeScope, ICoroutineRunner
    {
        [SerializeField] private ScreenLoading _screenLoading;
        private IContainerBuilder _builder;
        private IObjectResolver _resolver;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;
            RegisterDebug();
            RegisterGlobalServices();
            RegisterStateMachine();
            
            _builder.RegisterBuildCallback(resolver =>
            {
                _resolver = resolver;
            });
            
            _builder.RegisterEntryPoint<GameBootstrap>();
        }

        private void RegisterDebug()
        {
#if UNITY_EDITOR || DEBUG
            _builder.Register<DebugLogService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            _builder.RegisterBuildCallback(resolver =>
            {
                resolver.Resolve<IDebugLogService>();
            });
#endif
        }

        private void RegisterGlobalServices()
        {
            _builder.Register<SavedLoadService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            _builder.RegisterInstance(GetComponent<CoroutineRunner>()).As<ICoroutineRunner>();
            _builder.RegisterComponentInNewPrefab(_screenLoading, Lifetime.Singleton).DontDestroyOnLoad();
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

        protected override void OnDestroy()
        {
            _resolver.TryResolve(out ScreenLoading screenLoading);
            if (!screenLoading.IsDestroyed())
                Destroy(screenLoading.gameObject);
            
            base.OnDestroy();
        }
    }

    public class GameBootstrap : IStartable
    {
        private IStateSwitcher StateMachine { get; }

        public GameBootstrap(IStateSwitcher gameStateMachine)
        {
            StateMachine = gameStateMachine;
            this.Log("GameBootstrap Constructor");
        }

        public void Start()
        {
            StateMachine.Enter<LoadSavesState>();
        }
    }
}