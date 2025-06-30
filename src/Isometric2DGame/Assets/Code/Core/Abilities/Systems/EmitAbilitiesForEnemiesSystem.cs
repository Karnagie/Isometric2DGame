using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Core.Features.Cooldowns;
using Entitas;

namespace Code.Core.Abilities.Systems
{
    public class EmitAbilitiesForEnemiesSystem : IInitializeSystem
    {
        private readonly IGroup<GameEntity> _enemies;

        public EmitAbilitiesForEnemiesSystem(GameContext game)
        {
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.Id));
        }

        public void Initialize()
        {
            foreach (var enemy in _enemies)
            {
                CreateEntity.Empty()
                    .With(x => x.isSpeedAbility = true)
                    .AddTargetId(enemy.Id)
                    .PutOnCooldown(5f)
                    ;
            }
        }
    }
}