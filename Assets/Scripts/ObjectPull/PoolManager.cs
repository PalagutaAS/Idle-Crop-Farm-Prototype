using System.Collections.Generic;
using ObjectPull.ScriptableObjects;
using UnityEngine;
using VContainer.Unity;

namespace ObjectPull
{
    public class PoolManager : IInitializable, IPoolManager
    {
        private readonly ILibraryPoolConfigs _libraryPoolConfigs;
        private readonly Dictionary<string, IObjectPool> _pools;
        private readonly Dictionary<GameObject, string> _prefabToKey;
        private readonly Transform _transform;
        
        public PoolManager(ILibraryPoolConfigs libraryPoolConfigs)
        {
            _libraryPoolConfigs = libraryPoolConfigs;
            _pools = new Dictionary<string, IObjectPool>();
            _prefabToKey = new Dictionary<GameObject, string>();
            _transform = new GameObject("ObjectPool").transform;
        }

        public void Initialize()
        {
            foreach (var config in _libraryPoolConfigs.ListConfigs)
            {
                if (config.prefab == null) 
                {
                    Debug.LogWarning("Null prefab in pool configs, skipping...");
                    continue;
                }
            
                string key = config.prefab.name;
                _prefabToKey[config.prefab] = key;
            
                GameObject poolObject = new GameObject($"Pool_{key}");
                poolObject.transform.SetParent(_transform);
            
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
                _pools.TryGetValue(key, out IObjectPool pool))
            {
                return pool.GetObject();
            }
        
            Debug.LogWarning($"No pool found for prefab: {prefab.name}. Creating new instance.");
            return Object.Instantiate(prefab);
        }

        public T GetObject<T>(GameObject prefab) where T : Component
        {
            GameObject obj = GetObject(prefab);
            return obj != null ? obj.GetComponent<T>() : null;
        }

        public void ReturnObject(GameObject obj)
        {
            if (obj == null) return;
        
            var poolable = obj.GetComponent<IPoolableObject>();
            if (poolable != null && poolable.Pool != null)
            {
                poolable.Pool.ReturnObject(obj);
            }
            else
            {
                Object.Destroy(obj);
            }
        }

        public IObjectPool GetPool(GameObject prefab)
        {
            if (_prefabToKey.TryGetValue(prefab, out string key) && 
                _pools.TryGetValue(key, out IObjectPool pool))
            {
                return pool;
            }
            return null;
        }
    }

    public interface IPoolManager
    {
        public void ReturnObject(GameObject obj);
        public GameObject GetObject(GameObject prefab);
        public T GetObject<T>(GameObject prefab) where T : Component;
    }
}