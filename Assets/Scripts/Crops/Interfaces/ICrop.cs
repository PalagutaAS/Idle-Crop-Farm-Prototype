namespace Crops
{
    public interface ICrop
    {
        public CropType Type { get; }
        public int OnHarvest();
        public bool IsHarvesting { get; }
        public void PreparingForHarvest();
        public void Grow();
        public void Ripe();
    }
}