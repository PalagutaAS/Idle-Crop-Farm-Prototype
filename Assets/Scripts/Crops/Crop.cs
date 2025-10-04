namespace Crops
{
    public class Crop : BaseCrop
    {
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

        public override void Ripe()
        {
            IsHarvesting = false;
            gameObject.SetActive(true);
        }
    }
}