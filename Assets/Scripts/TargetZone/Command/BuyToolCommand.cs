using AI;
using Player.Interface;
using TargetZone.Interfaces;

namespace TargetZone.Command
{
    public class BuyToolCommand : IInteractionCommand
    {
        public string Title => "Buy Tool";
        private int _price = 10;

        public bool CanExecute(IPlayer player, CustomerController customer = null)
        {
            throw new System.NotImplementedException();
        }

        public void Execute(IPlayer player, CustomerController customer = null)
        {
            if (player.Wallet.Count >= _price)
            {
                //_player.Tools.Upgrade();
                player.Wallet.Payment(_price);
            }
        }
    }
}