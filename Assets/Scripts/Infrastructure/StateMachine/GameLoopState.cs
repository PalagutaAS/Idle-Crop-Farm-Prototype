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
            YG2.GameplayStart();
            PauseGameYG.SetState(1, false, true);
        }

        public void Exit()
        {
            YG2.GameplayStop();
            PauseGameYG.SetState(0, false, true);
            this.Log("Exit State");
        }
    }
}