using System.Collections.Generic;
using Entitas;

namespace Code.Core.Features.Movement.Systems
{
    public class SetMovementForEveryWithSpeedSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;
        
        private readonly List<GameEntity> _buffer = new(16);

        public SetMovementForEveryWithSpeedSystem(GameContext game)
        {
            _movers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Speed)
                .NoneOf(GameMatcher.ProcessingDeath));
        }

        public void Execute()
        {
            foreach (var mover in _movers.GetEntities(_buffer))
            {
                mover.isMovementAvailable = true;
            }
        }
    }
}