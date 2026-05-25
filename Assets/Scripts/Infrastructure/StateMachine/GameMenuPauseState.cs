namespace Infrastructure.StateMachine
{
    public class GameMenuPauseState : IState
    {
        private readonly IStateSwitcher _gameStateMachine;

        public GameMenuPauseState(IStateSwitcher gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
        }
        
    }
}