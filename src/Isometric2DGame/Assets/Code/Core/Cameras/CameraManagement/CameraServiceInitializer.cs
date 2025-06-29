using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Code.Core.Cameras.CameraManagement
{
    public class CameraServiceInitializer : MonoBehaviour, IInitializable
    {
        public CinemachineCamera Camera;
        
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