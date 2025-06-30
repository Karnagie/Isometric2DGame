using System.Collections.Generic;
using Code.Common.Extensions;
using Entitas;

namespace Code.Core.Features.Movement.Systems
{
    public class RemoveMovementPointOnCloseSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;
        
        private readonly List<GameEntity> _buffer = new(8);

        public RemoveMovementPointOnCloseSystem(GameContext game)
        {
            _movers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.MovementPoint
                ));
        }

        public void Execute()
        {
            foreach (var mover in _movers.GetEntities(_buffer))
            {
                if ((mover.MovementPoint - mover.WorldPosition.ToVector2()).sqrMagnitude < 0.001f)
                {
                    mover.RemoveMovementPoint();
                }
            }
        }
    }
}