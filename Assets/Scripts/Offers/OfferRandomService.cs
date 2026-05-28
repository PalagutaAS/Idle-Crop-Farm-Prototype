using System.Collections.Generic;
using System.Linq;
using Crops.ScriptableObjects;
using DefaultNamespace.Extensions;
using Fields;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Offers
{
    public class OfferRandomService : IOfferRandomService
    {
        private readonly LibraryCropConfigs _libraryCropConfigs;
        private readonly IFieldService _fieldService;
        private readonly int _maxLinesPerOffer;

        public OfferRandomService(LibraryCropConfigs libraryCropConfigs, IFieldService fieldService)
        {
            _libraryCropConfigs = libraryCropConfigs;
            _fieldService = fieldService;
            _maxLinesPerOffer = 4; //get from settings
        }
        
        public Offer GetRandom()
        {
            Dictionary<CropType, int> activeCropTypesToCountDict = _fieldService.GetActiveFieldCountPerCropType();

            List<CropType> availableTypes = activeCropTypesToCountDict.Keys.ToList();
            if (availableTypes.Count == 0)
                return Offer.Empty();
            
            
            int maxPossibleLines = Mathf.Min(availableTypes.Count, _maxLinesPerOffer);
            int numberOfLines = Random.Range(1, maxPossibleLines + 1);
            
            availableTypes.Shuffle();
            List<CropType> chosenTypes = availableTypes.GetRange(0, numberOfLines);
            
            var lines = new List<OfferLine>();

            foreach (CropType cropType in chosenTypes)
            {
                CropConfig config = _libraryCropConfigs.GetConfigByType(cropType);
                int activeCount = activeCropTypesToCountDict[cropType];

                int count = RandomCount(config, activeCount);
                int price = RandomPrice(config, count);
                lines.Add(new OfferLine(cropType, count, price));
            }

            return new Offer(lines, CalculateAdditionalPrice(lines));
        }

        private int CalculateAdditionalPrice(List<OfferLine> lines)
        {
            int basePrice = lines.Select(ol => ol.Price).Sum();
            int baseShift = Random.Range(-basePrice / 10, basePrice / 5);
            
            // Шанс "счастливого" оффера (5%), //get from settings
            bool isLucky = Random.Range(0f, 1f) < 0.05f;
            if (isLucky)
            {
                float multiplier = Mathf.LerpUnclamped(2.5f, 1.5f, Random.Range(0f, 1f));
                baseShift = Mathf.CeilToInt( Mathf.Abs(baseShift) * multiplier);
            }
            
            return baseShift;
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