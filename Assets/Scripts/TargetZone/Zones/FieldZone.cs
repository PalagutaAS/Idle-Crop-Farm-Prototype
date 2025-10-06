using System.Collections.Generic;
using Fields;
using Fields.ScriptableObjects;
using TargetZone.Command;
using TargetZone.Interfaces;
using VContainer;

namespace TargetZone.Zones
{
    public class FieldZone : BaseInteractionZone
    {
        [Inject] private LibraryFieldConfigs _libraryField;
        [Inject] private IFieldService _fieldService;

        protected void Awake()
        {
            NeedRefreshByChangedMoney();
        }
        
        protected override bool CanOpenPanel()
        {
            return _player != null;
        }

        protected override List<IInteractionCommand> GenerateCommands()
        {
            var commands = new List<IInteractionCommand>();
            foreach (var item in _libraryField.ConfigFields)
            {
                if (!_fieldService.HasInactiveField(item.Type)) continue;
                commands.Add(new BuyNewFieldCommand(item, _fieldService));
            }

            return commands;
        }
    }
}
