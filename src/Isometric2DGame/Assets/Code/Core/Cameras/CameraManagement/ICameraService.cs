using Unity.Cinemachine;

namespace Code.Core.Cameras.CameraManagement
{
    public interface ICameraService
    {
        CinemachineCamera MainCamera { get; }
        
        void Initialize(CinemachineCamera camera);
    }
}