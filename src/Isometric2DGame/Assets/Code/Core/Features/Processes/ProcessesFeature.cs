using Code.Core.Features.Processes.Systems;
using Code.Infrastructure.Systems;

namespace Code.Core.Features.Processes
{
    public sealed class ProcessesFeature : Feature
    {
        public ProcessesFeature(ISystemFactory systems)
        {
            Add(systems.Create<__ProcessAttackLogSystem>());
            Add(systems.Create<ProcessAttackSystem>());
            Add(systems.Create<ProcessByCooldownSystem>());

            Add(systems.Create<MarkProcessedEffectsSystem>());
        }
    }
}