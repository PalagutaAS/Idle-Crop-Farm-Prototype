using System.Collections.Generic;
using System.Linq;
using Crops.ScriptableObjects;
using UnityEngine;

namespace Offers
{
    public class OfferRandomService
    {
        private LibraryCropConfigs _libraryCropConfigs;
        
        public OfferRandomService(LibraryCropConfigs libraryCropConfigs)
        {
            _libraryCropConfigs = libraryCropConfigs;
        }
        
        public Offer GetRandomOffer(Dictionary<CropType, int> activeCropFields)
        {
            List<CropType> keysList = activeCropFields.Keys.ToList();
            CropType randomType = keysList[Random.Range(1, keysList.Count + 1) - 1];
            CropConfig config = _libraryCropConfigs.GetConfigByType(randomType);

            int activeCount = activeCropFields[randomType];
            
            int count = Mathf.RoundToInt(Random.Range(1 * (activeCount * .5f), 5 * (activeCount * .5f)));
            int price = Mathf.Max(count * config.Price - (Random.Range(1, config.Price)), config.Price);
            
            Debug.Log($"{randomType.ToString()} : {count} for {price}");
            return new Offer(randomType, count, price);
        }
    }
}