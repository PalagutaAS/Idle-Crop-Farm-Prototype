using System.Collections.Generic;
using System.Linq;
using Crops.ScriptableObjects;
using Fields;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Offers
{
    public class OfferRandomService : IOfferRandomService
    {
        private readonly LibraryCropConfigs _libraryCropConfigs;
        private readonly IFieldService _fieldService;

        public OfferRandomService(LibraryCropConfigs libraryCropConfigs, IFieldService fieldService)
        {
            _libraryCropConfigs = libraryCropConfigs;
            _fieldService = fieldService;
        }
        
        public Offer GetRandom()
        {
            Dictionary<CropType, int> activeCropTypesToCountDict = _fieldService.GetActiveFieldCountPerCropType();
                
            List<CropType> keysList = activeCropTypesToCountDict.Keys.ToList();
            if (keysList.Count == 0)
            {
                keysList.Add(CropType.Wheat);
            }
            CropType randomType = keysList[Random.Range(0, keysList.Count)];
            CropConfig config = _libraryCropConfigs.GetConfigByType(randomType);

            int activeCount = (activeCropTypesToCountDict.Count == 0) ? 1 : activeCropTypesToCountDict[randomType];
            
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

    public interface IOfferRandomService
    {
        public Offer GetRandom();
    }
}