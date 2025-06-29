using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Core.Cameras.CameraManagement;
using Entitas;

namespace Code.Core.Cameras.Systems
{
    public class EmitCameraTargetUpdateSystem : IExecuteSystem
    {
        private readonly ICameraService _cameraService;

        public EmitCameraTargetUpdateSystem(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }
        
        public void Execute()
        {
            if (_cameraService.MainCamera.Target.TrackingTarget == null)
                CreateEntity.Empty()
                    .With(x => x.isUpdateCameraTargetRequest = true)
                    ;
        }
    }
}