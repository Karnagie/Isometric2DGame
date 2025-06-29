using System.Collections.Generic;
using Code.Core.Features.Cooldowns;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Code.Core.Features.Lifetime.Systems
{
    public class FinalizeDeathSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(128);

        public FinalizeDeathSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ProcessingDeath,
                    GameMatcher.CooldownUp));
        }

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                entity.isDestructed = true;
            }
        }
    }
}