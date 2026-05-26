using Logging;
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

        public void Enter()
        {
            this.Log("Enter State");
            PauseGameYG.SetState(1, true, true);
        }

        public void Exit()
        {
            this.Log("Exit State");
            PauseGameYG.SetState(0, true, true);
        }
    }
}