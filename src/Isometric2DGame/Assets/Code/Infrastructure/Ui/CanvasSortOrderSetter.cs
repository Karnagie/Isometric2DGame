using System;
using Code.Infrastructure.Loggers;
using Code.Infrastructure.Loggers.Unity;
using UnityEngine;
using LogType = Code.Infrastructure.Loggers.LogType;

namespace Code.Infrastructure.Ui
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasSortOrderSetter : MonoBehaviour
    {
        public SortingOrder SortingOrder;
        
        private Canvas _canvas;
        
        private void Start()
        {
            Validate();
        }

        public void Init()
        {
            _canvas ??= GetComponent<Canvas>();
            
            _canvas.sortingOrder = (int) SortingOrder;
        }

        private void Validate()
        {
            if (_canvas == null)
            {
                $"Sorting Order not initialized!"
                    .Setup()
                    .AddFeatureType(FeatureType.UI)
                    .AddLogType(LogType.Error)
                    .Log();
                return;
            }

            if (_canvas.sortingOrder != (int) SortingOrder)
            {
                $"Sorting Order changed not correct!"
                    .Setup()
                    .AddFeatureType(FeatureType.UI)
                    .AddLogType(LogType.Error)
                    .Log();
                return;
            }
        }
    }
}