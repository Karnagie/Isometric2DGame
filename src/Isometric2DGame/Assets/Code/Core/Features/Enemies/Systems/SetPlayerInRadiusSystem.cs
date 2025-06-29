using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Loggers.Unity;
using Entitas;
using UnityEngine;

namespace Code.Core.Features.Enemies.Systems
{
    public class SetPlayerInRadiusSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _enemies;
        private readonly IGroup<GameEntity> _players;

        public SetPlayerInRadiusSystem(GameContext game)
        {
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.Id,
                    GameMatcher.WorldPosition,
                    GameMatcher.Radius));
            
            _players = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Player,
                    GameMatcher.WorldPosition,
                    GameMatcher.Id));
        }

        public void Execute()
        {
            foreach (var enemy in _enemies)
            foreach (var player in _players)
            {
                var wasInRadius = enemy.isPlayerInRadius;
                var enemyIsPlayerInRadius = Vector3.Distance(enemy.WorldPosition, player.WorldPosition) <= enemy.Radius;
                enemy.isPlayerInRadius = enemyIsPlayerInRadius;
                enemy.ReplaceTargetId(player.Id);

                if (enemyIsPlayerInRadius && wasInRadius == false)
                    CreateEntity.Empty()
                        .With(x => x.isChangeActionRequest = true)
                        .AddTargetId(enemy.Id)
                        ;
            }
        }
    }
}