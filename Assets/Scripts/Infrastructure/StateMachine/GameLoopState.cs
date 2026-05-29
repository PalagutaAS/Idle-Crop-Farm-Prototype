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
            this.Log("");
            YG2.GameplayStart();
            YG2.PauseGame(false, true,false,false,false);
            //PauseGameYG.SetState(1, false, true );
        }

        public void Exit()
        {
            YG2.GameplayStop();
            YG2.PauseGame(true, true,false,false,false);
            //PauseGameYG.SetState(0, false, true);
        }
    }
}