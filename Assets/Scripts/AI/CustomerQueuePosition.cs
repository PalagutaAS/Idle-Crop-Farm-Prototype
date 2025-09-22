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
            return _coordinator.TradePoint.position - (dir * _queueManager.OffsetBetween * (queueCount - 1));
        }
        
        public Vector3 GetPositionByPlaceInQueue(int place)
        {
            int queueCount = _queueManager.GetCount();
            Vector3 dir = (_coordinator.TradePoint.position - _coordinator.StartPoint.position).normalized;
            return _coordinator.TradePoint.position - (dir * _queueManager.OffsetBetween * (queueCount - (queueCount - place)));
        }
        
    }
}