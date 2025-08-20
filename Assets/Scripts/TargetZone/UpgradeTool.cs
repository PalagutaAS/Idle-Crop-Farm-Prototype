using UnityEditor.Rendering;
using UnityEngine;

namespace TargetZone
{
    public class UpgradeTool : MonoBehaviour
    {
        [SerializeField] private int _price;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ThirdPersonController player))
            {
                if (player.CoinWallet.Count >= _price)
                {
                    player.Tool.Upgrade();
                    player.CoinWallet.Payment(_price);
                }
            }

        }
    }
}
