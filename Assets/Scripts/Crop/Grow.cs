using System;
using UnityEngine;

namespace Crop
{
    public class Grow : MonoBehaviour
    {
        [SerializeField] private int _growTime = 10;

        private ICrop _crop;
        private void Awake()
        {
            _crop = GetComponent<ICrop>();
        }

        public void StartGrow()
        {
            Invoke(nameof(FinishGrow), 5f);
        }

        private void FinishGrow()
        {
            _crop.Ripe();
        }
    }
}
