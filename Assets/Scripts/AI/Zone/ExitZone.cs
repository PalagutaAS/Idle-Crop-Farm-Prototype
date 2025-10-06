using System;
using UnityEngine;

namespace AI
{
    public class ExitZone : BaseZone
    {
        public event Action<CustomerController> OnCustomerEnterZone;
        public event Action<CustomerController> OnCustomerExitZone;

        protected override bool AdditionalConditionEnter(CustomerController customer)
        {
            return customer.State == CustomerState.Leaving;
        }

        protected override bool AdditionalConditionExit(CustomerController customer)
        {
            return customer.State == CustomerState.Leaving;
        }

        protected override void SendEnterInvoke(CustomerController customer)
        {
            OnCustomerEnterZone?.Invoke(customer);
            //Debug.Log("Enter Exit Zone");
        }

        protected override void SendExitInvoke(CustomerController customer)
        {
            OnCustomerExitZone?.Invoke(customer);
            //Debug.Log("Exit Exit Zone");
        }
    }
}