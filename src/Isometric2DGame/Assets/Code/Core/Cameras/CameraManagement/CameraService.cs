using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.Cameras.CameraManagement
{
    public class CameraService : ICameraService
    {
        private UnityEngine.Camera _camera;

        public UnityEngine.Camera MainCamera => _camera;

        public void Initialize(UnityEngine.Camera camera)
        {
            _camera = camera;
        }
    }
}