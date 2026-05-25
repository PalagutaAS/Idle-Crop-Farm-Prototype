using Logging;

namespace Infrastructure.StateMachine
{
    public class GameMenuPauseState : IState
    {
        private readonly IStateSwitcher _gameStateMachine;
        private DebugMenu _debugMenu;

        public GameMenuPauseState(IStateSwitcher gameStateMachine, DebugMenu debugMenu)
        {
            _gameStateMachine = gameStateMachine;
            _debugMenu = debugMenu;
        }

        public void Exit()
        {
            _debugMenu?.Close();
        }

        public void Enter()
        {
            _debugMenu?.Open();
        }
        
    }
}