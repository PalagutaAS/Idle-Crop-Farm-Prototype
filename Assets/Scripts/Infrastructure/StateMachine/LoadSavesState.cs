using Infrastructure.PersistenceProgress;
using SavesData;

namespace Infrastructure.StateMachine
{
    public class LoadSavesState : IState
    {
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
            
            _sateMachine.Enter<LoadLevelState, string>("MainGame Scene");
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
                InventoryData = new InventoryData {Corn = 0, Wheat = 0, Potato = 0},
                WalletData = new WalletData() {Gold = 3000}
            };
    }
}