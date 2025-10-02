using System.Collections.Generic;
using UnityEngine;

namespace ObjectPull.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Pool Config List", menuName = "Object Pool/Pool Config List")]

    public class PoolConfigsSO : ScriptableObject
    {
        [SerializeField] private List<PoolConfigSO> _poolConfigs;
        public List<PoolConfigSO> List => _poolConfigs;
        
    }
}