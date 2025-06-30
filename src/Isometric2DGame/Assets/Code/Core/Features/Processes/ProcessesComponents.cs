using Entitas;

namespace Code.Core.Features.Processes
{
    [Game] public class Processed : IComponent { }
    [Game] public class Process : IComponent { }
    [Game] public class ProcessPause : IComponent { }
    [Game] public class ProducerId : IComponent { public int Value; }
    
    [Game] public class Damage : IComponent { public float Value; }
}