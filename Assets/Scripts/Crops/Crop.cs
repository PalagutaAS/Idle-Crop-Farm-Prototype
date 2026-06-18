using Crops.Animations;
using UnityEngine;

namespace Crops
{
    public class Crop : BaseCrop
    {
        [SerializeField] private CropGrowthAnimator _growthAnimator;
        private void OnEnable()
        {
            IsHarvesting = false;
            RandomRotation();
        }

        public override void PreparingForHarvest()
        {
            IsHarvesting = true;
        }

        public override int OnHarvest()
        {
            gameObject.SetActive(false);
            Grow();
            return _config.Count;
        }

        public override void Grow()
        {
            Invoke(nameof(Ripe), _config.GrowTime);
        }

        private void RandomRotation()
        {
            transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        }

        public override void Ripe()
        {
            IsHarvesting = false;
            RandomRotation();
            _growthAnimator.PlayGrowAnimation();
            gameObject.SetActive(true);
        }
    }
}