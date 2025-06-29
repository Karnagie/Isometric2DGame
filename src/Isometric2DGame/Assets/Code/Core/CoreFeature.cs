using Code.Common.Windows.Systems;
using Code.Core.Cameras;
using Code.Core.Features.ActionPlanning;
using Code.Core.Features.Cooldowns;
using Code.Core.Features.Enemies;
using Code.Core.Features.Lifetime;
using Code.Core.Features.Players;
using Code.Core.Features.Routing;
using Code.Core.Features.Stats;
using Code.Core.Input;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Core
{
  public class CoreFeature : Feature
  {
    public CoreFeature(ISystemFactory systems)
    {
      Add(systems.Create<InputFeature>());
      Add(systems.Create<CamerasFeature>());
      Add(systems.Create<BindViewFeature>());

      Add(systems.Create<ActionPlanningFeature>());
      Add(systems.Create<PlayersFeature>());
      Add(systems.Create<EnemiesFeature>());
      Add(systems.Create<RoutingFeature>());
      Add(systems.Create<StatsFeature>());
      Add(systems.Create<DeathFeature>());
      
      Add(systems.Create<CooldownsFeature>());

      Add(systems.Create<OpenWindowByRequestSystem>());
      Add(systems.Create<CloseWindowByRequestSystem>());
    }
  }
}