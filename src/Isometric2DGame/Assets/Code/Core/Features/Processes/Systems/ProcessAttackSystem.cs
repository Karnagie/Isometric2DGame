using System.Collections.Generic;
using Entitas;

namespace Code.Core.Features.Processes.Systems
{
    public class ProcessAttackSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _attacks;
        private readonly IGroup<GameEntity> _targets;

        private readonly GameContext _game;

        private readonly List<GameEntity> _buffer = new(16);

        public ProcessAttackSystem(GameContext game)
        {
            _game = game;
            _attacks = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Damage,
                    GameMatcher.Process,
                    GameMatcher.TargetId));
            
            _targets = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.CurrentHp,
                    GameMatcher.Id));
        }

        public void Execute()
        {
            foreach (var attack in _attacks.GetEntities(_buffer))
            {
                attack.isProcess = false;
                attack.isProcessed = true;
                
                var target = _game.GetEntityWithId(attack.TargetId);
                
                if(_targets.ContainsEntity(target) == false)
                    continue;

                target.ReplaceCurrentHp(target.CurrentHp - attack.Damage);
            }
        }
    }
}