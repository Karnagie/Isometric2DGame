using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Loggers;
using Code.Infrastructure.Loggers.Unity;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachines;
using Code.Progress.SaveLoad;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadProgressState : SimpleState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressState(
      IGameStateMachine stateMachine,
      ISaveLoadService saveLoadService)
    {
      _saveLoadService = saveLoadService;
      _stateMachine = stateMachine;
    }

    public override void Enter()
    {
      $"Load Progress State".Log(FeatureType.GameStateMachine);
      InitializeProgress();

      _stateMachine.Enter<ActualizeProgressState>();
    }

    private void InitializeProgress()
    {
      if (_saveLoadService.HasSavedProgress)
        _saveLoadService.LoadProgress();
      else
        CreateNewProgress();
    }

    private void CreateNewProgress()
    {
      _saveLoadService.CreateProgress();
    }
  }
}