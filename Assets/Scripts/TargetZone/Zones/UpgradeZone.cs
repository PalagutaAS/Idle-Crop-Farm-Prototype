using System.Collections.Generic;
using TargetZone.Command;
using TargetZone.Interfaces;
using Tools;
using Tools.Interface;
using Tools.ScriptableObjects;
using UnityEngine;

namespace TargetZone.Zones
{
    public class UpgradeZone : BaseInteractionZone
    {
        [SerializeField] private LibraryConfigsByLevel _libraryConfigs;
        
        protected override void Awake()
        {
            base.Awake();
            NeedRefreshByOnClickButton();
        }

        private void NeedRefreshByOnClickButton()
        {
            _panel.OnClickButton += RefreshPanel;
        }

        protected override bool CanOpenPanel()
        {
            return _player != null;
        }

        protected override List<IInteractionCommand> GenerateCommands()
        {
            var commands = new List<IInteractionCommand>();
            var types = _libraryConfigs.GetUsingTypes();
            foreach (var type in types)
            {
                IToolConfig toolConfig = _libraryConfigs.GetConfigByLevel(type, 1);
                commands.Add(new BuyNewToolCommand(toolConfig));
            }
            
            
            List<ITool> currentTools = _player.Tools.GetAllTools();
            int countTool = currentTools.Count;

            for (int i = 0; i < countTool; i++)
            {
                var level = currentTools[i].CurrentLevel;
                IToolConfig toolConfig = _libraryConfigs.GetConfigByLevel(currentTools[i].Type,level + 1);
                if (toolConfig == null) continue;
                commands.Add(new UpgradeToolCommand(toolConfig, currentTools[i]));
            }

            return commands;
        }
    }
}
