using Code.Core.Input.Systems;
using Code.Infrastructure.Systems;

namespace Code.Core.Input
{
  public class InputFeature : Feature
  {
    public InputFeature(ISystemFactory systems)
    {
      Add(systems.Create<InitializeInputSystem>());
      
      Add(systems.Create<EmitMovementInputSystem>());
      Add(systems.Create<EmitAttackInputSystem>());
    }
  }
}