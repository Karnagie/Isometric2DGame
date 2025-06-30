using System;
using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Core.Features.Cooldowns;
using Code.Core.Features.Stats;

namespace Code.Core.Features.Processes.Factories
{
    public class ProcessFactory : IProcessFactory
    {
        private readonly Dictionary<string, List<Func<int, ProcessSetup, GameEntity>>> _processes;

        public ProcessFactory()
        {
            _processes = new()
            {
                {"damage", new List<Func<int, ProcessSetup, GameEntity>>(){Damage}},
                {"speed-up", new List<Func<int, ProcessSetup, GameEntity>>(){SpeedUp}},
            };
        }
        
        public GameEntity Damage(int targetId, ProcessSetup setup)
        {
            return CreateEntity.Empty()
                    .With(x => x.isProcess = true)
                    .AddDamage(setup.Value)
                    .AddTargetId(targetId)
                ;
        }
        
        public GameEntity SpeedUp(int targetId, ProcessSetup setup)
        {
            return CreateEntity.Empty()
                    .With(x => x.isProcess = true)
                    .AddStatChange(StatId.Speed)
                    .AddValue(5)
                    .AddTargetId(targetId)
                    .PutOnCooldown(setup.Duration)
                ;
        }
        
        public GameEntity[] Process(string process, int targetId, ProcessSetup setup)
        {
            var processEntities = new GameEntity[_processes[process].Count];

            var index = 0;
            foreach (var processFunc in _processes[process])
            {
                var entity = processFunc.Invoke(targetId, setup);
                processEntities[index++] = entity;
            }

            return processEntities;
        }
    }
}