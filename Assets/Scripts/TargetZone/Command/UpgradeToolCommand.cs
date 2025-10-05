using Player.Interface;
using TargetZone.Interfaces;
using Tools.Interface;

namespace TargetZone.Command
{
    public class UpgradeToolCommand : IInteractionCommand
    {
        private readonly IToolConfig _configByLevel;
        private readonly ITool _tool;
        public string Title { get; }

        public UpgradeToolCommand(IToolConfig configByLevels, ITool tool)
        {
            _configByLevel = configByLevels;
            _tool = tool;
            Title = $"Upgrade {tool.Type.ToString()} {tool.CurrentLevel+1} lvl : {_configByLevel.Cost}";
        }

        public bool CanExecute(IPlayer player)
        {
            return (player.Wallet.Count >= _configByLevel.Cost);
            //TO DO ckeck max lvl tool;
        }

        public void Execute(IPlayer player)
        {
            if (CanExecute(player))
            {
                _tool.Upgrade(_configByLevel);
                player.Wallet.Payment(_configByLevel.Cost);
            }
        }
    }
}