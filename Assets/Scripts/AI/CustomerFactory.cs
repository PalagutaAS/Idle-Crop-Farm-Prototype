using AI.ScriptableObjects;
using ObjectPull;
using UnityEngine;

namespace AI
{
    public class CustomerFactory
    {
        private readonly PoolManager _poolManager;
        private readonly GameObject _charRoot;
        private readonly CustomerModels _customerModels;

        public CustomerFactory(PoolManager poolManager, GameObject charRoot, CustomerModels customerModels)
        {
            _poolManager = poolManager;
            _charRoot = charRoot;
            _customerModels = customerModels;
        }

        public CustomerController Create()
        {
            var customer = _poolManager.GetObject<CustomerController>(_charRoot);
            customer.gameObject.SetActive(true);
            if (customer.isInit) return customer;
            
            var skinGameObject = Object.Instantiate(_customerModels.GetRandomModel(), customer.transform);
            customer.Init(skinGameObject.GetComponent<Animator>());
            return customer;
        }
    }
}