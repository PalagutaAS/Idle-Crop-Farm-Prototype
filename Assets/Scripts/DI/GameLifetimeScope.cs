using AI;
using AI.ScriptableObjects;
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
        [SerializeField] private CustomerModels _customerModels;
        [SerializeField] private QueueConfig _queueConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            
            builder.RegisterInstance(_customerModels);
            builder.RegisterInstance(_poolConfigsSo);
            builder.RegisterInstance(_toolLibraryConfig);
            builder.RegisterInstance(_queueConfig);
            
            builder.Register<PoolManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterInstance(_player).As<IPlayer, ThirdPersonController>();
            builder.Register<CustomerFactory>(Lifetime.Scoped);
            builder.Register<Inventory>(Lifetime.Scoped);
            builder.Register<Wallet>(Lifetime.Scoped).WithParameter(120);
            builder.Register<PrintCoinCount>(Lifetime.Scoped);
            builder.Register<OfferRandomService>(Lifetime.Transient);
            builder.Register<IToolFactory, ToolFactory>(Lifetime.Scoped);
            builder.RegisterInstance(_toolPrefab).As<Tool>();
            builder.RegisterInstance(_customerController);
        }
    }
}
