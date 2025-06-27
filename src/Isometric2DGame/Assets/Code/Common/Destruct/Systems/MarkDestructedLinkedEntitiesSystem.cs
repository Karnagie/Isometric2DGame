using System.Collections.Generic;
using Entitas;

namespace Code.Common.Destruct.Systems
{
    public class MarkDestructedLinkedEntitiesSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly GameContext _game;
        
        private readonly List<GameEntity> _buffer = new(64);

        public MarkDestructedLinkedEntitiesSystem(GameContext game)
        {
            _game = game;
            _entities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Destructed,
                    GameMatcher.Id));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities(_buffer))
            {
                foreach (GameEntity linked in _game.GetEntitiesWithEntityLink(entity.Id))
                    linked.isDestructed = true;
            }
        }
    }
}