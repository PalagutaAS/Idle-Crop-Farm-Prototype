using UnityEngine;

namespace AI.StateMachine
{
    public class WaitForDealState : CustomerState
    {
        private CustomerPayload _payload;
        public override void Enter(CustomerPayload payload)
        {
            enabled = true;
            _payload = payload;
            Invoke(nameof(WaitingIsDone),5f);
        }
        public override void Exit()
        {
            CancelInvoke(nameof(WaitingIsDone));
            _payload.OnCompleted = null;
            enabled = false;
            _csm.ChangeState<MoveToPointState>(_payload);
        }

        public override void Update()
        {
            base.Update();
        }

        private void WaitingIsDone()
        {
            _payload.OnCompleted?.Invoke();
            Exit();
        }
    }
}