using System.Collections.Generic;
using Offers;
using TargetZone.Interfaces;

namespace TargetZone
{
    public class ZoneContext : IZoneContext
    {
        public List<IInteractionCommand> Commands { get; }
        public IOfferDisplayData OfferDisplayData { get; }

        public ZoneContext(List<IInteractionCommand> commands, IOfferDisplayData offerDisplayData = null)
        {
            Commands = commands;
            OfferDisplayData = offerDisplayData;
        }

        private ZoneContext()
        {
            Commands = new List<IInteractionCommand>();
            OfferDisplayData = null;
        }

        public static ZoneContext EmptyContext()
        {
            return new ZoneContext();
        }
    }
}