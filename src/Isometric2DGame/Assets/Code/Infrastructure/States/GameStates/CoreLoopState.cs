using System;
using Code.Common.Destruct;
using Code.Core;
using Code.Core.Features.Movement;
using Code.Infrastructure.Loggers;
using Code.Infrastructure.Loggers.Unity;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.Systems;
using Code.Meta;
using Entitas;

namespace Code.Infrastructure.States.GameStates
{
  public class CoreLoopState : EndOfFrameExitState
  {
    private readonly ISystemFactory _systems;
    private readonly GameContext _gameContext;
    private readonly MetaContext _metaContext;
    private readonly InputContext _inputContext;

    private CoreFeature _coreFeature;
    private MetaFeature _metaFeature;
    private MovementFeature _movementFeature;
    private ProcessDestructedFeature _processDestructedFeature;

    public CoreLoopState(ISystemFactory systems, GameContext gameContext, MetaContext metaContext, InputContext inputContext)
    {
      _metaContext = metaContext;
      _inputContext = inputContext;
      _systems = systems;
      _gameContext = gameContext;
    }
    
    public override void Enter()
    {
      $"Core Loop State".Log(FeatureType.GameStateMachine);
      _coreFeature = _systems.Create<CoreFeature>();
      _metaFeature = _systems.Create<MetaFeature>();
      
      _movementFeature = _systems.Create<MovementFeature>();
      _processDestructedFeature = _systems.Create<ProcessDestructedFeature>();
      
      Do(feature => feature.Initialize());
    }

    protected override void OnUpdate()
    {
      _coreFeature.Execute();
      _metaFeature.Execute();
      _processDestructedFeature.Execute();
      
      _coreFeature.Cleanup();
      _metaFeature.Cleanup();
      _processDestructedFeature.Cleanup();
    }

    protected override void OnFixedUpdate()
    {
      _movementFeature.Execute();
      
      _movementFeature.Cleanup();
    }

    protected override void ExitOnEndOfFrame()
    {
      Do(feature => feature.DeactivateReactiveSystems());
      
      Do(feature => feature.ClearReactiveSystems());

      DestructEntities();
      
      Do(feature =>
      {
        feature.Cleanup();
        feature.TearDown();
      });
      
      _coreFeature = null;
      _metaFeature = null;
      _movementFeature = null;
      _processDestructedFeature = null;
    }

    private void DestructEntities()
    {
      foreach (GameEntity entity in _gameContext.GetEntities()) 
        entity.isDestructed = true;
      
      foreach (MetaEntity entity in _metaContext.GetEntities()) 
        entity.isDestructed = true;
      
      foreach (InputEntity entity in _inputContext.GetEntities()) 
        entity.Destroy();
    }

    private void Do(Action<Feature> action)
    {
      action.Invoke(_coreFeature);
      action.Invoke(_metaFeature);
      action.Invoke(_movementFeature);
      action.Invoke(_processDestructedFeature);
    }
  }
}