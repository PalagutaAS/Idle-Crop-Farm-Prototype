using UnityEngine;

namespace Crop
{
    public class Grow : MonoBehaviour
    {
        [SerializeField] private int _growTime = 10;

        private Crop _crop;
        private void Awake()
        {
            _crop = GetComponent<Crop>();
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
