using System.Collections.Generic;
using Code.Common.Extensions;
using Entitas;

namespace Code.Core.Features.Routing.Systems
{
    public class SetNewRoutePointSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _routers;
        
        private readonly List<GameEntity> _buffer = new(4);

        public SetNewRoutePointSystem(GameContext game)
        {
            _routers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Routing,
                    GameMatcher.RoutePoints)
                .NoneOf(GameMatcher.CurrentRoutePoint));
        }

        public void Execute()
        {
            foreach (var router in _routers.GetEntities(_buffer))
            {
                router.AddCurrentRoutePoint(router.RoutePoints.PickRandom());
            }
        }
    }
}