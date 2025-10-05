using System.Collections.Generic;
using Fields;
using Fields.ScriptableObjects;
using TargetZone.Command;
using TargetZone.Interfaces;
using UnityEngine;
using VContainer;

namespace TargetZone.Zones
{
    public class FieldZone : BaseInteractionZone
    {
        [Inject] private ConfigLibraryFieldsByType _libraryFieldConfig;
        [SerializeField] private FieldService _fieldService;

        protected override void Awake()
        {
            base.Awake();
            NeedRefreshByChangedMoney();
        }
        
        private void NeedRefreshByChangedMoney()
        {
            _wallet.OnChangedByType += RefreshPanelByChange;
        }
        
        protected override bool CanOpenPanel()
        {
            return _player != null;
        }

        protected override List<IInteractionCommand> GenerateCommands()
        {
            var commands = new List<IInteractionCommand>();
            foreach (var item in _libraryFieldConfig.ConfigFields)
            {
                if (!_fieldService.HasInactiveField(item.Type)) continue;
                commands.Add(new BuyNewFieldCommand(item, _fieldService));
            }

            return commands;
        }
    }
}
