namespace Code.Core.Cameras.CameraManagement
{
    public interface ICameraService
    {
        UnityEngine.Camera MainCamera { get; }
        
        void Initialize(UnityEngine.Camera camera);
    }
}