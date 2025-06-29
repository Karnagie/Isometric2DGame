using Code.Core.Features.ActionPlanning;
using Code.Infrastructure.Loggers.Unity;
using Entitas;
using UnityEngine;

namespace Code.Core.Features.Enemies.Systems
{
    public class UpdateEnemyStateSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _enemies;
        private readonly IGroup<GameEntity> _players;

        public UpdateEnemyStateSystem(GameContext game)
        {
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.CurrentAction));
            
            _players = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Player,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (var enemy in _enemies)
            {
                enemy.isRouting = false;
                enemy.ReplaceDirection(Vector2.zero);
                if (enemy.hasMovementPoint)
                    enemy.RemoveMovementPoint();
                
                switch (enemy.CurrentAction.Name)
                {
                    case ActionNames.Idle:
                        break;
                    case ActionNames.Routing:
                        enemy.isRouting = true;
                        break;
                    case ActionNames.Chase:
                        foreach (var player in _players) 
                            enemy.ReplaceMovementPoint(player.WorldPosition);
                        break;
                    case ActionNames.Attack:
                        break;
                }
            }
        }
    }
}