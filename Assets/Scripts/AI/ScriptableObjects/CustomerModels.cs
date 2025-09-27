using UnityEngine;

namespace AI.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Customer Models", menuName = "Custom/Customers/Customer Models")]
    public class CustomerModels : ScriptableObject
    {
        [SerializeField] private GameObject[] _models;

        public GameObject GetRandomModel()
        {
            return _models[Random.Range(0, _models.Length)];
        }
    }
}