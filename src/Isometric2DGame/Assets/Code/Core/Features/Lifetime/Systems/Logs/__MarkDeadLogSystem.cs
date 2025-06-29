using System.Collections.Generic;
using Code.Infrastructure.Loggers;
using Code.Infrastructure.Loggers.Unity;
using Entitas;

namespace Code.Core.Features.Lifetime.Systems.Logs
{
    public class __MarkDeadLogSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(128);

        public __MarkDeadLogSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.CurrentHp,
                    GameMatcher.MaxHp)
                .NoneOf(GameMatcher.Dead));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities(_buffer))
            {
                if (entity.CurrentHp <= 0)
                {
                    $"{entity.Id} died"
                        .Setup()
                        .AddFeatureType(FeatureType.Core)
                        .Log()
                        ;
                }
            }
        }
    }
}