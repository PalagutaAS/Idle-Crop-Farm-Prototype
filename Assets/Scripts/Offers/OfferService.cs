using System.Collections.Generic;
using Fields;

namespace Offers
{
    public class OfferService
    {
        private readonly OfferRandomService _randomService;
        private readonly IFieldService _fieldService;

        public OfferService(OfferRandomService randomService, IFieldService fieldService)
        {
            _randomService = randomService;
            _fieldService = fieldService;
        }

        public Offer GetRandom()
        {
            Dictionary<CropType, int> listActiveCropFields = _fieldService.GetActiveCropType();
            
            return _randomService.GetRandomOffer(listActiveCropFields);
        }
    }
}