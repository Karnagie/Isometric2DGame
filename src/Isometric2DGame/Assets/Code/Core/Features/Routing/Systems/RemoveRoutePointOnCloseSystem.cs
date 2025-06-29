using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Core.Features.Routing.Systems
{
    public class RemoveRoutePointOnCloseSystem : IExecuteSystem
    {
        private const float CLOSE = 0.1f;
        
        private readonly IGroup<GameEntity> _routers;
        
        private readonly List<GameEntity> _buffer = new(4);

        public RemoveRoutePointOnCloseSystem(GameContext game)
        {
            _routers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Routing,
                    GameMatcher.CurrentRoutePoint,
                    GameMatcher.WorldPosition)
            );
        }

        public void Execute()
        {
            foreach (var router in _routers.GetEntities(_buffer))
            {
                if(Vector2.Distance(router.WorldPosition, router.CurrentRoutePoint) <= CLOSE)
                    router.RemoveCurrentRoutePoint();
            }
        }
    }
}