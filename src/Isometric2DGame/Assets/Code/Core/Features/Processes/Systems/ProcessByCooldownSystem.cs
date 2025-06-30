using Entitas;

namespace Code.Core.Features.Processes.Systems
{
    public class ProcessByCooldownSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _processes;

        public ProcessByCooldownSystem(GameContext game)
        {
            _processes = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Process,
                    GameMatcher.CooldownUp));
        }

        public void Execute()
        {
            foreach (var process in _processes)
            {
                process.isProcess = false;
                process.isProcessed = true;
            }
        }
    }
}