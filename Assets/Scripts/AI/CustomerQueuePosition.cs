using UnityEngine;

namespace AI
{
    public class CustomerQueuePosition
    {
        private readonly QueueManager _queueManager;
        private readonly CustomerCoordinator _coordinator;
        
        public CustomerQueuePosition(QueueManager queueManager, CustomerCoordinator coordinator)
        {
            _queueManager = queueManager;
            _coordinator = coordinator;
        }

        public Vector3 GetPosition()
        {
            int queueCount = _queueManager.GetCount();
            Vector3 dir = (_coordinator.TradePoint.position - _coordinator.StartPoint.position).normalized;
            return _coordinator.TradePoint.position - (dir * _queueManager.OffsetBetween() * (queueCount - 1));
        }
        
        public Vector3 GetPositionByIndex(int index)
        {
            Vector3 tradePosition = _coordinator.TradePoint.position;
            Vector3 startPosition = _coordinator.StartPoint.position;
            Vector3 direction = (tradePosition - startPosition).normalized;
            float offset = _queueManager.OffsetBetween();
        
            return tradePosition - (direction * offset * index);
        }
        
    }
}