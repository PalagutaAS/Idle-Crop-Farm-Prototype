using AI;
using UnityEngine;

namespace TargetZone
{
    public class TradeZone : MonoBehaviour
    {
        [SerializeField] private CostumerSpawner _spawner;
        
        private CustomerController _currentCustomer;
        private ThirdPersonController _player;

        private void OnTriggerEnter(Collider other)
        {
            if (_currentCustomer == null && other.TryGetComponent(out CustomerController customer))
            {
                _currentCustomer = customer;
            }
            if (_player == null && other.TryGetComponent(out ThirdPersonController player))
            {
                _player = player;
            }

            if (_player != null && _currentCustomer != null)
            {
                TryDeal();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (_currentCustomer != null && other.TryGetComponent(out CustomerController customer))
            {
                _currentCustomer = null;
            }
            if (_player != null && other.TryGetComponent(out ThirdPersonController player))
            {
                _player = null;
            }
        }

        private void TryDeal()
        {
            var offer = _currentCustomer.Offer;
            if (_player.Inventory.Remove(offer.Type, offer.Count))
            {
                DealComplete();
                _player.CoinWallet.Payout(offer.Price);
            }
        }
        
        private void DealComplete()
        {
            _spawner.CustomerGoToExit();
        }
    }
}
