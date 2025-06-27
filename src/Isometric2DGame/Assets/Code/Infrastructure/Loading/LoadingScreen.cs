using Code.Common.Extensions;
using Code.Common.StaticData;
using Code.Infrastructure.Ui;
using DG.Tweening;
using UnityEngine;

namespace Code.Infrastructure.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        public CanvasSortOrderSetter CanvasSortOrderSetter;
        public CanvasGroup CanvasGroup;

        public void Init()
        {
            CanvasSortOrderSetter.Init();
        }
        
        public void On()
        {
            CanvasGroup.DOEnable(UiStaticData.EnableDuration);
        }

        public void Off()
        {
            CanvasGroup.DODisable(UiStaticData.DisableDuration);
        }
    }
}