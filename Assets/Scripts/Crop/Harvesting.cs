using UnityEngine;

namespace Crop
{
    public class Harvesting : MonoBehaviour
    {

        private Tool _tool;
        private void Awake()
        {
            _tool = GetComponent<Tool>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent(out Crop crop)) return;

            _tool.CropHarvest(crop);
        }
    }
}
