using AI;
using Player.Interface;
using TargetZone.Interfaces;

namespace TargetZone.Command
{
    public class BreakDealCommand : IInteractionCommand
    {
        private readonly CustomerController _customer;

        public BreakDealCommand(CustomerController customer = null)
        {
            _customer = customer;
        }

        public string Title => "Break Deal";

        public bool CanExecute(IPlayer player)
        {
            return _customer.Offer.Active;
        }

        public void Execute(IPlayer player)
        {
            if (CanExecute(player))
            {
                _customer.Offer.Done();
                _customer.ChangeState(CustomerState.GoAway);
            }
        }

    }
}