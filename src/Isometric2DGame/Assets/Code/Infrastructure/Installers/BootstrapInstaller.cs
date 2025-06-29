using Code.Common.EntityIndices;
using Code.Common.StaticData;
using Code.Common.Windows;
using Code.Core.Cameras.CameraManagement;
using Code.Core.Common.Collisions;
using Code.Core.Common.Physics;
using Code.Core.Common.Random;
using Code.Core.Common.Time;
using Code.Core.Features.Enemies.Factories;
using Code.Core.Features.Players.Factories;
using Code.Core.Input.Services;
using Code.Core.Levels;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Bootstrapping;
using Code.Infrastructure.Identifiers;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachines;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View.Factory;
using Code.Progress.Provider;
using Code.Progress.SaveLoad;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
  {
    public Bootstrap Bootstrap;
    
    public override void InstallBindings()
    {
      BindBootstrap();
      BindInputService();
      BindInfrastructureServices();
      BindAssetManagementServices();
      BindCommonServices();
      BindSystemFactory();
      BindUIFactories();
      BindContexts();
      BindGameplayServices();
      BindUIServices();
      BindCoreFactories();
      BindStateMachine();
      BindStateFactory();
      BindGameStates();
      BindProgressServices();
      BindGameEntityIndices();
    }

    private void BindGameEntityIndices()
    {
      Container.BindInterfacesAndSelfTo<GameEntityIndices>().AsSingle();
    }

    private void BindBootstrap()
    {
      Container.BindInterfacesTo<Bootstrap>().FromInstance(Bootstrap).AsSingle();
    }

    private void BindStateMachine()
    {
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
    }

    private void BindStateFactory()
    {
      Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
    }

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadProgressState>().AsSingle();
      Container.BindInterfacesAndSelfTo<ActualizeProgressState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadingCoreState>().AsSingle();
      Container.BindInterfacesAndSelfTo<CoreEnterState>().AsSingle();
      Container.BindInterfacesAndSelfTo<CoreLoopState>().AsSingle();
    }

    private void BindContexts()
    {
      Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();
      
      Container.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
      Container.Bind<InputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
      Container.Bind<MetaContext>().FromInstance(Contexts.sharedInstance.meta).AsSingle();
    }

    private void BindProgressServices()
    {
      Container.Bind<IProgressProvider>().To<ProgressProvider>().AsSingle();
      Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
    }

    private void BindGameplayServices()
    {
      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
      Container.Bind<ILevelDataProvider>().To<LevelDataProvider>().AsSingle();
    }

    private void BindCoreFactories()
    {
      Container.Bind<IEntityViewFactory>().To<EntityViewFactory>().AsSingle();
      Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
      Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
    }
    
    private void BindSystemFactory()
    {
      Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();
    }

    private void BindInfrastructureServices()
    {
      Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
      Container.Bind<IIdentifierService>().To<IdentifierService>().AsSingle();
      Container.Bind<ICameraService>().To<CameraService>().AsSingle();
      Container.Bind<ILoadingViewService>().To<LoadingViewService>().AsSingle();
    }

    private void BindAssetManagementServices()
    {
      Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
    }

    private void BindCommonServices()
    {
      Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
      Container.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle();
      Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
      Container.Bind<IPhysicsResolver>().To<UnityPhysicsResolver>().AsSingle();
      Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
    }

    private void BindInputService()
    {
      Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
    }

    private void BindUIServices()
    {
      Container.Bind<IWindowService>().To<WindowService>().AsSingle();
    }

    private void BindUIFactories()
    {
      Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
    }
  }
}