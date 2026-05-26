using Infrastructure.StateMachine;
using Logging;
using UnityEngine;
using VContainer;

namespace UI
{
    public class MenuStateSwitch : MonoBehaviour
    {
        private IStateSwitcher _gsm;
        private IDebugMenu _debugMenu;
        
        [Inject]
        private void Constructor(IStateSwitcher gsm, IDebugMenu debugMenu)
        {
            _gsm = gsm;
            _debugMenu = debugMenu;
        }

        public void OnOpenMenu()
        {
            _gsm?.Enter<GameMenuPauseState>();
            _debugMenu.Open();
        }

        public void OnCloseMenu()
        {
            _gsm?.Enter<GameLoopState>();
            _debugMenu.Close();
        }
    }
}
