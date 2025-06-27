using Code.Core.Features.Movement.Physics;
using Code.Core.Features.Movement.Systems;
using Code.Infrastructure.Systems;

namespace Code.Core.Features.Movement
{
  public class MovementFeature : Feature
  {
    public MovementFeature(ISystemFactory systems)
    {
      Add(systems.Create<SetMovementForEveryWithSpeedSystem>());
      
      Add(systems.Create<SetDirectionByMovementPointSystem>());
      
      Add(systems.Create<SetMovingByMovementPointSystem>());
      
      Add(systems.Create<DeltaMoveToMovementPointOnCloseSystem>());
      Add(systems.Create<DirectionalDeltaMoveSystem>());

      Add(systems.Create<PhysicsFeature>());
      
      Add(systems.Create<AddOffsetToWorldPositionSystem>());

      Add(systems.Create<UpdateTransformPositionSystem>());
      Add(systems.Create<UpdateLocalTransformPositionSystem>());
      Add(systems.Create<RotateAlongDirectionSystem>());
      
      Add(systems.Create<RemoveMovementPointOnCloseSystem>());
    }
  }
}