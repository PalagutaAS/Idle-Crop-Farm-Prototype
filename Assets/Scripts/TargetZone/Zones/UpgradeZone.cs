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
        
        protected void Awake()
        {
            NeedRefreshByOnClickButton();
        }

        protected override bool CanOpenPanel()
        {
            return _player != null;
        }

        protected override List<IInteractionCommand> GenerateCommands()
        {
            var commands = new List<IInteractionCommand>();
            var types = _libraryToolConfigs.GetUsingTypes();
            foreach (var type in types)
            {
                IToolConfig toolConfig = _libraryToolConfigs.GetConfigByLevel(type, 1);
                commands.Add(new BuyNewToolCommand(toolConfig));
            }
            
            List<ITool> currentTools = _player.Tools.GetAllTools();
            int countTool = currentTools.Count;

            for (int i = 0; i < countTool; i++)
            {
                var level = currentTools[i].CurrentLevel;
                IToolConfig toolConfig = _libraryToolConfigs.GetConfigByLevel(currentTools[i].Type,level + 1);
                if (toolConfig == null) continue;
                commands.Add(new UpgradeToolCommand(toolConfig, currentTools[i]));
            }

            return commands;
        }
    }
}
