using System;
using Code.Common.Entity;
using Code.Infrastructure.Loading;
using Code.Infrastructure.Loggers;
using Code.Infrastructure.Loggers.Unity;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachines;
using Code.Infrastructure.Systems;
using Code.Meta;
using Code.Meta.Features.Simulation;
using Code.Progress.Data;
using Code.Progress.Provider;
using Code.Progress.SaveLoad;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class ActualizeProgressState : SimpleState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly IProgressProvider _progressProvider;
    private readonly ISystemFactory _systemFactory;
    private ActualizationFeature _actualizationFeature;
    private readonly TimeSpan _twoDays = TimeSpan.FromDays(2);
    private readonly ISaveLoadService _saveLoadService;

    public ActualizeProgressState(
      IGameStateMachine stateMachine,
      IProgressProvider progressProvider,
      ISaveLoadService saveLoadService,
      ISystemFactory systemFactory)
    {
      _saveLoadService = saveLoadService;
      _stateMachine = stateMachine;
      _progressProvider = progressProvider;
      _systemFactory = systemFactory;
    }
    
    public override void Enter()
    {
      _actualizationFeature = _systemFactory.Create<ActualizationFeature>();
      
      ActualizeProgress(_progressProvider.ProgressData);
      
      _stateMachine.Enter<LoadingCoreState, string>(Scenes.Core);
    }

    private void ActualizeProgress(ProgressData data)
    {
      $"Actualizing State".Log(FeatureType.GameStateMachine);
      _actualizationFeature.Initialize();
      _actualizationFeature.DeactivateReactiveSystems();
      
      
      _saveLoadService.SaveProgress();
    }

    protected override void Exit()
    {
      _actualizationFeature.Cleanup();
      _actualizationFeature.TearDown();
      _actualizationFeature = null;
    }
  }
}