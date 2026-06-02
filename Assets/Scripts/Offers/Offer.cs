using System.Collections.Generic;
using System.Linq;

namespace Offers
{
    [System.Serializable]
    public class Offer
    {
        public IReadOnlyList<OfferLine> Lines { get; private set; }
        public CropType Types { get; private set; }
        public int Price { get; private set; }
        public int AdditionalPrice { get; private set; }
        public bool Active { get; private set; }
        
        public Offer(IEnumerable<OfferLine> lines, int additionalPrice)
        {
            Lines = lines.ToList().AsReadOnly();
            AdditionalPrice = additionalPrice;
            
            int totalPrice = 0;
            int totalTypes = 0;
            foreach (OfferLine line in Lines)
            {
                totalPrice += line.Price;
                totalTypes += (int)line.Type;
            }
            
            Price = totalPrice + AdditionalPrice;
            Types = (CropType) totalTypes;
            Active = true;
        }
        
        public void Done() => Active = false;

        public static Offer Empty() => new Offer(new List<OfferLine>(), 0);
    }
    
    [System.Serializable]
    public struct OfferLine
    {
        public readonly CropType Type;
        public readonly int Count;
        public readonly int Price;
        
        public OfferLine(CropType type, int count, int price)
        {
            Type = type;
            Count = count;
            Price = price;
        }
    }
}
