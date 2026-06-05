using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Pool Config List", menuName = "Object Pool/Pool Config List")]

    public class LibraryPoolConfigs : ScriptableObject, ILibraryPoolConfigs
    {
        [SerializeField] private List<PoolConfigSO> _poolConfigs;
        public List<PoolConfigSO> ListConfigs => _poolConfigs;
    }

    public interface ILibraryPoolConfigs
    {
        public List<PoolConfigSO> ListConfigs { get; }
    }
}