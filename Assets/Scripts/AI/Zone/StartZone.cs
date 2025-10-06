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
            //Debug.Log("Enter Start Zone");
        }

        protected override void SendExitInvoke(CustomerController customer)
        {
            OnCustomerExitStartZone?.Invoke(customer);
            //Debug.Log("Exit Start Zone");
        }
    }
}