using System;
using AI.StateMachine;
using Offers;
using UnityEngine;

namespace AI
{
    public class CustomerController : MonoBehaviour
    {
        [SerializeField] private CustomerStateMachine _csm;

        public CustomerStateMachine Csm => _csm;

        private CustomerState _state;
        private Vector3 _targetPosition;
        private Offer _offer;
        public Offer Offer => _offer;
        public bool isInit { get; private set; }
        
        public event Action<CustomerController> OnFinishDeal;

        public void Init()
        {
            isInit = true;
        }

        public void SetOffer(Offer newOffer)
        {
            _offer = newOffer;
        }

        public void StartMovementTo(Vector3 targetPosition)
        {
            _csm.ChangeState<MoveToPointState>(new CustomerPayload(targetPosition));
        }

        public void WaitForDeal(Vector3 nextTargetPosition)
        {
            _csm.ChangeState<WaitForDealState>(new CustomerPayload(nextTargetPosition, DealIsDone));
        }
        
        public void DealIsDone()
        {
            _offer.Done();
            OnFinishDeal?.Invoke(this);
        }
        
    }
}
