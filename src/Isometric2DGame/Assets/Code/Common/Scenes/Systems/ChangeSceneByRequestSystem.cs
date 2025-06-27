using System;
using System.Collections.Generic;
using Code.Infrastructure.Loggers;
using Code.Infrastructure.Loggers.Unity;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachines;
using Entitas;

namespace Code.Common.Scenes.Systems
{
    public class ChangeSceneByRequestSystem : IExecuteSystem
    {
        private readonly IGroup<MetaEntity> _requests;
        
        private readonly IGameStateMachine _gameStateMachine;
        private List<MetaEntity> _buffer = new (1);

        public ChangeSceneByRequestSystem(MetaContext meta, IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _requests = meta.GetGroup(MetaMatcher
                .AllOf(
                    MetaMatcher.SceneChangeRequest,
                    MetaMatcher.StateType,
                    MetaMatcher.SceneName
                    ));
        }

        public void Execute()
        {
            foreach (var request in _requests.GetEntities(_buffer))
            {
                switch (request.StateType)
                {
                    case StateType.None:
                        break;
                    case StateType.Core:
                        _gameStateMachine.Enter<LoadingCoreState, string>(request.SceneName);
                        break;
                    default:
                        $"Not implemented request to open scene with type {request.StateType}"
                            .Setup()
                            .AddFeatureType(FeatureType.Meta)
                            .AddLogType(LogType.Error)
                            .Log();
                        break;
                }

                request.isDestructed = true;
            }
        }
    }
}