using UnityEngine;

namespace Code.Infrastructure.Loggers.Unity
{
    public class LoggerInitializer : MonoBehaviour
    {
        public void Init()
        {
            var defaultProfile = Resources.Load<LoggerProfile>("Profiles/DefaultProfile");
            var profiles = Resources.LoadAll<LoggerProfile>("Profiles/LocalProfiles");
            
            if (profiles.Length == 1)
            {
                Logger.Init(profiles[0]);
                
                $"Local profile used: {profiles[0].name}"
                    .Setup()
                    .AddLogType(LogType.Default)
                    .AddFeatureType(FeatureType.Infrastructure)
                    .Log();
                
                return;
            }
            
            if (profiles.Length > 1)
            {
                Logger.Init(profiles[0]);
                
                $"Found multiple logger profiles, used first: {profiles[0].name}"
                    .Setup()
                    .AddLogType(LogType.Warning)
                    .AddFeatureType(FeatureType.Infrastructure)
                    .Log();
                
                return;
            }
            
            Logger.Init(defaultProfile);
            
            $"Local profile not found, used default: {defaultProfile.name}"
                .Setup()
                .AddLogType(LogType.Default)
                .AddFeatureType(FeatureType.Infrastructure)
                .Log();
        }
    }
}