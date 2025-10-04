using Fields;
using Fields.ScriptableObjects;
using Player.Interface;
using TargetZone.Interfaces;

namespace TargetZone.Command
{
    public class BuyNewFieldCommand : IInteractionCommand
    {
        public string Title { get; }
        private readonly IFieldConfig _config;
        private readonly FieldService _fieldService;

        public BuyNewFieldCommand(IFieldConfig config, FieldService fieldService)
        {
            _config = config;
            _fieldService = fieldService;
            Title = $"Buy: {_config.Type.ToString()} for {_config.Price}";
        }
        public bool CanExecute(IPlayer player)
        {
            return player.Wallet.Count >= _config.Price;
        }

        public void Execute(IPlayer player)
        {
            if (CanExecute(player))
            { 
                _fieldService.OpenField(_config.Type);
                player.Wallet.Payment(_config.Price);
            }
        }

    }
}