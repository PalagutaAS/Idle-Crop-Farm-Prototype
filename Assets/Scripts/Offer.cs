using UnityEngine;

[System.Serializable]
public class Offer
{
    
    [field: SerializeField] public CropType Type { get; private set; }
    [field: SerializeField] public int Count { get; private set; }
    [field: SerializeField] public int Price { get; private set; }

    
    public Offer(CropType type, int count, int price)
    {
        Type = type;
        Count = count;
        Price = price;
    }


}
