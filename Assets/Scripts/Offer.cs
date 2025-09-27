[System.Serializable]
public class Offer
{
    public CropType Type { get; private set; }
    public int Count { get; private set; }
    public int Price { get; private set; }
    public bool Active { get; private set; }

    
    public Offer(CropType type, int count, int price)
    {
        Type = type;
        Count = count;
        Price = price;
        Active = true;
    }

    public void Done()
    {
        Active = false;
    }


}
