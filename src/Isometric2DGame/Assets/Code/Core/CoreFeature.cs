using Code.Common.Windows.Systems;
using Code.Core.Cameras;
using Code.Core.Features.Players;
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

      Add(systems.Create<PlayersFeature>());
      Add(systems.Create<StatsFeature>());

      Add(systems.Create<OpenWindowByRequestSystem>());
      Add(systems.Create<CloseWindowByRequestSystem>());
    }
  }
}