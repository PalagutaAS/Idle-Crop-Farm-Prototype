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
        }

        public string Title => $"Sell {_customer.Offer.Count} {_customer.Offer.Type}";

        public bool CanExecute(IPlayer player, CustomerController customer = null)
        {
            var offer = _customer.Offer;
            return _customer.Offer.Active && player.Inventory.Check(offer.Type, offer.Count);
        }

        public void Execute(IPlayer player, CustomerController customer = null)
        {
            if (CanExecute(player))
            {
                player.Wallet.Payout(_customer.Offer.Price);
                _customer.Offer.Done();
                _customer.ChangeState();
            }
        }

    }
}