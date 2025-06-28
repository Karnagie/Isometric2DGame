using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Core;
using Code.Core.Features.Players.Factories;
using Code.Core.Levels;
using Code.Infrastructure.Identifiers;
using Code.Infrastructure.Loading;
using Code.Infrastructure.Loggers;
using Code.Infrastructure.Loggers.Unity;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachines;
using UnityEngine;
using LogType = Code.Infrastructure.Loggers.LogType;

namespace Code.Infrastructure.States.GameStates
{
  public class CoreEnterState : SimpleState
  {
    private CoreFeature _coreFeature;
    
    private readonly IGameStateMachine _stateMachine;
    private readonly GameContext _gameContext;
    private readonly ILoadingViewService _loading;
    private readonly IPlayerFactory _playerFactory;
    private readonly ILevelDataProvider _levelDataProvider;

    public CoreEnterState(
      IGameStateMachine stateMachine,
      ILoadingViewService loading,
      IPlayerFactory playerFactory,
      ILevelDataProvider levelDataProvider
      )
    {
      _levelDataProvider = levelDataProvider;
      _playerFactory = playerFactory;
      _loading = loading;
      _stateMachine = stateMachine;
    }
    
    public override void Enter()
    {
      $"Core Enter State".Log(FeatureType.GameStateMachine);
      _loading.HideScreen();
      PlaceHero();
      
      _stateMachine.Enter<CoreLoopState>();
    }
    
    private void PlaceHero()
    {
      var player = _playerFactory.Create(_levelDataProvider.StartPoint);
      $"Player#{player.Id} spawned at {_levelDataProvider.StartPoint}"
        .Setup()
        .AddFeatureType(FeatureType.Core)
        .Log();
    }
  }
}