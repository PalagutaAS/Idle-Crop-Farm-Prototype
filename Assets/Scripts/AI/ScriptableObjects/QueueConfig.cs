using UnityEngine;

namespace AI.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Queue Config", menuName = "Custom/Customers/Queue Config")]

    public class QueueConfig : ScriptableObject
    {
        [SerializeField] private int _initCount;
        [SerializeField] private float _offset;
        
        public int InitCount => _initCount;
        public float Offset => _offset;
    }
}