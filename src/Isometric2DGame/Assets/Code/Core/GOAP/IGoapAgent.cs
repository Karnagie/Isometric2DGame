using System.Collections.Generic;

namespace Code.Core.GOAP
{
    public interface IGoapAgent
    {
        HashSet<AgentAction> Actions { get; }
    }
}