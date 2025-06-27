using Code.Core.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Core.Features.Movement.Systems
{
  public class DirectionalDeltaMoveSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _movers;
    private readonly ITimeService _time;

    public DirectionalDeltaMoveSystem(GameContext game, ITimeService time)
    {
      _time = time;
      _movers = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Direction, 
          GameMatcher.WorldPosition, 
          GameMatcher.Speed, 
          GameMatcher.MovementAvailable, 
          GameMatcher.Moving));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _movers)
      {
        var x = entity.WorldPosition.x + entity.Direction.x * entity.Speed * _time.DeltaTime;
        var z = entity.WorldPosition.z + entity.Direction.y * entity.Speed * _time.DeltaTime;
        entity.ReplaceWorldPosition(new Vector3(x, entity.WorldPosition.y, z));
      }
    }
  }
}