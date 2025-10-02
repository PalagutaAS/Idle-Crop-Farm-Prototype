using UnityEngine;

namespace Offers
{
    public class OfferRandomService
    {
        public Offer GetRandomOffer()
        {
            int count = Random.Range(1,5);
            int price = Mathf.Max(count * 3 - Random.Range(1, 5), 1);
            return new Offer(CropType.Wheat, count, price);
        }
    }
}