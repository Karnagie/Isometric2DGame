using Code.Core.Features.ActionPlanning.Systems;
using Code.Infrastructure.Systems;

namespace Code.Core.Features.ActionPlanning
{
    public sealed class ActionPlanningFeature : Feature
    {
        public ActionPlanningFeature(ISystemFactory systems)
        {
            Add(systems.Create<SetCurrentActionSystem>());
            
            Add(systems.Create<UpdateWeightsSystem>());
            Add(systems.Create<UpdateCurrentActionByCompletingSystem>());
            Add(systems.Create<UpdateCurrentActionByRequestSystem>());
        }
    }
}