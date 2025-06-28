using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Core.Features.Stats
{
    public static class InitStats
    {
        public static Dictionary<StatId, float> EmptyStatDictionary()
        {
            return Enum.GetValues(typeof(StatId))
                .Cast<StatId>()
                .Except(new[] {StatId.Unknown})
                .ToDictionary(x => x, _ => 0f);
        }
    }
}