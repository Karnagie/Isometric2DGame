using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace Code.Core.Cameras.CameraManagement
{
    public class CameraService : ICameraService
    {
        private CinemachineCamera _camera;

        public CinemachineCamera MainCamera => _camera;

        public void Initialize(CinemachineCamera camera)
        {
            _camera = camera;
        }
    }
}