using System;
using UnityEngine;

namespace Code.Core.GOAP
{
    public class AgentBelief 
    {
        public string Name { get; }

        private Func<bool> _condition = () => false;
        private Func<Vector3> _observedLocation = () => Vector3.zero;
    
        public Vector3 Location => _observedLocation();
    
        private AgentBelief(string name) 
        {
            Name = name;
        }
    
        public bool Evaluate() => _condition();

        public class Builder 
        {
            private readonly AgentBelief _belief;
        
            public Builder(string name)
            {
                _belief = new AgentBelief(name);
            }
        
            public Builder WithCondition(Func<bool> condition) 
            {
                _belief._condition = condition;
                return this;
            }
        
            public Builder WithLocation(Func<Vector3> observedLocation) 
            {
                _belief._observedLocation = observedLocation;
                return this;
            }
        
            public AgentBelief Build() 
            {
                return _belief;
            }
        }
    }
}