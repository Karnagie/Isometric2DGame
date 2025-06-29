using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Common.StaticData;
using Code.Core.Features.Stats;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Core.Features.Players.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IIdentifierService _id;
        private readonly IStaticDataService _staticData;

        public PlayerFactory(IIdentifierService id, IStaticDataService staticData)
        {
            _staticData = staticData;
            _id = id;
        }

        public GameEntity Create(Vector3 at)
        {
            var stats = _staticData.GetPlayerStats();
            
            return CreateEntity.Empty()
                    .AddId(_id.Next())
                    .With(x => x.isPlayer = true)
                    .AddViewPrefab(_staticData.GetPlayerPrefab())
                    
                    .AddBaseStats(stats)
                    .AddStatModifiers(InitStats.EmptyStatDictionary())
                
                    .With(x => x.isMovementAvailable = true)
                    .AddSpeed(0)
                    .AddWorldPosition(at)
                    .AddDirection(Vector2.zero)
                
                    .AddMaxHp(stats[StatId.Health])
                    .AddCurrentHp(stats[StatId.Health])
                ;
        }
    }
}