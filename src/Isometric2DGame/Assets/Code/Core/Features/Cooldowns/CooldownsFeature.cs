using Code.Core.Features.Cooldowns.Systems;
using Code.Infrastructure.Systems;

namespace Code.Core.Features.Cooldowns
{
    public sealed class CooldownsFeature : Feature
    {
        public CooldownsFeature(ISystemFactory systems)
        {
            Add(systems.Create<CooldownSystem>());
            Add(systems.Create<DestructOnCooldownUpSystem>());
        }
    }
}