using Entitas;

namespace Code.Common.Windows.Systems
{
    public class OpenWindowByRequestSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _requests;
        
        private readonly IWindowService _windowService;

        public OpenWindowByRequestSystem(GameContext game, IWindowService windowService)
        {
            _windowService = windowService;
            _requests = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WindowRequest,
                    GameMatcher.WindowId,
                    GameMatcher.Open
                    ));
        }

        public void Execute()
        {
            foreach (var request in _requests)
            {
                _windowService.Open(request.WindowId);

                request.isDestructed = true;
            }
        }
    }
}