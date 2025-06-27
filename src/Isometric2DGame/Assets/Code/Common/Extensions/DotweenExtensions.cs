using DG.Tweening;
using UnityEngine;

namespace Code.Common.Extensions
{
    public static class DotweenExtensions
    {
        public static void DOEnable(this CanvasGroup canvasGroup, float duration)
        {
            canvasGroup.gameObject.SetActive(true);
            
            canvasGroup.DOFade(1, duration);
        }
        
        public static void DODisable(this CanvasGroup canvasGroup, float duration)
        {
            canvasGroup.gameObject.SetActive(true);
            
            canvasGroup.DOFade(0, duration)
                .OnComplete((() => canvasGroup.gameObject.SetActive(false)));
        }
    }
}