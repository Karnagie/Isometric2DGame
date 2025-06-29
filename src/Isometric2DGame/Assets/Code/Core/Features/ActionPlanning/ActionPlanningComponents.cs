using System.Collections.Generic;
using Code.Core.Features.ActionPlanning.EnemyActions;
using Entitas;

namespace Code.Core.Features.ActionPlanning
{
    [Game] public class ActionPlanner : IComponent { }
    [Game] public class CurrentAction : IComponent { public Action Value; }
    [Game] public class Actions : IComponent { public Dictionary<string, Action> Value; }
    [Game] public class ChangeActionRequest : IComponent { }
    
    [Game] public class PlayerInRadius : IComponent { }
    [Game] public class PlayerInAttackRadius : IComponent { }
}