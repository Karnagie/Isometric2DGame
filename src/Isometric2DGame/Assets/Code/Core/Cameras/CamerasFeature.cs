using Code.Core.Cameras.Systems;
using Code.Infrastructure.Systems;

namespace Code.Core.Cameras
{
    public sealed class CamerasFeature : Feature
    {
        public CamerasFeature(ISystemFactory systems)
        {
            Add(systems.Create<EmitCameraTargetUpdateSystem>());
            
            Add(systems.Create<SetTargetForCamerasToPlayerSystem>());
        }
    }
}