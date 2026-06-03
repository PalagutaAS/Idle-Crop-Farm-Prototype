using UnityEngine;

namespace ObjectPool
{
    public class PoolableObject : MonoBehaviour, IPoolableObject
    {
        public IObjectPool Pool { get; private set; }

        public void Initialize(IObjectPool pool)
        {
            Pool = pool;
        }
    }

    public interface IPoolableObject
    {
        public IObjectPool Pool { get; }
        public void Initialize(IObjectPool pool);
    }
}