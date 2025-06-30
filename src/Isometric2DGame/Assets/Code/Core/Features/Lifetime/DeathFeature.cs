using Code.Core.Features.Lifetime.Systems;
using Code.Core.Features.Lifetime.Systems.Logs;
using Code.Infrastructure.Systems;

namespace Code.Core.Features.Lifetime
{
  public sealed class DeathFeature : Feature
  {
    public DeathFeature(ISystemFactory systems)
    {
      Add(systems.Create<__MarkDeadLogSystem>());
      Add(systems.Create<MarkDeadSystem>());
      
      Add(systems.Create<FinalizeDeathSystem>());
    }
  }
}