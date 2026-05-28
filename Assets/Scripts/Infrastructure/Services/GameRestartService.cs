using Infrastructure.DI;
using Infrastructure.StateMachine;
using UnityEngine;

namespace Infrastructure.Services
{
    public class GameRestartService : IRestartGameService
    {
        private readonly IStateSwitcher _gsm;
        private readonly ScreenLoading _screenLoading;

        public GameRestartService(IStateSwitcher gsm, ScreenLoading screenLoading)
        {
            _gsm = gsm;
            _screenLoading = screenLoading;
        }

        public void DoRestartGame()
        {
            _screenLoading.ShowAppear();
            var rootScope = Object.FindObjectOfType<BootstrapperLifetimeScope>();
            if (rootScope != null)
            {
                Object.Destroy(rootScope.gameObject);
            }
            _gsm.Enter<LoadLevelState, string>("BootstrapScene");
        }
    }

    public interface IRestartGameService
    {
        void DoRestartGame();
    }
}