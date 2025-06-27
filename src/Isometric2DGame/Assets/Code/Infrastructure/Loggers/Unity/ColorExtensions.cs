using UnityEngine;

namespace Code.Infrastructure.Loggers.Unity
{
    public static class ColorExtensions
    {
        public static string ToColor(this string message, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{message}</color>";
        }
    }
}