using Fields;
using Infrastructure.PersistenceProgress;
using Inventor;
using Logging;
using SavesData;
using TargetZone;
using Tools.Interface;
using YG;

namespace Infrastructure.StateMachine
{
    public class ApplyGameProgressState : IState
    {
        private readonly IInventoryChanger _inventory;
        private readonly IWallet _wallet;
        private readonly IPersistenceProgressService _progressService;
        private readonly IFieldService _fieldService;
        private readonly IToolManager _toolManager;
        private readonly FieldTriggerZoneMover _triggerZoneMover;
        private readonly ScreenLoading _screenLoading;
        private readonly IStateSwitcher _stateMachine;
        
        public ApplyGameProgressState(IStateSwitcher gameStateMachine, ScreenLoading screenLoading, IInventoryChanger inventory, IWallet wallet, IPersistenceProgressService progressService, IFieldService fieldService, IToolManager toolManager, FieldTriggerZoneMover triggerZoneMover)
        {
            _progressService = progressService;
            _fieldService = fieldService;
            _toolManager = toolManager;
            _triggerZoneMover = triggerZoneMover;
            _screenLoading = screenLoading;
            _stateMachine = gameStateMachine;
            _inventory = inventory;
            _wallet = wallet;
        }

        public void Enter()
        {
            this.Log("");
            ApplyGameProgress();
            _triggerZoneMover.MoveTriggers();

            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            _screenLoading.Hide();
            YG2.GameReadyAPI();
        }

        private void ApplyGameProgress()
        {
            ApplyInventoryData(_progressService.Progress.InventoryData);
            ApplyFieldsData(_progressService.Progress.FieldData);
            ApplyToolsData(_progressService.Progress.ToolsData);
            ApplyMoneyData(_progressService.Progress.WalletData);
        }

        private void ApplyMoneyData(WalletData data)
        {
            if (data?.Money == null) 
                return;
            
            foreach (var kvp in data.Money)
            {
                if (kvp.Key == MoneyType.Coin)
                {
                    _wallet.Payout(kvp.Value);
                }
            }
                
        }

        private void ApplyToolsData(ToolsData data)
        {
            if (data?.Tools == null) 
                return;

            foreach (var kvp in data.Tools)
            {
                _toolManager.TrySetupNewTool(kvp.Key, kvp.Value);
            }
        }

        private void ApplyFieldsData(FieldsData data)
        {
            if (data?.Fields == null) 
                return;

            foreach (var kvp in data.Fields)
            {
                for (int i = 0; i < kvp.Value; i++)
                {
                    _fieldService.OpenField(kvp.Key);
                }
            }
        }

        private void ApplyInventoryData(InventoryData data)
        {
            if (data?.Crops == null) 
                return;
            
            foreach (var kvp in data.Crops)
                _inventory.Add(kvp.Key, kvp.Value);
        }
    }
}