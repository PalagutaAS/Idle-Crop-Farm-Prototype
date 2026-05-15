
public class LoadLevelState : IPayloadedState<string>
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly ScreenLoading _screenLoading;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ScreenLoading screenLoading)
    {
        _stateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _screenLoading = screenLoading;
    }

    public void Enter(string nameScene)
    {
        _screenLoading.Show();
        _sceneLoader.Load(nameScene, () => _stateMachine.Enter<GameLoopState>());
    }

    public void Exit()
    {
        _screenLoading.Hide();
    }
}