using Infrastructure.PersistenceProgress;
using SavesData;

namespace Infrastructure.StateMachine
{
    public class LoadSavesState : IState
    {
        const string NameGameScene = "MainGame Scene";
        private readonly IStateSwitcher _sateMachine;
        private readonly IPersistenceProgressService _progressService;
        private readonly ISavedLoadService _savedLoadService;

        public LoadSavesState(IStateSwitcher sateMachine, IPersistenceProgressService progressService, ISavedLoadService savedLoadService)
        {
            _sateMachine = sateMachine;
            _progressService = progressService;
            _savedLoadService = savedLoadService;
        }

        public void Enter()
        {
            _progressService.Progress = _savedLoadService.LoadProgress() ?? DefaultGameProgress();
            
            _progressService.Progress.FieldData ??= DefaultFieldsData();
            _progressService.Progress.ToolsData ??= DefaultToolsData();
            _progressService.Progress.InventoryData ??= DefaultInventoryData();
            _progressService.Progress.WalletData ??= DefaultWalletData();

            _sateMachine.Enter<LoadLevelState, string>(NameGameScene);
        }


        public void Exit()
        {
            
        }
        /// <summary>
        /// Тут я возьму дефолтные значения из скриптаблобжекта или из json-файла
        /// </summary>
        /// <returns>GameProgress</returns>
        private GameProgress DefaultGameProgress() =>
            new GameProgress
            {
                InventoryData = DefaultInventoryData(),
                WalletData = DefaultWalletData(),
                ToolsData = DefaultToolsData(),
                FieldData = DefaultFieldsData(),
            };

        private static FieldsData DefaultFieldsData() => 
            new FieldsData { Fields = new SerializableDictionary<CropType, int>() };

        private static InventoryData DefaultInventoryData() => 
            new InventoryData { Crops = new SerializableDictionary<CropType, int>() };

        private static WalletData DefaultWalletData()
        {
            var walletData = new WalletData {Money = new SerializableDictionary<MoneyType, int>()};
            walletData.Money[MoneyType.Coin] = 600;
            return walletData;
        }

        private static ToolsData DefaultToolsData() => 
            new ToolsData() { Tools = new SerializableDictionary<ToolType, int>() };
    }
}