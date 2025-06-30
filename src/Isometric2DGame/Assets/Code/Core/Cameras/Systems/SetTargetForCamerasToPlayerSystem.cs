using Code.Core.Cameras.CameraManagement;
using Entitas;

namespace Code.Core.Cameras.Systems
{
    public class SetTargetForCamerasToPlayerSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _players;
        private readonly IGroup<GameEntity> _requests;

        private readonly ICameraService _cameraService;

        public SetTargetForCamerasToPlayerSystem(GameContext game, ICameraService cameraService)
        {
            _cameraService = cameraService;
            
            _requests = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.UpdateCameraTargetRequest
                ));
            
            _players = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Player,
                    GameMatcher.Transform
                ));
        }
        
        public void Execute()
        {
            foreach (var request in _requests)
            {
                request.isDestructed = true;
                foreach (var player in _players)
                {
                    _cameraService.MainCamera.Target.TrackingTarget = player.Transform;
                }
            }
        }
    }
}