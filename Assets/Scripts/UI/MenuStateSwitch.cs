using Infrastructure.StateMachine;
using Logging;
using UnityEngine;
using VContainer;

namespace UI
{
    public class MenuStateSwitch : MonoBehaviour
    {
        private IStateSwitcher _gsm;
        private DebugMenu _debugMenu;
        
        [Inject]
        private void Constructor(IStateSwitcher gsm, DebugMenu debugMenu)
        {
            _gsm = gsm;
            _debugMenu = debugMenu;
        }

        public void OnOpenMenu()
        {
            _gsm?.Enter<GameMenuPauseState>();
            _debugMenu?.gameObject.SetActive(true);
        }

        public void OnCloseMenu()
        {
            _gsm?.Enter<GameLoopState>();
            _debugMenu?.gameObject.SetActive(false);
        }
    }
}
