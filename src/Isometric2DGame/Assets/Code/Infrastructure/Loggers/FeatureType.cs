using System;

namespace Code.Infrastructure.Loggers
{
    [Flags]
    public enum FeatureType
    {
        Temporary = 1,
        Infrastructure = 2,
        GameStateMachine = 4,
        Core = 8,
        Meta = 16,
        UI = 32,
    }
}