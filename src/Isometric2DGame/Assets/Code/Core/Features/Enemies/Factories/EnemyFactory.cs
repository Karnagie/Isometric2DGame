using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Common.StaticData;
using Code.Core.Features.ActionPlanning;
using Code.Core.Features.ActionPlanning.EnemyActions;
using Code.Core.Features.Stats;
using Code.Infrastructure;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Core.Features.Enemies.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IIdentifierService _id;
        private readonly IStaticDataService _staticData;
        private readonly GameContext _game;
        private readonly ICoroutineRunner _coroutineRunner;

        public EnemyFactory(
            IIdentifierService id, 
            IStaticDataService staticData, 
            GameContext game, 
            ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _game = game;
            _staticData = staticData;
            _id = id;
        }

        public GameEntity Create(Vector3 at, EnemyId id)
        {
            var stats = _staticData.GetEnemyStats(id);

            var enemy = CreateEntity.Empty()
                .AddId(_id.Next())
                .With(x => x.isEnemy = true)
                .AddViewPrefab(_staticData.GetEnemyPrefab(id))
                    
                .AddBaseStats(stats)
                
                .With(x => x.isMovementAvailable = true)
                .AddSpeed(0)
                .AddWorldPosition(at)
                .AddDirection(Vector2.zero)
                
                .With(x => x.isActionPlanner = true);

            var idleAction = new IdleAction(enemy, ActionNames.Idle, _coroutineRunner);
            enemy.AddActions(new Dictionary<string, Action>()
            {
                {ActionNames.Idle, idleAction},
                {ActionNames.Routing, new RouteAction(enemy, ActionNames.Routing, _coroutineRunner)},
                {ActionNames.Chase, new ChaseAction(enemy, ActionNames.Chase, _game)},
                {ActionNames.Attack, new AttackAction(enemy, ActionNames.Attack)},
            });
            
            return enemy;
        }
    }
}