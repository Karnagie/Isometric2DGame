using System;

namespace Code.Infrastructure.Loggers
{
    [Flags]
    public enum LogType
    {
        Default = 1,
        Warning = 2,
        Error = 4,
        Exception = 8,
        Assertion = 16
    }
}