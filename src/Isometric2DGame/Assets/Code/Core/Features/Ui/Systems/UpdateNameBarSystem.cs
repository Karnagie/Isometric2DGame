using Entitas;

namespace Code.Core.Features.Ui.Systems
{
    public class UpdateNameBarSystem : IExecuteSystem
    {
        private static GameContext GameContext => Contexts.sharedInstance.game;
        
        private readonly IGroup<GameEntity> _namedEntities;
        private readonly IGroup<GameEntity> _targets;

        public UpdateNameBarSystem(GameContext game)
        {
            _namedEntities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TargetId,
                    GameMatcher.WorldPosition,
                    GameMatcher.TextField,
                    GameMatcher.Health));
            
            _targets = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Id,
                    GameMatcher.CurrentHp,
                    GameMatcher.WorldPosition,
                    GameMatcher.MaxHp));
        }

        public void Execute()
        {
            foreach (var named in _namedEntities)
            {
                var target = GameContext.GetEntityWithId(named.TargetId);
                if(_targets.ContainsEntity(target) == false)
                    continue;

                named.TextField.text = $"{(int)target.CurrentHp}/{(int)target.MaxHp}";
                named.ReplaceWorldPosition(target.WorldPosition+named.Offset);
            }
        }
    }
}