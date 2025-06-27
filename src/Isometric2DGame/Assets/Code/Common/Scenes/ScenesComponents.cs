using Entitas;

namespace Code.Common.Scenes
{
    [Meta] public class SceneChangeRequest : IComponent { }
    [Meta] public class StateTypeComponent : IComponent { public StateType Value; }
    [Meta] public class SceneName : IComponent { public string Value; }
}