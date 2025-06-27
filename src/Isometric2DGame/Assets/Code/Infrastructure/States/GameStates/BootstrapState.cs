using Code.Common.StaticData;
using Code.Infrastructure.Loading;
using Code.Infrastructure.Loggers;
using Code.Infrastructure.Loggers.Unity;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachines;

namespace Code.Infrastructure.States.GameStates
{
  public class BootstrapState : SimpleState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly IStaticDataService _staticDataService;
    private readonly ILoadingViewService _loadingViewService;

    public BootstrapState(
      IGameStateMachine stateMachine, 
      IStaticDataService staticDataService,
      ILoadingViewService loadingViewService)
    {
      _loadingViewService = loadingViewService;
      _stateMachine = stateMachine;
      _staticDataService = staticDataService;
    }
    
    public override void Enter()
    {
      $"Bootstrap State".Log(FeatureType.GameStateMachine);
      
      _staticDataService.LoadAll();
      
      _loadingViewService.Init();
      _loadingViewService.ShowScreen();
      
      _stateMachine.Enter<LoadProgressState>();
    }
  }
}