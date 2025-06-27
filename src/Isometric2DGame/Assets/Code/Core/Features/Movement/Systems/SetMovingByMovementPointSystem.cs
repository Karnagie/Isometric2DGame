using Entitas;

namespace Code.Core.Features.Movement.Systems
{
    public class SetMovingByMovementPointSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public SetMovingByMovementPointSystem(GameContext game)
        {
            _movers = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.MovementPoint));
        }

        public void Execute()
        {
            foreach (var mover in _movers)
            {
                mover.isMoving = true;
            }
        }
    }
}