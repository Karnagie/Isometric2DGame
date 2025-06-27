using Code.Common.Extensions;
using Entitas;

namespace Code.Core.Features.Movement.Systems
{
    public class SetDirectionByMovementPointSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public SetDirectionByMovementPointSystem(GameContext game)
        {
            _movers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.MovementPoint));
        }

        public void Execute()
        {
            foreach (var mover in _movers)
            {
                mover.ReplaceDirection((mover.MovementPoint - mover.WorldPosition.ToVector2()).normalized);
            }
        }
    }
}