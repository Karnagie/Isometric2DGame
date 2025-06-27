using Code.Infrastructure.Loggers;
using Code.Infrastructure.Loggers.Unity;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachines;
using RSG;
using UnityEngine;
using Zenject;
using LogType = Code.Infrastructure.Loggers.LogType;

namespace Code.Infrastructure.Bootstrapping
{
    public class Bootstrap : MonoBehaviour, IInitializable
    {
        public LoggerInitializer LoggerInitializer;
        
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void Initialize()
        {
            LoggerInitializer.Init();
            Promise.UnhandledException += LogPromiseException;
            _gameStateMachine.Enter<BootstrapState>();
        }
        
        private void LogPromiseException(object sender, ExceptionEventArgs e)
        {
            e.Exception
                .Setup()
                .AddFeatureType(FeatureType.Infrastructure)
                .AddLogType(LogType.Error)
                .Log();
        }
    }
}