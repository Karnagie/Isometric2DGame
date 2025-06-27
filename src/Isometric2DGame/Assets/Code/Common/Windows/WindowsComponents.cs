using Entitas;

namespace Code.Common.Windows
{
    [Game] public class WindowRequest : IComponent { }
    [Game] public class WindowIdComponent : IComponent { public WindowId Value; }
    [Game] public class Open : IComponent { }
    [Game] public class Close : IComponent { }
}