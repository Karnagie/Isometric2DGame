using Code.Infrastructure.Loggers.Unity;

namespace Code.Core.Features.ActionPlanning.EnemyActions
{
    public class AttackAction : Action
    {
        public AttackAction(GameEntity entity, string name) : base(entity, name) { }
        
        protected override float UpdateWeightInternal(GameEntity entity)
        {
            var hasPlayer = entity.isPlayerInAttackRadius ? 1 : 0f;
            
            return hasPlayer;
        }

        protected override bool IsCompleteInternal(GameEntity entity)
        {
            $"Attacking!".Log();
            return true;
        }
    }
}