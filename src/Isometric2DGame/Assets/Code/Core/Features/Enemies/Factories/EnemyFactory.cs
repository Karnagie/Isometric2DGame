using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Common.StaticData;
using Code.Core.Features.Stats;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Core.Features.Enemies.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IIdentifierService _id;
        private readonly IStaticDataService _staticData;

        public EnemyFactory(IIdentifierService id, IStaticDataService staticData)
        {
            _staticData = staticData;
            _id = id;
        }

        public GameEntity Create(Vector3 at, EnemyId id)
        {
            var stats = _staticData.GetEnemyStats(id);
            
            return CreateEntity.Empty()
                    .AddId(_id.Next())
                    .With(x => x.isEnemy = true)
                    .AddViewPrefab(_staticData.GetEnemyPrefab(id))
                    
                    .AddBaseStats(stats)
                
                    .With(x => x.isMovementAvailable = true)
                    .AddSpeed(0)
                    .AddWorldPosition(at)
                    .AddDirection(Vector2.zero)
                
                    .With(x => x.isRouting = true)
                ;
        }
    }
}