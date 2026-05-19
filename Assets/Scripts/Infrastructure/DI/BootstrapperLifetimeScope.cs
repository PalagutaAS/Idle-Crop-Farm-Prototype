using Infrastructure.PersistenceProgress;
using Infrastructure.StateMachine;
using Inventor;
using Player.Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Wallets;

namespace Infrastructure.DI
{
    public class BootstrapperLifetimeScope : LifetimeScope, ICoroutineRunner
    {
        [SerializeField] private ScreenLoading _screenLoading;
        private IContainerBuilder _builder;
    
        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;
            RegisterOtherServices();
            RegisterStateMachine();
            
            _builder.RegisterEntryPoint<GameBootstrap>();
        }

        private void RegisterOtherServices()
        {
            _builder.RegisterInstance(GetComponent<CoroutineRunner>()).As<ICoroutineRunner>();
            _builder.RegisterComponentInNewPrefab(_screenLoading, Lifetime.Singleton);
            _builder.Register<SceneLoader>(Lifetime.Singleton);
            _builder.Register<PersistenceProgressService>(Lifetime.Singleton).AsImplementedInterfaces();
            _builder.Register<Inventory>(Lifetime.Singleton).AsImplementedInterfaces();
            _builder.Register<Wallet>(Lifetime.Singleton).AsImplementedInterfaces().WithParameter(MoneyType.Coin).WithParameter(0);
            _builder.Register<StandaloneInputService>(Lifetime.Singleton).As<IInputService>();
        }

        private void RegisterStateMachine()
        {
            _builder.Register<ResolverStateFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            _builder.Register<LoadSavesState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            _builder.Register<SavedLoadService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            _builder.Register<LoadLevelState>(Lifetime.Singleton).AsSelf().As<IExitableState, IPayloadedState<string>>();
            _builder.Register<GameLoopState>(Lifetime.Singleton).AsSelf().As<IExitableState, IState>();
            _builder.Register<GameStateMachine>(Lifetime.Singleton).AsSelf().As<IStateSwitcher>();
        }
    }

    public class GameBootstrap : IStartable
    {
        private IStateSwitcher StateMachine { get; }

        public GameBootstrap(IStateSwitcher gameStateMachine)
        {
            StateMachine = gameStateMachine;
        }

        public void Start()
        {
            StateMachine.Enter<LoadSavesState>();
        }
    }
}