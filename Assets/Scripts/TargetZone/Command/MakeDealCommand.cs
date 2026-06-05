using System.Linq;
using Offers;
using Player.Interface;
using TargetZone.Interfaces;

namespace TargetZone.Command
{
    public class MakeDealCommand : IInteractionCommand
    {
        private readonly IOffer _offer;
        public string Title { get; }

        public MakeDealCommand(IOffer offer)
        {
            _offer = offer;
            Title = $"Sell for {_offer.Price}";
        }

        public bool CanExecute(IPlayer player)
        {
            return _offer.Active && _offer.Lines.All(line => player.Inventory.HasEnoughByCrop(line.Type, line.Count));
        }

        public void Execute(IPlayer player)
        {
            if (CanExecute(player))
            {
                _offer.CancelDeal();
                player.Wallet.Payout(_offer.Price);
                _offer.Lines.All(line => player.Inventory.Remove(line.Type, line.Count));
            }
        }
    }
}