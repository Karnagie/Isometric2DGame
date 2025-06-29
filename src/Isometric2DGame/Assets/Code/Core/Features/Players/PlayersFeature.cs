using Code.Core.Features.Players.Systems;
using Code.Infrastructure.Systems;

namespace Code.Core.Features.Players
{
    public sealed class PlayersFeature : Feature
    {
        public PlayersFeature(ISystemFactory systems)
        {
            Add(systems.Create<SetMovingByInputSystem>());
            Add(systems.Create<SetDirectionByInputSystem>());
        }
    }
}