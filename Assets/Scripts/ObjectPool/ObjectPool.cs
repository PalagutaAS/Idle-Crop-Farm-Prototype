using System.Collections.Generic;
using ObjectPool.ScriptableObjects;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPool : MonoBehaviour, IObjectPool
    {
        [SerializeField] private PoolConfigSO _config;
    
        private Queue<GameObject> _pool;
        private List<GameObject> _activeObjects;
        
        public void InitializeWithConfig(PoolConfigSO config)
        {
            if (config == null)
            {
                Debug.LogError("Config is null!");
                return;
            }
        
            _config = config;
            
            _pool = new Queue<GameObject>();
            _activeObjects = new List<GameObject>();
        
            for (int i = 0; i < _config.initialSize; i++)
            {
                CreateNewObject();
            }
        }

        private GameObject CreateNewObject()
        {
            GameObject obj = Instantiate(_config.prefab, transform);
            obj.SetActive(false);
        
            var poolable = obj.GetComponent<IPoolableObject>() ?? obj.AddComponent<PoolableObject>();
            poolable.Initialize(this);
        
            _pool.Enqueue(obj);
            return obj;
        }

        public GameObject GetObject()
        {
            GameObject obj = (_pool.Count <= 0) ? CreateNewObject() : null;
            
            obj = _pool.Dequeue();
            obj.SetActive(true);
            _activeObjects.Add(obj);
             
            return obj;
        }

        public T GetObject<T>() where T : Component
        {
            GameObject obj = GetObject();
            return obj != null ? obj.GetComponent<T>() : null;
        }

        public void ReturnObject(GameObject obj)
        {
            if (obj == null || !_activeObjects.Contains(obj)) return;
        
            obj.SetActive(false);
            obj.transform.SetParent(transform);
        
            _activeObjects.Remove(obj); 
            _pool.Enqueue(obj);
        }
    }

    public interface IObjectPool
    {
        public void InitializeWithConfig(PoolConfigSO config);
        public T GetObject<T>() where T : Component;
        public GameObject GetObject();
        public void ReturnObject(GameObject obj);
        
    }
}