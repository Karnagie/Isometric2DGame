using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Core;
using Code.Core.Features.Enemies;
using Code.Core.Features.Enemies.Factories;
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
    private readonly IGameStateMachine _stateMachine;
    private readonly GameContext _gameContext;
    private readonly ILoadingViewService _loading;
    private readonly IPlayerFactory _playerFactory;
    private readonly ILevelDataProvider _levelDataProvider;
    private readonly IEnemyFactory _enemyFactory;

    public CoreEnterState(
      IGameStateMachine stateMachine,
      ILoadingViewService loading,
      IPlayerFactory playerFactory,
      IEnemyFactory enemyFactory,
      ILevelDataProvider levelDataProvider
      )
    {
      _enemyFactory = enemyFactory;
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
      PlaceEnemies();
      
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
    
    private void PlaceEnemies()
    {
      for (int i = 0; i < 2; i++)
      {
        var enemy = _enemyFactory.Create(new Vector3(5, i*2, 0), EnemyId.Basic);
        $"{nameof(EnemyId.Basic)}Enemy#{enemy.Id} spawned at {new Vector3(5, i, 0)}"
          .Setup()
          .AddFeatureType(FeatureType.Core)
          .Log();
      }
      
      for (int i = 2; i < 3; i++)
      {
        var enemy = _enemyFactory.Create(new Vector3(5, i*2, 0), EnemyId.Fast);
        $"{nameof(EnemyId.Fast)}Enemy#{enemy.Id} spawned at {new Vector3(5, i, 0)}"
          .Setup()
          .AddFeatureType(FeatureType.Core)
          .Log();
      }
    }
  }
}