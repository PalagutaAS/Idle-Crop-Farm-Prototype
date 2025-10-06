using System.Collections.Generic;
using System.Linq;
using Crops.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Offers
{
    public class OfferRandomService
    {
        private readonly LibraryCropConfigs _libraryCropConfigs;
        
        public OfferRandomService(LibraryCropConfigs libraryCropConfigs)
        {
            _libraryCropConfigs = libraryCropConfigs;
        }
        
        public Offer GetRandomOffer(Dictionary<CropType, int> activeCropFields)
        {
            List<CropType> keysList = activeCropFields.Keys.ToList();
            if (keysList.Count == 0)
            {
                keysList.Add(CropType.Wheat);
            }
            CropType randomType = keysList[Random.Range(0, keysList.Count)];
            CropConfig config = _libraryCropConfigs.GetConfigByType(randomType);

            int activeCount = activeCropFields[randomType];
            
            int count = RandomCount(config, activeCount);
            int price = RandomPrice(config, count);
            
            return new Offer(randomType, count, price);
        }

        private int RandomCount(CropConfig config, int activeCount)
        {   
            int additional = Random.Range(-activeCount, activeCount * 2);
            int min = Random.Range(config.Count, activeCount + config.Count);
            int max = Random.Range(config.Count * 2, (activeCount + config.Count) * 2);
            int count = Mathf.Max(activeCount, Random.Range(min, max) + additional);
            return count;
        }
        private int RandomPrice(CropConfig config, int count)
        {
            int priceForAll = config.Price * count;
            int additional = Random.Range(-config.Price, 3 * config.Price);
            bool isLucky = Random.Range(0f, 1f) < 0.05f;
            if (isLucky)
            {
                float t = Mathf.Clamp01((priceForAll - 10) / 40f);
                float multiplier = Mathf.LerpUnclamped(2.5f, 1.5f, t);
                priceForAll = Mathf.CeilToInt(priceForAll * multiplier);
            }

            return Mathf.Max(1, Random.Range(priceForAll + additional, priceForAll));
        }
    }
}