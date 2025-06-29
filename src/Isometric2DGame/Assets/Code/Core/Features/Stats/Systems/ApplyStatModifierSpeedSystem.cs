using Code.Common.Extensions;
using Entitas;

namespace Code.Core.Features.Stats.Systems
{
    public class ApplyStatModifierSpeedSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _statOwners;

        public ApplyStatModifierSpeedSystem(GameContext game)
        {
            _statOwners = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.StatModifiers,
                    GameMatcher.Speed
                ));
        }

        public void Execute()
        {
            foreach (GameEntity statOwner in _statOwners)
            {
                statOwner.ReplaceSpeed(statOwner.Speed+MoveSpeed(statOwner).ZeroIfNegative());
            }
        }

        private static float MoveSpeed(GameEntity statOwner)
        {
            return statOwner.StatModifiers[StatId.Speed];
        }
    }
}