using Entitas;

namespace Code.Core.Features.Routing.Systems
{
    public class SetMovementPointToCurrentRoutePointSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _routers;

        public SetMovementPointToCurrentRoutePointSystem(GameContext game)
        {
            _routers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Routing,
                    GameMatcher.CurrentRoutePoint));
        }

        public void Execute()
        {
            foreach (var router in _routers)
            {
                router.ReplaceMovementPoint(router.CurrentRoutePoint);
            }
        }
    }
}