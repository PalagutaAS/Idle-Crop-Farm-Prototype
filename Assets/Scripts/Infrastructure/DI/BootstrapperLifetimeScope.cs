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
    
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(GetComponent<CoroutineRunner>()).As<ICoroutineRunner>();
            builder.RegisterComponentInNewPrefab(_screenLoading, Lifetime.Singleton);
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<PersistenceProgressService>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<ContainerStateFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<Inventory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<Wallet>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf().WithParameter(MoneyType.Coin).WithParameter(0);
            builder.Register<LoadSavesState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SavedLoadService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<LoadLevelState>(Lifetime.Singleton).AsSelf().As<IExitableState, IPayloadedState<string>>();
            builder.Register<GameLoopState>(Lifetime.Singleton).AsSelf().As<IExitableState, IState>();
            builder.Register<GameStateMachine>(Lifetime.Singleton).AsSelf().As<IStateSwitcher>();
            builder.Register<StandaloneInputService>(Lifetime.Singleton).As<IInputService>();

            builder.RegisterEntryPoint<GameBootstrap>();
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