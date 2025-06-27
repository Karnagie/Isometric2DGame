using Entitas;

namespace Code.Core.Features.Movement.Systems
{
    public class UpdateLocalTransformPositionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public UpdateLocalTransformPositionSystem(GameContext game)
        {
            _movers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.LocalPosition,
                    GameMatcher.Transform));
        }
    
        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                mover.Transform.localPosition = mover.LocalPosition;
            }
        }
    }
}