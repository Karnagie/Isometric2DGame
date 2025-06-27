using Code.Common.Extensions;
using Code.Core.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Core.Features.Movement.Systems
{
    public class DeltaMoveToMovementPointOnCloseSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;
        private readonly ITimeService _time;

        public DeltaMoveToMovementPointOnCloseSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _movers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Direction, 
                    GameMatcher.WorldPosition, 
                    GameMatcher.MovementPoint, 
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
                var newPosition = new Vector3(x, entity.WorldPosition.y, z);

                if (!(Vector3.Distance(newPosition, entity.WorldPosition) >=
                      Vector3.Distance(entity.WorldPosition.ToVector2(), entity.MovementPoint))) continue;
                
                entity.ReplaceWorldPosition(new Vector3(entity.MovementPoint.x, entity.WorldPosition.y, entity.MovementPoint.y));

                entity.ReplaceDirection(Vector2.zero);
            }
        }
    }
}