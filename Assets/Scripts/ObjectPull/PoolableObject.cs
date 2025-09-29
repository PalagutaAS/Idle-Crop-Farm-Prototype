using UnityEngine;

namespace ObjectPull
{
    public class PoolableObject : MonoBehaviour
    {
        //[System.Serializable]
        //public class PoolEvents
        //{
        //    public UnityEvent onGetFromPool;
        //    public UnityEvent onReturnToPool;
        //}
    
        //[SerializeField] private PoolEvents _events;
    
        public ObjectPool Pool { get; private set; }

        public void Initialize(ObjectPool pool)
        {
            Pool = pool;
        }

        //public void OnGetFromPool()
        //{
        //    _events?.onGetFromPool?.Invoke();
        //}
        //public void OnReturnToPool()
        //{
        //    _events?.onReturnToPool?.Invoke();
        //}

        [ContextMenu("Return To Pool")]
        public void ReturnToPool()
        {
            if (Pool != null)
            {
                Pool.ReturnObject(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}