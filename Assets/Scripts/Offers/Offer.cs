using System;
using System.Collections.Generic;
using System.Linq;

namespace Offers
{
    public interface IOfferDisplayData
    {
        public IReadOnlyList<OfferLine> Lines { get; }
    }

    public interface IOfferCanceler
    {
        public void CancelDeal();
        public event Action<IOfferDisplayData> OnCancel;
    }

    public interface IOffer : IOfferCanceler, IOfferDisplayData
    {
        public CropType Types { get; }
        public int Price { get; }
        public int AdditionalPrice { get; }
        public bool Active { get; }
    }

    [System.Serializable]
    public class Offer : IOffer
    {
        public IReadOnlyList<OfferLine> Lines { get; private set; }
        public CropType Types { get; private set; }
        public int Price { get; private set; }
        public int AdditionalPrice { get; private set; }
        public bool Active { get; private set; }
        
        public event Action<IOfferDisplayData> OnCancel;
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
        
        private void Done() => Active = false;

        public void CancelDeal()
        {
            Done();
            OnCancel?.Invoke(this);
        }

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
