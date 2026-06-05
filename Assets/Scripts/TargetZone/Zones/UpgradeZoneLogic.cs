using System;
using System.Collections.Generic;
using Player.Interface;
using TargetZone.Command;
using TargetZone.Interfaces;
using Tools.Interface;
using Tools.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace TargetZone.Zones
{
    public class UpgradeZoneLogic : BaseZoneLogic
    {
        private ILibraryToolConfigs _libraryToolConfigs;
        private IToolManager _toolManager;
        private IPlayer _player;
        private bool _playerInside;
        
        public override bool CanActivate => _playerInside;

        public override event Action OnContextUpdated;

        [Inject]
        private void Constructor(IToolManager toolManager, ILibraryToolConfigs libraryToolConfigs, IPlayer player)
        {
            _toolManager = toolManager;
            _libraryToolConfigs = libraryToolConfigs;
            _player = player;
        }

        public override IZoneContext GenerateContext()
        {
            List<IInteractionCommand> commands = CreateByNewToolInteractionCommands();
            commands.AddRange(CreateUpgradeToolInteractionCommands());

            return new ZoneContext(commands);
        }

        public override void HandleEnter(GameObject obj)
        {
            _playerInside = obj.TryGetComponent<IPlayer>(out _);

            NotifyContextUpdated();
        }

        public override void HandleExit(GameObject obj)
        {
            _playerInside = (!obj.TryGetComponent<IPlayer>(out _));

            NotifyContextUpdated();
        }

        private List<IInteractionCommand> CreateUpgradeToolInteractionCommands()
        {
            List<IInteractionCommand> commands = new List<IInteractionCommand>();
            List<ITool> currentTools = _player.Tools.GetAllTools();

            for (int i = 0; i < currentTools.Count; i++)
            {
                var level = currentTools[i].CurrentLevel;
                IToolConfig toolConfig = _libraryToolConfigs.GetConfigByLevel(currentTools[i].Type, level + 1);
                if (toolConfig == null) continue;
                commands.Add(new UpgradeToolCommand(toolConfig, currentTools[i]));
            }
            return commands;
        }

        private List<IInteractionCommand> CreateByNewToolInteractionCommands()
        {
            List<IInteractionCommand> commands = new List<IInteractionCommand>();
            var types = _libraryToolConfigs.GetUsingTypes();

            foreach (var type in types)
            {
                if (_toolManager.HasToolOfType(type))
                    continue;
                IToolConfig toolConfig = _libraryToolConfigs.GetConfigByLevel(type, 1);
                commands.Add(new BuyNewToolCommand(toolConfig));
            }

            return commands;
        }
        
        private void NotifyContextUpdated() => OnContextUpdated?.Invoke();
    }
}
