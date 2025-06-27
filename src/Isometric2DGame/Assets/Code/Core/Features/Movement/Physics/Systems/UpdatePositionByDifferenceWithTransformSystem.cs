using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Core.Common.Physics;
using Entitas;
using UnityEngine;

namespace Code.Core.Features.Movement.Physics.Systems
{
    public class UpdatePositionByDifferenceWithTransformSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;
        
        private readonly IPhysicsService _physicsService;

        private readonly Dictionary<GameEntity, Vector3> _positions = new();
        
        private readonly List<GameEntity> _penetratedList = new(32);

        public UpdatePositionByDifferenceWithTransformSystem(
            GameContext game, 
            IPhysicsService physicsService)
        {
            _physicsService = physicsService;
            _movers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Transform,
                    GameMatcher.SyncWorldPositionWithTransform));
        }
    
        public void Execute()
        {
            _penetratedList.Clear();
            
            foreach (GameEntity mover in _movers)
            {
                if (_positions.ContainsKey(mover) == false)
                    _positions.Add(mover, mover.WorldPosition);
                
                var penetrated = CalculateStaticCollisions(mover);
                
                var beforePos = mover.WorldPosition;
                CalculateEntityCollision(mover);
                var penetrated1 =  CalculateEntityCollision(mover);;
                if (penetrated1)
                {
                    var backMove = (mover.WorldPosition - beforePos);
                    mover.ReplaceWorldPosition(mover.WorldPosition + backMove * 0.5f);   
                    mover.Transform.position = mover.WorldPosition;
                }
                
                penetrated = CalculateStaticCollisions(mover);
                
                if(penetrated)
                    _penetratedList.Add(mover);
            }

            foreach (GameEntity mover in _movers)
            {
                if (_penetratedList.Contains(mover) == false)
                {
                    _positions[mover] = mover.WorldPosition;
                }
            }
        }

        private bool CalculateStaticCollisions(GameEntity mover)
        {
            var staticColliders = 
                _physicsService.FindNonEntityColliders(mover, mover.WorldPosition, 2f);
                
            var newPos = new Vector3();
                
            foreach (var staticCollider in staticColliders)
            {
                newPos += _physicsService.CalculatePosition(mover, mover.WorldPosition,
                    staticCollider)-mover.WorldPosition;
            }
                
            mover.ReplaceWorldPosition(mover.WorldPosition+newPos.SetY(mover.WorldPosition.y));
            mover.Transform.position = mover.WorldPosition;

            var penetrated = false;
            var newPenetratedPos = Vector3.zero;
            foreach (var staticCollider in staticColliders)
            {
                var stillHasPenetration =
                    _physicsService.GetPenetration(mover, staticCollider);
                if (stillHasPenetration.Item1 &&
                    stillHasPenetration.Item3 > 0.01f)
                {
                    newPenetratedPos += (stillHasPenetration.Item2).normalized * stillHasPenetration.Item3;
                        
                    penetrated = true;
                }
            }

            if (penetrated)
            {
                var backVector = (_positions[mover] - (mover.WorldPosition + newPenetratedPos)).normalized;

                var backVectorD = (_positions[mover] - mover.WorldPosition).magnitude;
                newPenetratedPos += backVector * backVectorD * 0.1f;
                    
                newPos = mover.WorldPosition + newPenetratedPos;
                    
                mover.ReplaceWorldPosition(newPos.SetY(mover.WorldPosition.y));
                mover.Transform.position = newPos;
            }

            return penetrated;
        }

        private bool CalculateEntityCollision(GameEntity mover)
        {
            var entities = 
                _physicsService.FindCollidedEntities(mover, mover.WorldPosition, 2f);
            var penetrated = false;

            foreach (var entity in entities)
            {
                var prevMover = _positions[mover];
                var moverDistance = (mover.WorldPosition - prevMover).magnitude;
                var prevEntity = _positions[entity];
                var entityDistance = (entity.WorldPosition - prevEntity).magnitude;
                var summDistance = moverDistance + entityDistance;
                
                if(summDistance == 0)
                    continue;

                var newPos = _physicsService.CalculatePosition(entity, entity.WorldPosition,
                    mover);

                var insideVector = (newPos-entity.WorldPosition);
                var entityNewPos = 
                    entity.WorldPosition + (insideVector * (entityDistance / summDistance));
                var moverNewPos = 
                    mover.WorldPosition - (insideVector * (moverDistance / summDistance));
                
                entity.ReplaceWorldPosition(entityNewPos.SetY(entity.WorldPosition.y));   
                entity.Transform.position = entity.WorldPosition;
                
                mover.ReplaceWorldPosition(moverNewPos.SetY(mover.WorldPosition.y));   
                mover.Transform.position = mover.WorldPosition;
                penetrated = true;
            }

            return penetrated;
        }
    }
}