using Code.Core.Features.ActionPlanning.EnemyActions;
using Code.Infrastructure.Loggers.Unity;
using Entitas;
using UnityEngine;

namespace Code.Core.Features.ActionPlanning.Systems
{
    public class UpdateCurrentActionByRequestSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _planners;
        private readonly IGroup<GameEntity> _request;

        public UpdateCurrentActionByRequestSystem(GameContext game)
        {
            _planners = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ActionPlanner,
                    GameMatcher.Id,
                    GameMatcher.CurrentAction,
                    GameMatcher.Actions));
            
            _request = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ChangeActionRequest,
                    GameMatcher.TargetId));
        }

        public void Execute()
        {
            foreach (var planner in _planners)
            foreach (var request in _request)
            {
                if(request.TargetId != planner.Id)
                    continue;
                
                request.isDestructed = true;
                
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