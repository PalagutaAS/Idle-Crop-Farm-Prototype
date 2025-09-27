using System.Collections.Generic;
using TargetZone.Command;
using TargetZone.Interfaces;

namespace TargetZone.Zones
{
    public class FieldZone : BaseInteractionZone
    {
        protected override bool CanOpenPanel()
        {
            return _player != null;
        }

        protected override List<IInteractionCommand> GenerateCommands()
        {
            var commands = new List<IInteractionCommand>();
            
            commands.Add(new BuyNewFieldCommand("A field of Potatoes for 1000"));
            commands.Add(new BuyNewFieldCommand("A field of Corn for 1500"));
            

            return commands;
        }
    }
}
