using Code.Common.Entity;
using Code.Common.Extensions;
using Entitas;
using UnityEngine;

namespace Code.Core.Features.Enemies.Systems
{
    public class SetPlayerInAttackRadiusSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _enemies;
        private readonly IGroup<GameEntity> _players;

        public SetPlayerInAttackRadiusSystem(GameContext game)
        {
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.WorldPosition,
                    GameMatcher.AttackRadius,
                    GameMatcher.Id));
            
            _players = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Player,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (var enemy in _enemies)
            foreach (var player in _players)
            {
                var wasInRadius = enemy.isPlayerInAttackRadius;
                var enemyIsPlayerInAttackRadius = 
                    Vector3.Distance(enemy.WorldPosition, player.WorldPosition) <= enemy.AttackRadius;
                enemy.isPlayerInAttackRadius = enemyIsPlayerInAttackRadius;

                if (enemyIsPlayerInAttackRadius && wasInRadius == false)
                    CreateEntity.Empty()
                        .With(x => x.isChangeActionRequest = true)
                        .AddTargetId(enemy.Id)
                        ;
            }
        }
    }
}