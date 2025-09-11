using UnityEngine;

namespace Crop
{
    public class Wheat : Crop
    {
        [SerializeField] private int _count;
        [SerializeField] private Grow _grow;
        
        private void Awake()
        {
            _grow = GetComponent<Grow>();
        }
        
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
            IsHarvesting = false;
            gameObject.SetActive(true);
        }
    }
}
