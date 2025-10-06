using System;
using UnityEngine;

namespace AI
{
    public class TradeZone : BaseZone
    {
        public event Action<CustomerController> OnCustomerEnterTradeZone;
        public event Action<CustomerController> OnCustomerExitTradeZone;

        protected override void SendEnterInvoke(CustomerController customer)
        {
            OnCustomerEnterTradeZone?.Invoke(customer);
        }

        protected override void SendExitInvoke(CustomerController customer)
        {
            OnCustomerExitTradeZone?.Invoke(customer);
        }
        
    }
}