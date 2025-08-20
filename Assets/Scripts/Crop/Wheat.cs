using UnityEngine;

namespace Crop
{
    public class Wheat : Crop, ICrop
    {
        [SerializeField] private int _count;
        [SerializeField] private Grow _grow;
        
        private void Awake()
        {
            _grow = GetComponent<Grow>();
        }

        [field: SerializeField] public CropType Type { get; private set; }
        
        public override int OnHarvest()
        {
            gameObject.SetActive(false);
            Grow();
            return _count;
        }

        public override void Grow()
        {
            _grow.StartGrow();
        }

        public override void Ripe()
        {
            gameObject.SetActive(true);
        }
    }
}
