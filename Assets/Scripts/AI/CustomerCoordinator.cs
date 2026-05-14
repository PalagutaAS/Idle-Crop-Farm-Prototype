using ObjectPull;
using UnityEngine;
using VContainer;

namespace AI
{
    public class CustomerCoordinator : MonoBehaviour
    {
        [SerializeField] private StartZone _startPoint;
        [SerializeField] private TradeZone _tradePoint;
        [SerializeField] private ExitZone _exitPoint;
        
        [SerializeField] private QueueManager _queueManager;
        [SerializeField] private CustomerSpawner _spawner;
        
        [Inject]
        private IPoolManager _poolManager;

        private CustomerQueuePosition _queuePosition;

        public Transform StartPoint => _startPoint.transform;
        public Transform TradePoint => _tradePoint.transform;
        public Transform ExitPoint => _exitPoint.transform;

        private void Awake()
        {
            _queuePosition = new(_queueManager, this);
            _spawner.OnCustomerCreated += OnCustomerCreated;
            _tradePoint.OnCustomerEnterTradeZone += OnCustomerReachedTrade;
            _exitPoint.OnCustomerEnterZone += OnCustomerReachedExit;
        }
        
        private void OnCustomerReachedTrade(CustomerController customer) =>
            customer.WaitForDeal(_exitPoint.transform.position);

        private void OnCustomerServed(CustomerController customer)
        {
            if (_queueManager.IsFirstInQueue(customer))
            {
                _queueManager.GetFirst();
                UpdateQueuePositions();
                customer.StartMovementTo(_exitPoint.transform.position);
                customer.OnFinishDeal -= OnCustomerServed;
            }
        }

        private void OnCustomerCreated(CustomerController customer)
        {
            InitializeCustomer(customer);
            SendCustomerToQueue(customer);
            customer.OnFinishDeal += OnCustomerServed;
        }
        private void InitializeCustomer(CustomerController customer) => 
            customer.transform.position = _startPoint.transform.position;

        private void SendCustomerToQueue(CustomerController customer)
        {
            _queueManager.Enqueue(customer);
            Vector3 queuePosition = _queuePosition.GetPosition();
            customer.StartMovementTo(queuePosition);
        }
        
        private void UpdateQueuePositions()
        {
            int i = 0;
            foreach (var cstmr in _queueManager.QueueCollection)
            {
                Vector3 newPosition = _queuePosition.GetPositionByIndex(i++);
                cstmr.StartMovementTo(newPosition);
            }
        }

        private void OnCustomerReachedExit(CustomerController customer) => 
            CleanupCustomer(customer);

        private void CleanupCustomer(CustomerController customer)
        {
            _poolManager.ReturnObject(customer.gameObject);
        }

        private void OnDestroy()
        {
            _spawner.OnCustomerCreated -= OnCustomerCreated;
            _tradePoint.OnCustomerEnterTradeZone -= OnCustomerReachedTrade;
            _exitPoint.OnCustomerEnterZone -= OnCustomerReachedExit;
        }
    }
}