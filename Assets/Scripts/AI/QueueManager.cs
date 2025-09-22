using UnityEngine;
using System.Collections.Generic;

namespace AI
{
    public class QueueManager : MonoBehaviour
    {
        [SerializeField] private int _count = 10;
        [field: SerializeField] public int OffsetBetween { get; private set; }

        private Queue<CustomerController> _queue;

        public Queue<CustomerController> QueueCollection  => _queue;
        
        private void Awake()
        {
            _queue = new Queue<CustomerController>(_count);
        }

        public CustomerController GetFirst()
        {
            return _queue.Dequeue();
        }

        public int GetCount()
        {
            return _queue.Count;
        }

        public int Enqueue(CustomerController customerController)
        {
            _queue.Enqueue(customerController);
            return GetCount();
        }

    }
}
