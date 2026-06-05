using System;
using System.Collections.Generic;
using Fields;
using Fields.ScriptableObjects;
using Inventor;
using Player.Interface;
using TargetZone.Command;
using TargetZone.Interfaces;
using UnityEngine;
using VContainer;

namespace TargetZone.Zones
{
    public class FieldZoneLogic : BaseZoneLogic
    {
        [SerializeField] private CropType _type;
        private ILibraryFieldConfig _libraryField;
        private IFieldService _fieldService;
        private IWallet _wallet;
        private IPlayer _player;

        public CropType Type => _type;
        public override bool CanActivate => _player != null;
        public override event Action OnContextUpdated;
        
        [Inject]
        protected void Constructor(ILibraryFieldConfig config, IFieldService fieldService, IWallet wallet)
        {
            _wallet = wallet;
            _libraryField = config;
            _fieldService = fieldService;
            _wallet.OnChangedByTypeForUI += WalletOnOnChangedByTypeForUI;
        }

        private void WalletOnOnChangedByTypeForUI(MoneyType type, int current)
        {
            if (type == MoneyType.Coin)
            {
                NotifyContextUpdated();
            }
        }

        public override IZoneContext GenerateContext()
        {
            var commands = new List<IInteractionCommand>();
            if (!_fieldService.HasInactiveField(Type))
                return ZoneContext.EmptyContext();
            
            IFieldConfig fieldConfig = _libraryField.GetConfigByType(Type);
            commands.Add(new BuyNewFieldCommand(fieldConfig, _fieldService));

            return new ZoneContext(commands);
        }

        public override void HandleEnter(GameObject obj)
        {
            if (obj.TryGetComponent(out IPlayer player))
            {
                _player = player;
            }
            NotifyContextUpdated();
        }

        public override void HandleExit(GameObject obj)
        {
            if (obj.TryGetComponent(out IPlayer player))
            {
                _player = null;
            }
            NotifyContextUpdated();
        }
        
        private void NotifyContextUpdated() => OnContextUpdated?.Invoke();

        private void OnDestroy()
        {
            _wallet.OnChangedByTypeForUI -= WalletOnOnChangedByTypeForUI;
        }

        private void OnDisable()
        {
            _player = null;
            _wallet.OnChangedByTypeForUI -= WalletOnOnChangedByTypeForUI;
        }
    }
}
