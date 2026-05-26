using AI;
using AI.ScriptableObjects;
using Crops.ScriptableObjects;
using Fields;
using Fields.ScriptableObjects;
using Infrastructure.Services;
using Infrastructure.StateMachine;
using Inventor;
using Logging;
using ObjectPull;
using ObjectPull.ScriptableObjects;
using Offers;
using Player;
using Player.Interface;
using Player.Tools;
using Tools;
using Tools.Interface;
using Tools.ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Wallets;

namespace Infrastructure.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private ThirdPersonController _player;
        [SerializeField] private FieldCollectProvider _fieldCollectProvider;
        [SerializeField] private PlayerTools _toolsManager;
        
        [Space,Header("Prefab Register")]
        [SerializeField] private Tool _toolPrefab;
        [SerializeField] private CustomerController _customerController;
        
        [Space,Header("Scriptable Object Register")]
        [SerializeField] private LibraryPoolConfigs _libraryPoolConfigs;
        [SerializeField] private LibraryToolConfigs _libraryToolConfig;
        [SerializeField] private LibraryFieldConfigs _libraryFieldConfigs;
        [SerializeField] private LibraryCropConfigs _libraryCropConfigs;
        [SerializeField] private CustomerModels _customerModels;
        [SerializeField] private QueueConfig _queueConfig;
        
        [Space, Header("DEBUG")]
        [SerializeField] protected DebugMenu _debugCanvasMenu;
        
        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;
            
            _builder.RegisterInstance(_player).As<IPlayer>();
            _builder.RegisterInstance(_fieldCollectProvider).AsImplementedInterfaces();
            _builder.RegisterInstance(_toolsManager).AsImplementedInterfaces();
            
            RegisterStates();
            RegisterPrefabs();
            RegisterScriptableObjects();
            Register();
            RegisterDebug();
            this.Log("All containers is build");
        }

        private void RegisterDebug()
        {
#if UNITY_EDITOR || DEBUG
            _builder.RegisterComponentInNewPrefab(_debugCanvasMenu, Lifetime.Singleton).AsImplementedInterfaces();
            _builder.RegisterBuildCallback(resolver =>
            {
                resolver.TryResolve(out IDebugMenu service);
            });
#else
            _builder.Register<EmptyDebugMenu>(Lifetime.Singleton).AsImplementedInterfaces();
#endif
        }

        private void Register()
        {
            _builder.Register<GameRestartService>(Lifetime.Scoped).AsImplementedInterfaces();
            _builder.Register<ResetSaveService>(Lifetime.Scoped).AsImplementedInterfaces();
            _builder.Register<FieldService>(Lifetime.Singleton).AsImplementedInterfaces();
            _builder.Register<Inventory>(Lifetime.Singleton).AsImplementedInterfaces();
            _builder.Register<Wallet>(Lifetime.Singleton).AsImplementedInterfaces().WithParameter(MoneyType.Coin).WithParameter(0);
            _builder.Register<SaveToGameDataService>(Lifetime.Singleton).AsImplementedInterfaces();
            _builder.Register<PoolManager>(Lifetime.Singleton).AsImplementedInterfaces();
            _builder.Register<CustomerFactory>(Lifetime.Scoped).AsImplementedInterfaces();
            _builder.Register<OfferRandomService>(Lifetime.Transient).AsImplementedInterfaces();
            _builder.Register<ToolFactory>(Lifetime.Scoped).As<IToolFactory>();
        }
        
        private void RegisterStates()
        {
            _builder.RegisterBuildCallback(resolver =>
            {
                var stateSwitcher = resolver.Resolve<IStateSwitcher>();
                stateSwitcher.SetStateFactory(resolver.Resolve<IStateFactory>());
            });

            _builder.Register<GameLoopState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            _builder.Register<GameMenuPauseState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            _builder.Register<ApplyGameProgressState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }

        private void RegisterScriptableObjects()
        {
            _builder.RegisterInstance(_libraryPoolConfigs).AsImplementedInterfaces();
            _builder.RegisterInstance(_libraryToolConfig);
            _builder.RegisterInstance(_queueConfig);
            _builder.RegisterInstance(_libraryFieldConfigs).AsImplementedInterfaces();
            _builder.RegisterInstance(_libraryCropConfigs);
        }

        private void RegisterPrefabs()
        {
            _builder.RegisterInstance(_toolPrefab).As<Tool>();
            _builder.RegisterInstance(_customerModels);
            _builder.RegisterInstance(_customerController);
        }
    }
}
