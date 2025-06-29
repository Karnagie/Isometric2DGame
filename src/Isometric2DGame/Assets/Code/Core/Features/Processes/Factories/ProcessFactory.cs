using System;
using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;

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