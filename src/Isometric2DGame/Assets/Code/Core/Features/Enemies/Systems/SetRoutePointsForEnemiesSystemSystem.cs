using System.Linq;
using Code.Core.Levels;
using Entitas;

namespace Code.Core.Features.Enemies.Systems
{
    public class SetRoutePointsForEnemiesSystemSystem : IInitializeSystem
    {
        private readonly IGroup<GameEntity> _enemies;
        
        private readonly ILevelDataProvider _levelDataProvider;

        public SetRoutePointsForEnemiesSystemSystem(GameContext game, ILevelDataProvider levelDataProvider)
        {
            _levelDataProvider = levelDataProvider;
            _enemies = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Enemy)
            );
        }

        public void Initialize()
        {
            foreach (var enemy in _enemies)
            {
                enemy.AddRoutePoints(_levelDataProvider.RoutePoints.ToList());
            }
        }
    }
}