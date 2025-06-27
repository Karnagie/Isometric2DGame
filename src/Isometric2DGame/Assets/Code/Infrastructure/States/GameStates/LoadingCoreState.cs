using Code.Infrastructure.Loading;
using Code.Infrastructure.Loggers;
using Code.Infrastructure.Loggers.Unity;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachines;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadingCoreState : SimplePayloadState<string>
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;

    public LoadingCoreState(IGameStateMachine stateMachine, ISceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }
    
    public override void Enter(string sceneName)
    {
      $"Loading Core State".Log(FeatureType.GameStateMachine);
      _sceneLoader.LoadScene(sceneName, EnterBattleLoopState);
    }

    private void EnterBattleLoopState()
    {
      _stateMachine.Enter<CoreEnterState>();
    }
  }
}