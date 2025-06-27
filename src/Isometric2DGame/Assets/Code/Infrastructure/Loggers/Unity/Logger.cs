using System;
using System.Collections.Generic;
using Code.Infrastructure.Serialization;
using UnityEngine;

namespace Code.Infrastructure.Loggers.Unity
{
    public static class Logger
    {
        private const int LOG_CAPACITY = 2048;
        
        private static LoggerProfile loggerProfile;
        
        public static readonly List<string> logs = new (LOG_CAPACITY);
        public static readonly List<int> logsCounts = new (LOG_CAPACITY);
        public static readonly List<string> logsStackTraces = new (LOG_CAPACITY);
        
        [HideInCallstack]
        public static MessageBuilder Log(this MessageBuilder builder)
        {
            var message = builder.Create(loggerProfile);
            
            Log(builder, message);
            
            return builder;
        }
        
        [HideInCallstack]
        public static T Log<T>(this T obj, 
            LogType logType = LogType.Default, 
            FeatureType featureType = FeatureType.Temporary)
        {
            var builder = obj != null ?
                new MessageBuilder(obj.ToString()) : new MessageBuilder();
            
            builder.AddLogType(logType);
            builder.AddFeatureType(featureType);
            
            var message = builder.Create(loggerProfile);
            
            Log(builder, message);
            
            return obj;
        }
        
        [HideInCallstack]
        public static T LogJson<T>(this T obj, 
            LogType logType = LogType.Default, 
            FeatureType featureType = FeatureType.Temporary)
        {
            var builder = obj != null ?
                new MessageBuilder(obj.ToJson()) : new MessageBuilder();
            
            builder.AddLogType(logType);
            builder.AddFeatureType(featureType);
            
            var message = builder.Create(loggerProfile);
            
            Log(builder, message);
            
            return obj;
        }
        
        [HideInCallstack]
        public static MessageBuilder Setup(this object obj)
        {
            var builder = obj != null ?
                new MessageBuilder(obj.ToString()) : new MessageBuilder();
            
            return builder;
        }
        
        [HideInCallstack]
        public static MessageBuilder Log(this object obj, FeatureType featureType)
        {
            var builder = obj != null ?
                new MessageBuilder(obj.ToString()) : new MessageBuilder();
            
            builder.AddFeatureType(featureType);
            
            var message = builder.Create(loggerProfile);
            
            Log(builder, message);
            
            return builder;
        }
        
        public static void Init(LoggerProfile loggerProfile)
        {
            Logger.loggerProfile = loggerProfile;
        }
        
        [HideInCallstack]
        private static void Log(MessageBuilder builder, string message)
        {
            SaveLogs(builder);
            
            if (loggerProfile.FeatureMask.HasFlag(builder.FeatureType) == false)
                return;
            
            if (loggerProfile.LogMask.HasFlag(builder.LogType) == false)
            {
                return;
            }
            
            switch (builder.LogType)
            {
                case LogType.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogType.Error:
                    Debug.LogError(message);
                    break;
                case LogType.Exception:
                    Debug.LogException(new Exception(message));
                    break;
                case LogType.Assertion:
                    Debug.LogAssertion(message);
                    break;
                
                case LogType.Default:
                default:
                    Debug.Log(message);
                    break;
            }
        }
        
        private static void SaveLogs(MessageBuilder builder)
        {
            var value = builder.Create(new ExportLoggerProfile());
            
            if (logs.Count > 0 && logs[^1].ToString().Equals(value))
            {
                logsCounts[^1] += 1;
            }
            else
            {
                logs.Add(value);
                logsCounts.Add(1);
                logsStackTraces.Add(Environment.StackTrace);
            }
            
            if (logs.Count >= LOG_CAPACITY)
            {
                logs.RemoveRange(0, LOG_CAPACITY/2);
                logsCounts.RemoveRange(0, LOG_CAPACITY/2);
                logsStackTraces.RemoveRange(0, LOG_CAPACITY/2);
            }
        }
    }
}