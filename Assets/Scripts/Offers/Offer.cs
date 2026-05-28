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
            Price = Lines.Select(ol => ol.Price).Sum() + AdditionalPrice;
            Types = (CropType) Lines.Select(ol => (int) ol.Type).Sum();
            Active = true;
        }

        public string GetDescription() => string.Join(", ", Lines.Select(l => $"{l.Count} {l.Type}")) + $"\n for {Price}";
        
        public void Done() => Active = false;

        public static Offer Empty() => new Offer(new List<OfferLine>(), 0);
    }
    
    [System.Serializable]
    public struct OfferLine
    {
        public OfferLine(CropType type, int count, int price)
        {
            Type = type;
            Count = count;
            Price = price;
        }

        public CropType Type { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }
    }
}
