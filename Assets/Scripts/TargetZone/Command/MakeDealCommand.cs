using System.Linq;
using AI;
using Player.Interface;
using TargetZone.Interfaces;

namespace TargetZone.Command
{
    public class MakeDealCommand : IInteractionCommand
    {
        private readonly CustomerController _customer;

        public MakeDealCommand(CustomerController customer)
        {
            _customer = customer;
            Title = $"Sell for {_customer.Offer.Price}";
        }
        public string Title { get; }

        public bool CanExecute(IPlayer player)
        {
            var offer = _customer.Offer;
            return offer.Active && offer.Lines.All(line => player.Inventory.HasEnoughByCrop(line.Type, line.Count));
        }

        public void Execute(IPlayer player)
        {
            if (CanExecute(player))
            {
                var offer = _customer.Offer;
                player.Wallet.Payout(offer.Price);
                offer.Lines.All(line => player.Inventory.Remove(line.Type, line.Count));
                _customer.CancelDeal();
            }
        }
    }
}