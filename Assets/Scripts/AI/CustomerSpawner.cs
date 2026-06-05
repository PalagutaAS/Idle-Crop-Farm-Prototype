using System;
using Fields;
using Offers;
using UnityEngine;
using VContainer;

namespace AI
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnRate;

        private IOfferRandomService _offerService;
        private ICustomerFactory _factory;
        private IFieldService _fieldService;
        public event Action<CustomerController> OnCustomerCreated;

        [Inject]
        private void Constructor(ICustomerFactory factory, IOfferRandomService offerRandomService, IFieldService fieldService)
        {
            _factory = factory;
            _fieldService = fieldService;
            _offerService = offerRandomService;
        }

        private void Start()
        {
            Spawn();
        }

        [ContextMenu("Spawn")]
        private void ImmediatelySpawn()
        {
            if (_fieldService.GetActiveFieldCountPerCropType().Count != 0)
            {
                var customer = _factory.Create();
                customer.SetOffer(_offerService.GetRandom());
                OnCustomerCreated?.Invoke(customer);
            }
        }
        
        private void Spawn()
        {
            ImmediatelySpawn();
            
            Invoke(nameof(Spawn), _spawnRate);
        }
    }
}
