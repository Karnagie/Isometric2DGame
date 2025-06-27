using System;
using UnityEngine;

namespace Code.Core.Common.Layers
{
    public class Layer : MonoBehaviour
    {
        public GameObject[] Objects;

        private int _layerValue;

        public void SetLayer(int value)
        {
            _layerValue = value;
            UpdateLayer();
        }

        private void UpdateLayer()
        {
            foreach (var o in Objects)
            {
                o.layer = _layerValue;
            }
        }
    }
}