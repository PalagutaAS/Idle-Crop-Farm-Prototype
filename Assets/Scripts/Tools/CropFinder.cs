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
            _hitColliders = new Collider[12];
        }
        
        public void CheckExistingColliders(Vector3 centerSphere)
        {
            int count = Physics.OverlapSphereNonAlloc(centerSphere, _tool.Radius, _hitColliders, _layerMask);
            if (count == 0) return;
            
            for (int i = 0; i < count; i++)
            {
                if (!_hitColliders[i].TryGetComponent(out Crop.Crop crop)) continue;
                
                _tool.TriggerEnter(crop);
                return;
            }
        }

    }
}