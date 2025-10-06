using System;
using Offers;
using UnityEngine;
using VContainer;

namespace AI
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnRate;

        private OfferService _offerService;
        private CustomerFactory _factory;
        public event Action<CustomerController> OnCustomerCreated;

        [Inject]
        private void Constructor(CustomerFactory factory, OfferService offerRandomService)
        {
            _factory = factory;
            _offerService = offerRandomService;
        }

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            var customer = _factory.Create();
            customer.SetOffer(_offerService.GetRandom());
            OnCustomerCreated?.Invoke(customer);
            Invoke(nameof(Spawn), _spawnRate);
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
