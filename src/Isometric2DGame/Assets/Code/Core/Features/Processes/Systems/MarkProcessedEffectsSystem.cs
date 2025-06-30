using Entitas;

namespace Code.Core.Features.Processes.Systems
{
    public class MarkProcessedEffectsSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _effects;

        public MarkProcessedEffectsSystem(GameContext game)
        {
            _effects = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Processed));
        }

        public void Execute()
        {
            foreach (var effect in _effects)
            {
                effect.isDestructed = true;
            }
        }
    }
}