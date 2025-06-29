using System.Collections.Generic;
using Code.Core.Features.Enemies;

namespace Code.Core.GOAP
{
    public interface IGoapPlanner 
    {
        ActionPlan Plan(IGoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null);
    }
}