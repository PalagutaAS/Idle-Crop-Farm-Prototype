using System.Collections.Generic;
using ObjectPull.ScriptableObjects;
using UnityEngine;

namespace ObjectPull
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private List<PoolConfigSO> _poolConfigs;
    
        private Dictionary<string, ObjectPool> _pools;
        private Dictionary<GameObject, string> _prefabToKey;
        
        private void Awake()
        {
            InitializePools();
        }

        private void InitializePools()
        {
            _pools = new Dictionary<string, ObjectPool>();
            _prefabToKey = new Dictionary<GameObject, string>();
        
            foreach (var config in _poolConfigs)
            {
                if (config.prefab == null) 
                {
                    Debug.LogWarning("Null prefab in pool configs, skipping...");
                    continue;
                }
            
                string key = config.prefab.name;
                _prefabToKey[config.prefab] = key;
            
                GameObject poolObject = new GameObject($"Pool_{key}");
                poolObject.transform.SetParent(transform);
            
                var pool = poolObject.AddComponent<ObjectPool>();
                pool.InitializeWithConfig(config);
            
                _pools[key] = pool;
            }
        
        }

        public GameObject GetObject(GameObject prefab)
        {
            if (_prefabToKey == null)
            {
                Debug.LogError("PoolManager not initialized!");
                return null;
            }
        
            if (_prefabToKey.TryGetValue(prefab, out string key) && 
                _pools.TryGetValue(key, out ObjectPool pool))
            {
                return pool.GetObject();
            }
        
            Debug.LogWarning($"No pool found for prefab: {prefab.name}. Creating new instance.");
            return Instantiate(prefab);
        }

        public T GetObject<T>(GameObject prefab) where T : Component
        {
            GameObject obj = GetObject(prefab);
            return obj != null ? obj.GetComponent<T>() : null;
        }

        public void ReturnObject(GameObject obj)
        {
            if (obj == null) return;
        
            var poolable = obj.GetComponent<PoolableObject>();
            if (poolable != null && poolable.Pool != null)
            {
                poolable.Pool.ReturnObject(obj);
            }
            else
            {
                Destroy(obj);
            }
        }

        public ObjectPool GetPool(GameObject prefab)
        {
            if (_prefabToKey.TryGetValue(prefab, out string key) && 
                _pools.TryGetValue(key, out ObjectPool pool))
            {
                return pool;
            }
            return null;
        }
    }
}