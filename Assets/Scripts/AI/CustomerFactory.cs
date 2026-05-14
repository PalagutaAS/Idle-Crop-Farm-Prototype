using AI.ScriptableObjects;
using ObjectPull;
using UnityEngine;

namespace AI
{
    public class CustomerFactory : ICustomerFactory
    {
        private readonly IPoolManager _poolManager;
        private readonly GameObject _charPrefab;
        private readonly CustomerModels _customerModels;

        public CustomerFactory(IPoolManager poolManager, CustomerController charRoot, CustomerModels customerModels)
        {
            _poolManager = poolManager;
            _charPrefab = charRoot.gameObject;
            _customerModels = customerModels;
        }

        public CustomerController Create()
        {
            var customer = _poolManager.GetObject<CustomerController>(_charPrefab);
            customer.gameObject.SetActive(true);
            if (customer.isInit) return customer;
            
            Object.Instantiate(_customerModels.GetRandomModel(), customer.transform);
            customer.Init();
            return customer;
        }
    }

    public interface ICustomerFactory
    {
        public CustomerController Create();
    }
}