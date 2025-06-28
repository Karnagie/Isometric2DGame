using Code.Core.Features.Stats.Systems;
using Code.Infrastructure.Systems;

namespace Code.Core.Features.Stats
{
    public sealed class StatsFeature : Feature
    {
        public StatsFeature(ISystemFactory systems)
        {
            Add(systems.Create<ApplySpeedFromStatsSystem>());
            Add(systems.Create<ApplyStatModifierSpeedSystem>());
            
            Add(systems.Create<StatChangeSystem>());
        }
    }
}