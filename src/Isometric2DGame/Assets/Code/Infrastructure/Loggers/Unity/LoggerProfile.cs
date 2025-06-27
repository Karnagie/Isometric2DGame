using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Infrastructure.Loggers.Unity
{
    [CreateAssetMenu(fileName = "LoggerProfile", menuName = "LoggerProfile", order = 0)]
    public class LoggerProfile : ScriptableObject, ILoggerProfile
    {
        //todo add priority for local profiles
        
        [SerializeField] private FeatureType _featureMask;
        [SerializeField] private LogType _logMask;
        [SerializeField] private bool _applyColor;
        
        [SerializeField] private List<ColorSetup<FeatureType>> _featureColors;
        [SerializeField] private List<ColorSetup<LogType>> _logColors;
        
        public FeatureType FeatureMask => _featureMask;
        public LogType LogMask => _logMask;
        public bool ApplyColor => _applyColor;
        
        
        public Color GetFeatureColor(FeatureType featureType)
        {
            return _featureColors
                .FirstOrDefault(x => x.featureType == featureType).color;
        }
        
        public Color GetLogColor(LogType logType)
        {
            return _logColors
                .FirstOrDefault(x => x.featureType == logType).color;
        }
    }
}