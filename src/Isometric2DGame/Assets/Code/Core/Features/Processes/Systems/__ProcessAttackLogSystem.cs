using System.Collections.Generic;
using Code.Infrastructure.Loggers;
using Code.Infrastructure.Loggers.Unity;
using Entitas;

namespace Code.Core.Features.Processes.Systems
{
    public class __ProcessAttackLogSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _attacks;
        private readonly IGroup<GameEntity> _targets;

        private readonly GameContext _game;

        private readonly List<GameEntity> _buffer = new(16);

        public __ProcessAttackLogSystem(GameContext game)
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
                var target = _game.GetEntityWithId(attack.TargetId);
                
                if(_targets.ContainsEntity(target) == false)
                    continue;
                
                ($"{target.Id} gets damage {attack.Damage}\n" +
                    $"Current hp: {target.CurrentHp}\n" +
                    $"Result hp: {target.CurrentHp - attack.Damage}")
                    .Setup()
                    .AddFeatureType(FeatureType.Core)
                    .Log();
            }
        }
    }
}