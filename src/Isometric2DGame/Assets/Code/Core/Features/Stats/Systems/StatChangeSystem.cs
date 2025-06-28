using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.EntityIndices;
using Code.Common.Extensions;
using Entitas;

namespace Code.Core.Features.Stats.Systems
{
    public class StatChangeSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _statOwners;
        private readonly GameContext _game;

        public StatChangeSystem(GameContext game)
        {
            _game = game;

            _statOwners = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Id,
                    GameMatcher.BaseStats,
                    GameMatcher.StatModifiers));
        }

        public void Execute()
        {
            foreach (var statOwner in _statOwners)
            foreach (var stat in statOwner.BaseStats.Keys)
            {
                statOwner.StatModifiers[stat] = 0;
        
                foreach (var statChange in _game.TargetStatChanges(stat, statOwner.Id)) 
                    statOwner.StatModifiers[stat] += statChange.Value;
            }
        }
    }
}