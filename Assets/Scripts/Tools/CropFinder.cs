using UnityEngine;

namespace Tools
{
    public class CropFinder
    {
        private readonly Tool _tool;
        private readonly LayerMask _layerMask;
        private readonly Collider[] _hitColliders;
        public CropFinder(Tool tool, LayerMask layerMask)
        {
            _tool = tool;
            _layerMask = layerMask;
            _hitColliders = new Collider[10];
        }
        
        public void CheckExistingColliders(Vector3 centerSphere)
        {
            int count = Physics.OverlapSphereNonAlloc(centerSphere, _tool.Radius, _hitColliders, _layerMask);
            if (count == 0) return;
            
            foreach (var collider in _hitColliders)
            {
                if (!collider.TryGetComponent(out Crop.Crop crop) || crop.IsHarvesting) continue;
                
                _tool.TriggerEnter(crop);
                return;
            }
        }

    }
}