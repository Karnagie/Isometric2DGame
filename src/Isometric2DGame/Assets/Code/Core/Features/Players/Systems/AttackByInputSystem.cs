using System.Linq;
using Code.Core.Common.Physics;
using Code.Core.Features.Processes;
using Code.Core.Features.Processes.Factories;
using Code.Infrastructure.Loggers.Unity;
using Entitas;
using UnityEngine;

namespace Code.Core.Features.Players.Systems
{
    public class AttackByInputSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _players;
        private readonly IGroup<InputEntity> _inputs;
        
        private readonly IProcessFactory _processFactory;
        private readonly IPhysicsService _physicsService;

        public AttackByInputSystem(
            GameContext game,
            InputContext input, 
            IProcessFactory processFactory,
            IPhysicsService physicsService)
        {
            _physicsService = physicsService;
            _processFactory = processFactory;
            _players = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Player,
                    GameMatcher.WorldPosition));

            _inputs = input.GetGroup(InputMatcher
                .AllOf(
                    InputMatcher.Input,
                    InputMatcher.Attacking));
        }

        public void Execute()
        {
            foreach (var input in _inputs)
            foreach (var player in _players)
            {
                var enemies = _physicsService.CircleCast(
                    player.WorldPosition, 
                    5,
                    LayerMask.NameToLayer("Enemy"));
                
                foreach (var enemy in enemies)
                {
                    _processFactory.Damage(enemy.Id, P.Damage(10));
                }
            }
        }
    }
}