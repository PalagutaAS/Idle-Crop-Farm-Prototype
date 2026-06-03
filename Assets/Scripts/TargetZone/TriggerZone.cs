using System;
using UnityEngine;

namespace TargetZone
{
    public class TriggerZone : MonoBehaviour
    {
        public event Action<GameObject> OnEnter;
        public event Action<GameObject> OnExit;

        private void OnTriggerEnter(Collider other) => OnEnter?.Invoke(other.gameObject);
        private void OnTriggerExit(Collider other) => OnExit?.Invoke(other.gameObject);
    }
}