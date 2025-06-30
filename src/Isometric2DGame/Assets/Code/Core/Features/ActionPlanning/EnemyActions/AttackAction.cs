using Code.Core.Features.Processes;
using Code.Core.Features.Processes.Factories;

namespace Code.Core.Features.ActionPlanning.EnemyActions
{
    public class AttackAction : Action
    {
        private readonly IProcessFactory _processFactory;
        
        public AttackAction(GameEntity entity, string name, IProcessFactory processFactory) : base(entity, name)
        {
            _processFactory = processFactory;
        }
        
        protected override float UpdateWeightInternal(GameEntity entity)
        {
            var hasPlayer = entity.isPlayerInAttackRadius ? 1 : 0f;
            
            return hasPlayer;
        }

        protected override bool IsCompleteInternal(GameEntity entity)
        {
            _processFactory.Damage(entity.TargetId, P.Damage(0.1f));
            return true;
        }
    }
}