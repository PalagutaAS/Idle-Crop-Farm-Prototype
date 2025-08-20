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
                if (player.Wallet.Count >= _price)
                {
                    player.Tool.Upgrade();
                    player.Wallet.Payment(_price);
                }
            }

        }
    }
}
