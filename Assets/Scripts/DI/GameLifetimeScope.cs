using AI;
using AI.ScriptableObjects;
using Fields.ScriptableObjects;
using ObjectPull;
using ObjectPull.ScriptableObjects;
using Offers;
using Player;
using Player.Interface;
using Player.Tools;
using Tools;
using Tools.Interface;
using Tools.ScriptableObjects;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Wallets;

namespace DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private ThirdPersonController _player; 
        
        [Space,Header("Prefab Register")]
        [SerializeField] private Tool _toolPrefab;
        [SerializeField] private CustomerController _customerController;
        
        [Space,Header("Scriptable Object Register")]
        [SerializeField] private PoolConfigsSO _poolConfigsSo; 
        [SerializeField] private LibraryConfigsByLevel _toolLibraryConfig; 
        [SerializeField] private ConfigLibraryFieldsByType _fieldLibraryConfig; 
        [SerializeField] private CustomerModels _customerModels;
        [SerializeField] private QueueConfig _queueConfig;
        
        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;
            
            _builder.RegisterInstance(_player).As<IPlayer, ThirdPersonController>();
            
            RegisterPrefabs();
            RegisterScriptableObjects();
            Register();
        }

        private void Register()
        {
            _builder.Register<PoolManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            _builder.Register<CustomerFactory>(Lifetime.Scoped);
            _builder.Register<Inventory>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            _builder.Register<Wallet>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf().WithParameter(2500);
            _builder.Register<PrintCount>(Lifetime.Scoped);
            _builder.Register<OfferRandomService>(Lifetime.Transient);
            _builder.Register<IToolFactory, ToolFactory>(Lifetime.Scoped);
        }

        private void RegisterScriptableObjects()
        {
            _builder.RegisterInstance(_poolConfigsSo);
            _builder.RegisterInstance(_toolLibraryConfig);
            _builder.RegisterInstance(_queueConfig);
            _builder.RegisterInstance(_fieldLibraryConfig);
        }

        private void RegisterPrefabs()
        {
            _builder.RegisterInstance(_toolPrefab).As<Tool>();
            _builder.RegisterInstance(_customerModels);
            _builder.RegisterInstance(_customerController);
        }
    }
}
