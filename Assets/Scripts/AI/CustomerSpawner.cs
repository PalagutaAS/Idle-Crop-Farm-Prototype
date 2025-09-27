using System;
using AI.ScriptableObjects;
using Offers;
using UnityEngine;

namespace AI
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _charRoot;
        [SerializeField] private CustomerModels _models;
        [SerializeField] private float _spawnRate;
        [SerializeField] private Transform _aiRoot;

        private OfferRandomService _offerService;
        public event Action<CustomerController> OnCustomerCreated;

        private void Awake()
        {
            _offerService = new OfferRandomService();
        }

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            var charRootGameObject = Instantiate(_charRoot, _aiRoot.transform);
            var customer = charRootGameObject.GetComponent<CustomerController>();
            var model = _models.GetRandomModel();
            var skinGameObject = Instantiate(model, charRootGameObject.transform);
            customer.Init(skinGameObject.GetComponent<Animator>(), _offerService.GetRandomOffer());
            
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
