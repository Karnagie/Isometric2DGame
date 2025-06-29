using System.Collections.Generic;
using Code.Core.Features.ActionPlanning.EnemyActions;
using Entitas;

namespace Code.Core.Features.ActionPlanning.Systems
{
    public class SetCurrentActionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _planners;
        
        private readonly List<GameEntity> _buffer = new(4);

        public SetCurrentActionSystem(GameContext game)
        {
            _planners = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ActionPlanner,
                    GameMatcher.Actions)
                .NoneOf(GameMatcher.CurrentAction));
        }

        public void Execute()
        {
            foreach (var planner in _planners.GetEntities(_buffer))
            {
                Action maxAction = null;
                var maxWeight = float.MinValue;

                foreach (var action in planner.Actions)
                {
                    if (action.Value.Weight > maxWeight)
                    {
                        maxWeight = action.Value.Weight;
                        maxAction = action.Value;
                    }
                }
                
                planner.ReplaceCurrentAction(maxAction!);
                maxAction!.Start();
            }
        }
    }
}