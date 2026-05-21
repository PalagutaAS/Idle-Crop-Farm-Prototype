using Fields;
using Infrastructure.PersistenceProgress;
using Inventor;
using SavesData;
using Tools.Interface;

namespace Infrastructure.StateMachine
{
    public class ApplyGameProgressState : IState
    {
        private readonly IInventoryChanger _inventory;
        private readonly IWallet _wallet;
        private readonly IPersistenceProgressService _progressService;
        private readonly IFieldService _fieldService;
        private readonly IToolManager _toolManager;
        private readonly ScreenLoading _screenLoading;
        private readonly IStateSwitcher _stateMachine;

        public ApplyGameProgressState(IStateSwitcher gameStateMachine, ScreenLoading screenLoading, IInventoryChanger inventory, IWallet wallet, IPersistenceProgressService progressService, IFieldService fieldService, IToolManager toolManager)
        {
            _progressService = progressService;
            _fieldService = fieldService;
            _toolManager = toolManager;
            _screenLoading = screenLoading;
            _stateMachine = gameStateMachine;
            _inventory = inventory;
            _wallet = wallet;
        }

        public void Enter()
        {
            ApplyGameProgress();
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            _screenLoading.Hide();
        }

        private void ApplyGameProgress()
        {
            ApplyInventoryData(_progressService.Progress.InventoryData);
            _wallet.Payout(_progressService.Progress.WalletData.Gold);
            ApplyFieldsData();
            ApplyToolsData();
        }

        private void ApplyToolsData()
        {
            if (_progressService.Progress.ToolsData == null)
                return;
            
            _toolManager.TrySetupNewTool(ToolType.Shovel, _progressService.Progress.ToolsData.Shovel);
            _toolManager.TrySetupNewTool(ToolType.Pickaxe, _progressService.Progress.ToolsData.Scythe);
        }

        private void ApplyFieldsData()
        {
            for (int i = _progressService.Progress.FieldData.Corn; i > 0; i--) 
                _fieldService.OpenField(CropType.Corn);

            for (int i = _progressService.Progress.FieldData.Wheat; i > 0; i--) 
                _fieldService.OpenField(CropType.Wheat);

            for (int i = _progressService.Progress.FieldData.Potato; i > 0; i--)
                _fieldService.OpenField(CropType.Potato);
            
        }

        private void ApplyInventoryData(InventoryData data)
        {
            _inventory.Add(CropType.Wheat, data.Wheat);
            _inventory.Add(CropType.Potato, data.Potato);
            _inventory.Add(CropType.Corn, data.Corn);
        }
    }
}