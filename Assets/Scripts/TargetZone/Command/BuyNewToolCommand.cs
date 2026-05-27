using Player.Interface;
using TargetZone.Interfaces;
using Tools.Interface;

namespace TargetZone.Command
{
    public class BuyNewToolCommand : IInteractionCommand
    {
        public string Title { get; }
        private IToolConfig _toolConfig;
        public BuyNewToolCommand(IToolConfig toolConfig)
        {
            _toolConfig = toolConfig;
            Title = $"Buy: {toolConfig.Type.ToString()} for {_toolConfig.Cost}";
        }

        public bool CanExecute(IPlayer player)
        {
            return player.Wallet.Count >= _toolConfig.Cost && !player.Tools.HasToolOfType(_toolConfig.Type) && player.Tools.HasEmptySlot();
        }

        public void Execute(IPlayer player)
        {
            if (CanExecute(player) && player.Tools.TrySetupNewTool(_toolConfig.Type))
            {
                player.Wallet.Payment(_toolConfig.Cost);
            }
        }
    }
}