using System;
using UnityEngine;

namespace AI
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _charRoot;
        [SerializeField] private GameObject _charPrefab;
        [SerializeField] private float _spawnRate;

        [SerializeField] private Transform _aiRoot;
        
        public event Action<CustomerController> OnCustomerCreated;
        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            var charRootGameObject = Instantiate(_charRoot, _aiRoot.transform);
            var customer = charRootGameObject.GetComponent<CustomerController>();
            var skinGameObject = Instantiate(_charPrefab, charRootGameObject.transform);
            customer.Init(skinGameObject.GetComponent<Animator>());
            
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
