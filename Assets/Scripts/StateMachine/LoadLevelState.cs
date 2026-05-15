
public class LoadLevelState : IPayloadedState<string>
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
        _stateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter(string nameScene)
    {
        _sceneLoader.Load(nameScene, () => _stateMachine.Enter<GameLoopState>());
    }

    public void Exit()
    {
    }
}