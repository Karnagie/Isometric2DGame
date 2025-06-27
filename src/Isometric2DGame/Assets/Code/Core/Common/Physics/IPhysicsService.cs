using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.Common.Physics
{
  public interface IPhysicsService
  {
    GameEntity Raycast(Vector2 worldPosition, Vector2 direction, int layerMask);
    GameEntity LineCast(Vector2 start, Vector2 end, int layerMask);
    TEntity OverlapPoint<TEntity>(Vector2 worldPosition, int layerMask) where TEntity : class;
    IEnumerable<GameEntity> RaycastAll(Vector2 worldPosition, Vector2 direction, int layerMask);
    IEnumerable<GameEntity> CircleCast(Vector3 position, float radius, int layerMask);
    int OverlapCircle(Vector3 worldPos, float radius, Collider2D[] hits, int layerMask);
    int CircleCastNonAlloc(Vector3 position, float radius, int layerMask, GameEntity[] hitBuffer);
    Vector3 CalculatePosition (GameEntity entity, Vector3 worldPositionA, Collider collider);
    Vector3 CalculatePosition (GameEntity entity, Vector3 worldPositionA, GameEntity near);
    Collider[] FindNonEntityColliders(GameEntity entity, Vector3 position, float radius);
    GameEntity[] FindCollidedEntities(GameEntity entity, Vector3 position, float radius);

    (bool, Vector3, float) GetPenetration(
      GameEntity entity, Collider colliderB
    );
    (bool, Vector3, float) GetPenetration(
      GameEntity entity, GameEntity nearEntity
    );
  }
}