using System.Collections.Generic;
using TargetZone.Command;
using TargetZone.Interfaces;
using Tools.Interface;
using Tools.ScriptableObjects;
using UnityEngine;

namespace TargetZone
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
            commands.Add(new BuyNewToolCommand());
            
            List<ITool> currentTools = _player.Tools.GetAllTools();
            int countTool = currentTools.Count;

            for (int i = 0; i < countTool; i++)
            {
                var level = currentTools[i].CurrentLevel;
                IToolConfig toolConfig = _libraryConfigs.GetConfigByLevel(level + 1);
                if (toolConfig == null) continue;
                commands.Add(new UpgradeToolCommand(toolConfig, currentTools[i]));
            }

            return commands;
        }
    }
}
