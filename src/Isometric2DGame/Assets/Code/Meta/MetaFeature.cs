using Code.Common.Destruct;
using Code.Common.Scenes.Systems;
using Code.Infrastructure.Systems;
using Code.Meta.Features.Simulation.Systems;
using Code.Progress;

namespace Code.Meta
{
  public class MetaFeature : Feature
  {
    public MetaFeature(ISystemFactory systems)
    {
      Add(systems.Create<ChangeSceneByRequestSystem>());
      
      Add(systems.Create<EmitTickSystem>(MetaConstants.SimulationTickSeconds));
      
      Add(systems.Create<PeriodicallySaveProgressSystem>(MetaConstants.SaveProgressPeriodSeconds));
      
      Add(systems.Create<CleanupTickSystem>());
    }
  }
}