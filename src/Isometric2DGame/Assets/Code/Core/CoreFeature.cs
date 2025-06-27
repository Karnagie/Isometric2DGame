using Code.Common.Windows.Systems;
using Code.Core.Cameras;
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
      Add(systems.Create<BindViewFeature>());

      Add(systems.Create<OpenWindowByRequestSystem>());
      Add(systems.Create<CloseWindowByRequestSystem>());
      Add(systems.Create<CamerasFeature>());
    }
  }
}