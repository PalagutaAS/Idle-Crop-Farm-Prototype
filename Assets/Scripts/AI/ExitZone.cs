using ObjectPull;
using UnityEngine;

namespace AI
{
    public class ExitZone : MonoBehaviour
    {
        [SerializeField] private PoolManager _poolManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CustomerController customer) && customer.State == CustomerState.GoAway)
            {
                _poolManager.ReturnObject(customer.gameObject);
            }
        }
    }
}