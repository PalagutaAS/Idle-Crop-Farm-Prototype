
namespace Infrastructure.StateMachine
{
    public class GameLoopState : IState
    {
        private readonly IStateSwitcher _gameStateMachine;

        public GameLoopState(IStateSwitcher gameStateMachine)
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