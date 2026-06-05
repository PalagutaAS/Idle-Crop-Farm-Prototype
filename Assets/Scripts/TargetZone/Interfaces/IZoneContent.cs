using System.Collections.Generic;
using Offers;

namespace TargetZone.Interfaces
{
    public interface IZoneContext
    {
        List<IInteractionCommand> Commands { get; }
        IOfferDisplayData OfferDisplayData { get; }
    }
}