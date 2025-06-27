using Color = UnityEngine.Color;

namespace Code.Infrastructure.Loggers
{
    public interface ILoggerProfile
    {
        FeatureType FeatureMask { get; }
        LogType LogMask { get; }
        bool ApplyColor { get; }
        
        Color GetFeatureColor(FeatureType featureType);
        Color GetLogColor(LogType logType);
    }
}