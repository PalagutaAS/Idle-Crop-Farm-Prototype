
namespace Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IStateSwitcher _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ScreenLoading _screenLoading;


        public LoadLevelState(IStateSwitcher gameStateMachine, SceneLoader sceneLoader, ScreenLoading screenLoading)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _screenLoading = screenLoading;
        }

        public void Enter(string nameScene)
        {
            _screenLoading.Show();
            _sceneLoader.Load(nameScene, OnLoad);
        }

        private void OnLoad()
        {
            _stateMachine.Enter<ApplyGameProgressState>();
        }

        public void Exit()
        {
            
        }
        
    }
}