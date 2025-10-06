using System.Collections;
using Crops;
using UnityEngine;

namespace Tools
{
    public class CropFinder
    {
        private readonly Tool _tool;
        private readonly LayerMask _layerMask;
        private readonly Collider[] _hitColliders;
        private readonly CropType _cropType;
        public CropFinder(Tool tool, LayerMask layerMask, CropType type)
        {
            _tool = tool;
            _layerMask = layerMask;
            _cropType = type;
            _hitColliders = new Collider[12];
        }
        
        public void CheckExistingColliders(Vector3 centerSphere)
        {
            int count = Physics.OverlapSphereNonAlloc(centerSphere, _tool.Radius, _hitColliders, _layerMask);
            if (count == 0) return;
            
            for (int i = 0; i < count; i++)
            {
                if (!_hitColliders[i].TryGetComponent(out ICrop crop) || !_cropType.HasFlag(crop.Type) || crop.IsHarvesting) continue;
                
                _tool.TriggerEnter(crop);
                return;
            }
        }

    }
}