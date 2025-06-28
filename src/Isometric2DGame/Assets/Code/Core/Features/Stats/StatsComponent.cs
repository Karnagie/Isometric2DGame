using System.Collections.Generic;
using Entitas;

namespace Code.Core.Features.Stats
{
    [Game] public class BaseStats : IComponent { public Dictionary<StatId, float> Value; }
    [Game] public class StatModifiers : IComponent { public Dictionary<StatId, float> Value; }
    
    [Game] public class StatChange : IComponent { public StatId Value; }
}