using Unity.Mathematics;
using UnityEngine;

namespace AI
{
    public class CostumerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _charRoot;
        [SerializeField] private GameObject _charPrefab;
        [SerializeField] private float _spawnRate;

        [SerializeField] private Transform _aiRoot;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _tradePoint;
        [SerializeField] private Transform _exitPoint;

        [SerializeField] private QueueManager _queueManager;

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {

            var charRootGameObject = Instantiate(_charRoot, _startPoint.position, quaternion.identity, _aiRoot.transform);
            var customer = charRootGameObject.GetComponent<CustomerController>();
            var skinGameObject = Instantiate(_charPrefab, charRootGameObject.transform);
            customer.Init(skinGameObject.GetComponent<Animator>());

            int queueCount = _queueManager.Enqueue(customer);
            Vector3 dir = (_tradePoint.position - _startPoint.position).normalized;
            customer.StartMovementTo(_tradePoint.position - (dir * _queueManager.OffsetBetween * (queueCount - 1)));
            if (queueCount < 5)
            {
                Invoke(nameof(Spawn), _spawnRate);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CustomerGoToExit();
            }
        }

        public void CustomerGoToExit()
        {
            var firstCustomer = _queueManager.GetFirst();
            firstCustomer.StartMovementTo(_exitPoint.position);
                
            int queueCount = _queueManager.GetCount();
            int i = 0;
            foreach (var customer in _queueManager.QueueCollection)
            {
                Vector3 direction = (_tradePoint.position - customer.transform.position).normalized;
                customer.StartMovementTo(_tradePoint.position - direction * (_queueManager.OffsetBetween * (queueCount - (queueCount - i))));
                i++;
            }
        }
    }
}
