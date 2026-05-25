using YG;

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
            PauseGameYG.SetState(0, true, true);
        }

        public void Enter()
        {
            PauseGameYG.SetState(1, true, true);
        }
    }
}