using Entitas;

namespace Code.Core.Features.Cooldowns.Systems
{
    public class DestructOnCooldownUpSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _cooldowns;

        public DestructOnCooldownUpSystem(GameContext game)
        {
            _cooldowns = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.CooldownUp,
                    GameMatcher.DestructOnCooldownUp));
        }

        public void Execute()
        {
            foreach (var cooldown in _cooldowns)
            {
                cooldown.isDestructed = true;
            }
        }
    }
}