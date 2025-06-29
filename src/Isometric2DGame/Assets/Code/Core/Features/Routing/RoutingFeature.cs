using Code.Core.Features.Routing.Systems;
using Code.Infrastructure.Systems;

namespace Code.Core.Features.Routing
{
    public sealed class RoutingFeature : Feature
    {
        public RoutingFeature(ISystemFactory systems)
        {
            Add(systems.Create<SetNewRoutePointSystem>());
            
            Add(systems.Create<SetMovementPointToCurrentRoutePointSystem>());
            
            Add(systems.Create<RemoveRoutePointOnCloseSystem>());
        }
    }
}