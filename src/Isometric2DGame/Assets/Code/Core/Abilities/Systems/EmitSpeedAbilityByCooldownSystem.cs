using System.Collections.Generic;
using Code.Core.Features.Cooldowns;
using Code.Core.Features.Processes;
using Code.Core.Features.Processes.Factories;
using Entitas;

namespace Code.Core.Abilities.Systems
{
    public class EmitSpeedAbilityByCooldownSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _abilities;
        
        private readonly IProcessFactory _processFactory;
        
        private readonly List<GameEntity> _buffer = new (4);

        public EmitSpeedAbilityByCooldownSystem(GameContext game, IProcessFactory processFactory)
        {
            _processFactory = processFactory;
            _abilities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.SpeedAbility,
                    GameMatcher.CooldownUp,
                    GameMatcher.TargetId));
        }

        public void Execute()
        {
            foreach (var ability in _abilities.GetEntities(_buffer))
            {
                _processFactory.SpeedUp(ability.TargetId, P.EnemySpeedUp(5, 2f));
                ability.PutOnCooldown();
            }
        }
    }
}