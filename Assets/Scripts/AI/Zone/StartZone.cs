using System;

namespace AI
{
    public class StartZone : BaseZone
    {
        public event Action<CustomerController> OnCustomerEnterStartZone;
        public event Action<CustomerController> OnCustomerExitStartZone;

        protected override void SendEnterInvoke(CustomerController customer)
        {
            OnCustomerEnterStartZone?.Invoke(customer);
        }

        protected override void SendExitInvoke(CustomerController customer)
        {
            OnCustomerExitStartZone?.Invoke(customer);
        }
    }
}