using Entitas;

namespace Code.Core.Features.ActionPlanning.Systems
{
    public class UpdateWeightsSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _planners;

        public UpdateWeightsSystem(GameContext game)
        {
            _planners = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ActionPlanner,
                    GameMatcher.Actions));
        }

        public void Execute()
        {
            foreach (var planner in _planners)
            {
                foreach (var keyValuePair in planner.Actions)
                {
                    keyValuePair.Value.UpdateWeight();
                }
            }
        }
    }
}