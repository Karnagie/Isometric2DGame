using Entitas;

namespace Code.Common.Windows.Systems
{
    public class CloseWindowByRequestSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _requests;
        
        private readonly IWindowService _windowService;

        public CloseWindowByRequestSystem(GameContext game, IWindowService windowService)
        {
            _windowService = windowService;
            _requests = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WindowRequest,
                    GameMatcher.WindowId,
                    GameMatcher.Close
                ));
        }

        public void Execute()
        {
            foreach (var request in _requests)
            {
                _windowService.Close(request.WindowId);
                
                request.isDestructed = true;
            }
        }
    }
}