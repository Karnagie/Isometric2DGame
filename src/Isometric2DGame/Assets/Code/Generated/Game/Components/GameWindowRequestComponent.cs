//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherWindowRequest;

    public static Entitas.IMatcher<GameEntity> WindowRequest {
        get {
            if (_matcherWindowRequest == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.WindowRequest);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherWindowRequest = matcher;
            }

            return _matcherWindowRequest;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Code.Common.Windows.WindowRequest windowRequestComponent = new Code.Common.Windows.WindowRequest();

    public bool isWindowRequest {
        get { return HasComponent(GameComponentsLookup.WindowRequest); }
        set {
            if (value != isWindowRequest) {
                var index = GameComponentsLookup.WindowRequest;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : windowRequestComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}
