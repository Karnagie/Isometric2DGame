using Entitas;

namespace Code.Core.Features.Movement.Systems
{
    public class AddOffsetToWorldPositionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _followers;
        private readonly IGroup<GameEntity> _targets;

        public AddOffsetToWorldPositionSystem(GameContext game)
        {
            _followers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Offset
                    ));
        }

        public void Execute()
        {
            foreach (var follower in _followers)
            {
                follower.ReplaceWorldPosition(follower.WorldPosition + follower.Offset);  
            }
        }
    }
}