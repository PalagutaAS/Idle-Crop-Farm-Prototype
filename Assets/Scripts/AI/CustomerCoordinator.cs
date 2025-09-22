using UnityEngine;

namespace AI
{
    public class CustomerCoordinator : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _tradePoint;
        [SerializeField] private Transform _exitPoint;
        
        [SerializeField] private QueueManager _queueManager;
        [SerializeField] private CustomerSpawner _spawner;

        private CustomerQueuePosition _queuePosition;

        public Transform StartPoint => _startPoint;
        public Transform TradePoint => _tradePoint;
        public Transform ExitPoint => _exitPoint;

        private void Awake()
        {
            _queuePosition = new(_queueManager, this);
            _spawner.OnCustomerCreated += GoToTrade;
        }

        void GoToTrade(CustomerController customer)
        {
            _queueManager.Enqueue(customer);
            customer.transform.position = _startPoint.position;
            customer.StartMovementTo(_queuePosition.GetPosition());
            customer.OnCustomerGoToExit += GoToExit;
        }
        
        void GoToExit(CustomerController customer)
        {
            _queueManager.
                GetFirst().
                StartMovementTo(_exitPoint.position);
            
            int i = 0;
            foreach (var cstmr in _queueManager.QueueCollection)
            {
                cstmr.StartMovementTo(_queuePosition.GetPositionByPlaceInQueue(i++));
            }
        }
    }
}