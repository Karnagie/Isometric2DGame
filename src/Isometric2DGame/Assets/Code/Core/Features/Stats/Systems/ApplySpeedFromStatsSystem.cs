using Code.Common.Extensions;
using Entitas;

namespace Code.Core.Features.Stats.Systems
{
    public class ApplySpeedFromStatsSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _statOwners;

        public ApplySpeedFromStatsSystem(GameContext game)
        {
            _statOwners = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.BaseStats,
                    GameMatcher.Speed
                ));
        }

        public void Execute()
        {
            foreach (GameEntity statOwner in _statOwners)
            {
                statOwner.ReplaceSpeed(MoveSpeed(statOwner).ZeroIfNegative());
            }
        }

        private static float MoveSpeed(GameEntity statOwner)
        {
            return statOwner.BaseStats[StatId.Speed];
        }
    }
}