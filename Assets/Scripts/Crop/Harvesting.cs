using UnityEngine;

namespace Crop
{
    public class Harvesting : MonoBehaviour
    {
        [SerializeField] private ThirdPersonController _player;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Crop crop)) return;
            int cropCount = crop.OnHarvest();
            _player.Inventory.Add(crop.Type, cropCount);
        }
    }
}
