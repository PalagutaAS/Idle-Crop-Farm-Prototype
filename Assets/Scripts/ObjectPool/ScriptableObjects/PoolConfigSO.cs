using UnityEngine;

namespace ObjectPool.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Pool Config", menuName = "Object Pool/Pool Config")]
    public class PoolConfigSO : ScriptableObject
    {
        [Header("Pool Settings")]
        public GameObject prefab;
        public int initialSize = 10;
    }
}
