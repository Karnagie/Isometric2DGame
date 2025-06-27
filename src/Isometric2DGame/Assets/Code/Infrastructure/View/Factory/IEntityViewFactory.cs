using UnityEngine;

namespace Code.Infrastructure.View.Factory
{
  public interface IEntityViewFactory
  {
    EntityBehaviour CreateViewForEntity(
      GameEntity entity,
      Transform parenTransform = null, 
      bool usePrefabLocalPosition = false);
    EntityBehaviour CreateViewForEntityFromPrefab(GameEntity entity);
  }
}