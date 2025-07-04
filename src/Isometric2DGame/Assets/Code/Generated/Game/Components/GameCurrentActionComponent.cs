//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCurrentAction;

    public static Entitas.IMatcher<GameEntity> CurrentAction {
        get {
            if (_matcherCurrentAction == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CurrentAction);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCurrentAction = matcher;
            }

            return _matcherCurrentAction;
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

    public Code.Core.Features.ActionPlanning.CurrentAction currentAction { get { return (Code.Core.Features.ActionPlanning.CurrentAction)GetComponent(GameComponentsLookup.CurrentAction); } }
    public Code.Core.Features.ActionPlanning.EnemyActions.Action CurrentAction { get { return currentAction.Value; } }
    public bool hasCurrentAction { get { return HasComponent(GameComponentsLookup.CurrentAction); } }

    public GameEntity AddCurrentAction(Code.Core.Features.ActionPlanning.EnemyActions.Action newValue) {
        var index = GameComponentsLookup.CurrentAction;
        var component = (Code.Core.Features.ActionPlanning.CurrentAction)CreateComponent(index, typeof(Code.Core.Features.ActionPlanning.CurrentAction));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceCurrentAction(Code.Core.Features.ActionPlanning.EnemyActions.Action newValue) {
        var index = GameComponentsLookup.CurrentAction;
        var component = (Code.Core.Features.ActionPlanning.CurrentAction)CreateComponent(index, typeof(Code.Core.Features.ActionPlanning.CurrentAction));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveCurrentAction() {
        RemoveComponent(GameComponentsLookup.CurrentAction);
        return this;
    }
}
