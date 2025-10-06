using AI;
using AI.ScriptableObjects;
using Crops.ScriptableObjects;
using Fields;
using Fields.ScriptableObjects;
using Inventor;
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

namespace DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private ThirdPersonController _player;
        [SerializeField] private FieldService _fieldService;
        
        [Space,Header("Prefab Register")]
        [SerializeField] private Tool _toolPrefab;
        [SerializeField] private CustomerController _customerController;
        
        [Space,Header("Scriptable Object Register")]
        [SerializeField] private PoolConfigsSO _poolConfigsSo; 
        [SerializeField] private LibraryConfigsByLevel _libraryToolConfig; 
        [SerializeField] private ConfigLibraryFieldsByType _libraryFieldConfig; 
        [SerializeField] private LibraryCropConfigs _libraryCropConfig; 
        [SerializeField] private CustomerModels _customerModels;
        [SerializeField] private QueueConfig _queueConfig;
        
        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;
            
            _builder.RegisterInstance(_player).As<IPlayer>();
            _builder.RegisterInstance(_fieldService).AsImplementedInterfaces();
            
            RegisterPrefabs();
            RegisterScriptableObjects();
            Register();
        }

        private void Register()
        {
            _builder.Register<PoolManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            _builder.Register<CustomerFactory>(Lifetime.Scoped);
            _builder.Register<Inventory>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            _builder.Register<Wallet>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf().WithParameter<MoneyType>(MoneyType.Coin).WithParameter<int>(2500);
            _builder.Register<OfferRandomService>(Lifetime.Transient);
            _builder.Register<OfferService>(Lifetime.Scoped);
            _builder.Register<ToolFactory>(Lifetime.Scoped).As<IToolFactory>();
        }

        private void RegisterScriptableObjects()
        {
            _builder.RegisterInstance(_poolConfigsSo);
            _builder.RegisterInstance(_libraryToolConfig);
            _builder.RegisterInstance(_queueConfig);
            _builder.RegisterInstance(_libraryFieldConfig);
            _builder.RegisterInstance(_libraryCropConfig);
        }

        private void RegisterPrefabs()
        {
            _builder.RegisterInstance(_toolPrefab).As<Tool>();
            _builder.RegisterInstance(_customerModels);
            _builder.RegisterInstance(_customerController);
        }
    }
}
