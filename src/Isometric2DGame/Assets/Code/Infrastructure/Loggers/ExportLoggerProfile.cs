using System;
using System.Collections.Generic;
using System.Linq;
using Color = UnityEngine.Color;

namespace Code.Infrastructure.Loggers
{
    public class ExportLoggerProfile : ILoggerProfile
    {
        private readonly List<ColorSetup<FeatureType>> _featureColors = new();
        private readonly List<ColorSetup<LogType>> _logColors = new();
        
        public FeatureType FeatureMask { get; } = AllFlags<FeatureType>();
        
        public LogType LogMask { get; } = AllFlags<LogType>();
        public bool ApplyColor { get; } = false;
        
        
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

        private static TEnum AllFlags<TEnum>()
            where TEnum : struct
        {
            Type enumType = typeof(TEnum);
            long newValue = 0;
            var enumValues = Enum.GetValues(enumType);
            foreach (var value in enumValues)
            {
                long v = (long)Convert.ChangeType(value, TypeCode.Int64);
                if(v == 1 || v % 2 == 0)
                {
                    newValue |= v;
                }
            }
            return (TEnum)Enum.ToObject(enumType , newValue);
        }
    }
}