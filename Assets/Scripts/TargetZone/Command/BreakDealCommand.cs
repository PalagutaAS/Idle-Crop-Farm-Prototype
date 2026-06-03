using Offers;
using Player.Interface;
using TargetZone.Interfaces;

namespace TargetZone.Command
{
    public class BreakDealCommand : IInteractionCommand
    {
        private readonly IOffer _offer;

        public BreakDealCommand(IOffer offer = null)
        {
            _offer = offer;
        }

        public string Title => "Break Deal";

        public bool CanExecute(IPlayer player = null)
        {
            return _offer.Active;
        }

        public void Execute(IPlayer player = null)
        {
            if (CanExecute())
            {
                _offer.CancelDeal();
            }
        }
    }
}