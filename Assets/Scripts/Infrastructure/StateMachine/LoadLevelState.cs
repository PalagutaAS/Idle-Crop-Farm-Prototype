
using Infrastructure.PersistenceProgress;
using Inventor;
using SavesData;

namespace Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IStateSwitcher _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ScreenLoading _screenLoading;
        private readonly IInventoryChanger _inventory;
        private readonly IWallet _wallet;
        private readonly IPersistenceProgressService _progressService;

        public LoadLevelState(IStateSwitcher gameStateMachine, SceneLoader sceneLoader, ScreenLoading screenLoading, IInventoryChanger inventory, IWallet wallet, IPersistenceProgressService progressService)
        {
            _progressService = progressService;
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _screenLoading = screenLoading;
            _inventory = inventory;
            _wallet = wallet;
        }

        public void Enter(string nameScene)
        {
            _screenLoading.Show();
            _sceneLoader.Load(nameScene, OnLoad);
        }

        private void OnLoad()
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
        }
        
        private void ApplyInventoryData(InventoryData data)
        {
            _inventory.Add(CropType.Wheat, data.Wheat);
            _inventory.Add(CropType.Potato, data.Potato);
            _inventory.Add(CropType.Corn, data.Corn);
        }
    }
}