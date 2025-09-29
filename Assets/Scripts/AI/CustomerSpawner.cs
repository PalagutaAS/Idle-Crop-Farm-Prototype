using System;
using AI.ScriptableObjects;
using ObjectPull;
using Offers;
using UnityEngine;

namespace AI
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _charRoot;
        [SerializeField] private CustomerModels _models;
        [SerializeField] private float _spawnRate;
        [SerializeField] private PoolManager _poolManager;

        private OfferRandomService _offerService;
        private CustomerFactory _factory;
        public event Action<CustomerController> OnCustomerCreated;

        private void Awake()
        {
            _factory = new CustomerFactory(_poolManager, _charRoot, _models);
            _offerService = new OfferRandomService();
        }

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            var customer = _factory.Create();
            customer.SetOffer(_offerService.GetRandomOffer());
            OnCustomerCreated?.Invoke(customer);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            { 
                Spawn();
            }
        }
        
    }
}
