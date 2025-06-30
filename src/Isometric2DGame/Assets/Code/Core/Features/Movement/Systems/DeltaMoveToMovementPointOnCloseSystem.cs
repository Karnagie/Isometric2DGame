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
                var y = entity.WorldPosition.y + entity.Direction.y * entity.Speed * _time.DeltaTime;
                var newPosition = new Vector3(x, y, entity.WorldPosition.z);

                if (!(Vector3.Distance(newPosition, entity.WorldPosition) >=
                      Vector3.Distance(entity.WorldPosition.ToVector2(), entity.MovementPoint))) continue;
                
                entity.ReplaceWorldPosition(new Vector3(entity.MovementPoint.x, entity.MovementPoint.y, entity.WorldPosition.z));

                entity.ReplaceDirection(Vector2.zero);
            }
        }
    }
}