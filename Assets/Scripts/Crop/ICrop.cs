namespace Crop
{
    public interface ICrop
    {
        CropType Type { get; }
        int OnHarvest();
        void Grow();
        void Ripe();
    }
}

public enum CropType
{
    Wheat,
}