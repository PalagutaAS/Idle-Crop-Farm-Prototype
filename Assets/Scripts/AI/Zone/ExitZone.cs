using System;

namespace AI
{
    public class ExitZone : BaseZone
    {
        public event Action<CustomerController> OnCustomerEnterZone;
        public event Action<CustomerController> OnCustomerExitZone;

        protected override bool AdditionalConditionEnter(CustomerController customer)
        {
            return false;
        }

        protected override bool AdditionalConditionExit(CustomerController customer)
        {
            return false;
        }

        protected override void SendEnterInvoke(CustomerController customer)
        {
            OnCustomerEnterZone?.Invoke(customer);
        }

        protected override void SendExitInvoke(CustomerController customer)
        {
            OnCustomerExitZone?.Invoke(customer);
        }
    }
}