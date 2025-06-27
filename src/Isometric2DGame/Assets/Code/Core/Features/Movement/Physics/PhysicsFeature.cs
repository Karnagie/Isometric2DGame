using Code.Core.Features.Movement.Physics.Systems;
using Code.Infrastructure.Systems;

namespace Code.Core.Features.Movement.Physics
{
    public sealed class PhysicsFeature : Feature
    {

        public PhysicsFeature(ISystemFactory systems)
        {
            Add(systems.Create<UpdatePositionByDifferenceWithTransformSystem>());
        }
    }
}