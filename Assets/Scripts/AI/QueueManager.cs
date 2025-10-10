using UnityEngine;
using System.Collections.Generic;
using AI.ScriptableObjects;

namespace AI
{
    public class QueueManager : MonoBehaviour
    {
        [SerializeField] private QueueConfig _queueConfig;

        private Queue<CustomerController> _queue;

        public Queue<CustomerController> QueueCollection => _queue;
        
        private void Awake()
        {
            _queue = new Queue<CustomerController>(_queueConfig.InitCount);
        }

        public float OffsetBetween() => _queueConfig.Offset;
        
        public CustomerController GetFirst() => _queue.Dequeue();

        public int GetCount() => _queue.Count;

        public int Enqueue(CustomerController customerController)
        {
            _queue.Enqueue(customerController);
            return GetCount();
        }

        public bool IsFirstInQueue(CustomerController customer)
        {
            if (_queue.Count == 0)
                return false;
            
            return _queue.Peek() == customer;
        }
    }
}
