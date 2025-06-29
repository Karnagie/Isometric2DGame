using System;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Core.GOAP
{
    public class MoveStrategy : IActionStrategy 
    {
        private readonly Func<Vector3> _destination;
        private readonly GameEntity _entity;

        public bool CanPerform => !Complete;
        public bool Complete => IsComplete();
    
        public MoveStrategy(GameEntity entity, Func<Vector3> destination) 
        {
            _entity = entity;
            _destination = destination;
        }
    
        public void Start() => _entity.ReplaceMovementPoint(_destination());
        
        public void Stop()
        {
            if (_entity.hasMovementPoint) 
                _entity.RemoveMovementPoint();
        }
        
        private bool IsComplete()
        {
            if (Vector3.Distance(_entity.WorldPosition, _entity.MovementPoint) < 0.1f)
                return true;

            return false;
        }
    }
}