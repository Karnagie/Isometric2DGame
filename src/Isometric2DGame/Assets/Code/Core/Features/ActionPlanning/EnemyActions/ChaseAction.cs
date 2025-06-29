using Entitas;
using UnityEngine;

namespace Code.Core.Features.ActionPlanning.EnemyActions
{
    public class ChaseAction : Action
    {
        private readonly IGroup<GameEntity> _chasers;
        private readonly IGroup<GameEntity> _targets;

        private readonly GameContext _game;

        public ChaseAction(GameEntity entity, string name, GameContext game) : base(entity, name)
        {
            _game = game;

            _chasers = _game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.PlayerInRadius,
                    GameMatcher.WorldPosition,
                    GameMatcher.TargetId,
                    GameMatcher.Radius));
            
            _targets = _game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Id,
                    GameMatcher.WorldPosition));
        }
        
        protected override float UpdateWeightInternal(GameEntity entity)
        {
            if (CanChase(entity) == false)
                return -1f;
            
            var distance = Vector3.Distance(entity.WorldPosition, _game.GetEntityWithId(entity.TargetId).WorldPosition);
            var weight = distance/entity.Radius;
            
            return weight;
        }

        protected override bool IsCompleteInternal(GameEntity entity)
        {
            if (CanChase(entity) == false)
                return true;
            
            var distance = Vector3.Distance(entity.WorldPosition, _game.GetEntityWithId(entity.TargetId).WorldPosition);
            if (distance < 0.1f)
                return true;

            return false;
        }

        private bool CanChase(GameEntity entity)
        {
            if (_chasers.ContainsEntity(entity) == false)
                return false;

            var target = _game.GetEntityWithId(entity.TargetId);
            if (target == null)
                return false;
            if (_targets.ContainsEntity(target) == false)
                return false;

            return true;
        }
    }
}