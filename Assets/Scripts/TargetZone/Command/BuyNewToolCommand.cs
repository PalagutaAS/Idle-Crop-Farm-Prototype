using Player.Interface;
using TargetZone.Interfaces;

namespace TargetZone.Command
{
    public class BuyNewToolCommand : IInteractionCommand
    {
        public string Title { get; }
        public BuyNewToolCommand()
        {
            Title = "Buy New Tool: Pickaxe for 50";
        }

        public bool CanExecute(IPlayer player)
        {
            return player.Wallet.Count >= 50 && player.Tools.HasEmptySlot();
        }

        public void Execute(IPlayer player)
        {
            if (CanExecute(player))
            {
                player.Tools.TrySetupNewTool();
                player.Wallet.Payment(50);
            }
        }
    }
}