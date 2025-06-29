using UnityEngine;

namespace Code.Core.GOAP
{
    public class RoutingStrategy : IActionStrategy 
    {
        private readonly GameEntity _entity;

        public bool CanPerform => !Complete;
        public bool Complete => IsComplete();
    
        public RoutingStrategy(GameEntity entity)
        {
            _entity = entity;
        }

        public void Start()
        {
            _entity.isRouting = true;
        }

        private bool IsComplete()
        {
            if (Vector3.Distance(_entity.WorldPosition, _entity.CurrentRoutePoint) < 0.1f)
                return true;

            return false;
        }
    }
}