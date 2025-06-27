using System;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Loggers.Unity;
using UnityEngine;
using Zenject;
using LogType = Code.Infrastructure.Loggers.LogType;

namespace Code.Infrastructure.View.Factory
{
  public class EntityViewFactory : IEntityViewFactory
  {
    private readonly IAssetProvider _assetProvider;
    private readonly IInstantiator _instantiator;
    private readonly Vector3 _farAway = new(-999, 999, 0);

    public EntityViewFactory(IAssetProvider assetProvider, IInstantiator instantiator)
    {
      _assetProvider = assetProvider;
      _instantiator = instantiator;
    }
    
    public EntityBehaviour CreateViewForEntity(GameEntity entity, Transform parenTransform = null, 
      bool usePrefabLocalPosition = false)
    {
      try
      {
        var viewPrefab = _assetProvider.LoadAsset<EntityBehaviour>(entity.ViewPath);
        var view = usePrefabLocalPosition
          ? _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(
            viewPrefab,
            parentTransform: parenTransform)
          : _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(
            viewPrefab,
            position: _farAway,
            Quaternion.identity,
            parentTransform: parenTransform);

        view.SetEntity(entity);
        return view;
      }
      catch (Exception e)
      {
        ($"{entity.ViewPath}\n{e}").Log(LogType.Error);
        throw;
      }
      
      throw new Exception("View not found");
    }

    public EntityBehaviour CreateViewForEntityFromPrefab(GameEntity entity)
    {
      EntityBehaviour view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(
        entity.ViewPrefab,
        position: _farAway,
        Quaternion.identity,
        parentTransform: null);
      
      view.SetEntity(entity);

      return view;
    }
  }
}