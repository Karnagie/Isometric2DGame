using Code.Core.Features.ActionPlanning.EnemyActions;
using Entitas;

namespace Code.Core.Features.ActionPlanning.Systems
{
    public class UpdateCurrentActionByCompletingSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _planners;

        public UpdateCurrentActionByCompletingSystem(GameContext game)
        {
            _planners = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ActionPlanner,
                    GameMatcher.CurrentAction,
                    GameMatcher.Actions));
        }

        public void Execute()
        {
            foreach (var planner in _planners)
            {
                if(planner.CurrentAction.Complete == false)
                    continue;
                
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