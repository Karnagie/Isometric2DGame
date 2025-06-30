using Code.Core.Abilities.Systems;
using Code.Infrastructure.Systems;

namespace Code.Core.Abilities
{
    public sealed class AbilitiesFeature : Feature
    {
        public AbilitiesFeature(ISystemFactory systems)
        {
            Add(systems.Create<EmitAbilitiesForEnemiesSystem>());
            Add(systems.Create<EmitSpeedAbilityByCooldownSystem>());
        }
    }
}