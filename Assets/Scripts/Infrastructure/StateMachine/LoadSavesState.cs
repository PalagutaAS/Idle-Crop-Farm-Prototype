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
            new FieldsData {};

        private static InventoryData DefaultInventoryData() => 
            new InventoryData {Corn = 0, Wheat = 0, Potato = 0};

        private static WalletData DefaultWalletData() => 
            new WalletData {Gold = 100};

        private static ToolsData DefaultToolsData() => 
            new ToolsData()
            {
                Scythe = 0,
                Shovel = 0,
            };
    }
}