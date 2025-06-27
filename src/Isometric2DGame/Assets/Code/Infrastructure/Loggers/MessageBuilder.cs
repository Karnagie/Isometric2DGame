using Code.Infrastructure.Loggers.Unity;
using Color = UnityEngine.Color;

namespace Code.Infrastructure.Loggers
{
    public class MessageBuilder
    {
        private readonly string _message;
        private FeatureType _featureType = FeatureType.Temporary;
        private LogType _logType = LogType.Default;
        
        public LogType LogType => _logType;
        public FeatureType FeatureType => _featureType;
        
        public MessageBuilder(string message = "Empty message")
        {
            _message = message;
        }
        
        public MessageBuilder AddLogType(LogType type)
        {
            _logType = type;
            
            return this;
        }
        
        public MessageBuilder AddFeatureType(FeatureType type)
        {
            _featureType = type;
            
            return this;
        }
        
        public string Create(ILoggerProfile profile)
        {
            var result = string.Empty;
            
            var featureColor = profile.GetFeatureColor(_featureType);
            result += TryColour($"[{_featureType.ToString()}] ", featureColor, profile.ApplyColor);
            
            var logColor = profile.GetLogColor(_logType);
            result += TryColour(_message, logColor, profile.ApplyColor);
            
            return result;
        }
        
        private string TryColour(string message, Color color, bool apply)
        {
            if (apply == false)
                return message;
            
            return message.ToColor(color);
        }
    }
}