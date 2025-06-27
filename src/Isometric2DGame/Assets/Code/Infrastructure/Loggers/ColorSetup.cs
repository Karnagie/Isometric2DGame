using System;
using Color = UnityEngine.Color;

namespace Code.Infrastructure.Loggers
{
    [Serializable]
    public struct ColorSetup<T>
    {
        public T featureType;
        public Color color;
    }
}