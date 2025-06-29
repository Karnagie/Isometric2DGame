using Code.Core.Features.Enemies.Systems;
using Code.Infrastructure.Systems;

namespace Code.Core.Features.Enemies
{
    public sealed class EnemiesFeature : Feature
    {
        public EnemiesFeature(ISystemFactory systems)
        {
            Add(systems.Create<SetRoutePointsForEnemiesSystemSystem>());
        }
    }
}