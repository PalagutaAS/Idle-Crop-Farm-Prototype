using UnityEngine;

namespace ObjectPull
{
    public class PoolableObject : MonoBehaviour
    {
        public ObjectPool Pool { get; private set; }

        public void Initialize(ObjectPool pool)
        {
            Pool = pool;
        }

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