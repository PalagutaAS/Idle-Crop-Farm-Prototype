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
            Title = $"Sell {_customer.Offer.Count} {_customer.Offer.Type} for {_customer.Offer.Price}";
        }
        public string Title { get; }

        public bool CanExecute(IPlayer player)
        {
            var offer = _customer.Offer;
            return _customer.Offer.Active && player.Inventory.Check(offer.Type, offer.Count);
        }

        public void Execute(IPlayer player)
        {
            if (CanExecute(player))
            {
                var offer = _customer.Offer;
                player.Wallet.Payout(_customer.Offer.Price);
                player.Inventory.Remove(offer.Type, offer.Count);
                offer.Done();
                _customer.ChangeState(CustomerState.Leaving);
            }
        }

    }
}