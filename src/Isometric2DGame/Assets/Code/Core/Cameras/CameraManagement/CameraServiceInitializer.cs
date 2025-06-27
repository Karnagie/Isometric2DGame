using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Core.Cameras.CameraManagement
{
    public class CameraServiceInitializer : MonoBehaviour, IInitializable
    {
        public UnityEngine.Camera Camera;
        
        private ICameraService _cameraService;

        [Inject]
        private void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public void Initialize()
        {
            _cameraService.Initialize(Camera);
        }
    }
}