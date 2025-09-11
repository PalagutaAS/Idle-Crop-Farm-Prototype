using UI;
using UnityEngine;

namespace TargetZone
{
    public class UpgradeTool : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private UpgradeMenu _upgradeMenu;

        private ThirdPersonController _player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ThirdPersonController player))
            {
                _player = player;
                _upgradeMenu.Open();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ThirdPersonController player))
            {
                _player = null;
                _upgradeMenu.Close();
            }
        }

        public void Upgrade()
        {
            if (_player.Wallet.Count >= _price)
            {
                _player.Tool.Upgrade();
                _player.Wallet.Payment(_price);
            }
        }
    }
}
