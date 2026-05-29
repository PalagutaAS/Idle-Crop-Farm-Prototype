using System.Collections.Generic;
using TargetZone.Command;
using TargetZone.Interfaces;
using Tools.Interface;
using Tools.ScriptableObjects;
using VContainer;

namespace TargetZone.Zones
{
    public class UpgradeZone : BaseInteractionZone
    {
        [Inject] private LibraryToolConfigs _libraryToolConfigs;
        [Inject] private IToolManager _toolManager;
        
        protected void Awake()
        {
            NeedRefreshByOnClickButton();
        }

        protected override bool CanOpenPanel()
        {
            return Player != null;
        }

        protected override List<IInteractionCommand> GenerateCommands()
        {
            var commands = CreateByNewToolInteractionCommands(new List<IInteractionCommand>());
            
            CreateUpgradeToolInteractionCommands(commands);

            return commands;
        }

        private void CreateUpgradeToolInteractionCommands(List<IInteractionCommand> commands)
        {
            List<ITool> currentTools = Player.Tools.GetAllTools();
            int countTool = currentTools.Count;

            for (int i = 0; i < countTool; i++)
            {
                var level = currentTools[i].CurrentLevel;
                IToolConfig toolConfig = _libraryToolConfigs.GetConfigByLevel(currentTools[i].Type, level + 1);
                if (toolConfig == null) continue;
                commands.Add(new UpgradeToolCommand(toolConfig, currentTools[i]));
            }
        }

        private List<IInteractionCommand> CreateByNewToolInteractionCommands(List<IInteractionCommand> commands)
        {
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
    }
}
