using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(BoxCollider))]
    public class BaseZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CustomerController customer) && AdditionalConditionEnter(customer))
            {
                SendEnterInvoke(customer);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CustomerController customer) && AdditionalConditionExit(customer))
            {
                SendExitInvoke(customer);
            }
        }

        protected virtual bool AdditionalConditionEnter(CustomerController customer) => true;
        
        protected virtual bool AdditionalConditionExit(CustomerController customer) => true;

        protected virtual void SendEnterInvoke(CustomerController customer) { }

        protected virtual void SendExitInvoke(CustomerController customer) { }
        
    }
}