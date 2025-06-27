using System;
using System.Collections.Generic;
using Code.Core.Common.Collisions;
using UnityEngine;

namespace Code.Core.Common.Physics
{
  public class PhysicsService : IPhysicsService
  {
    private static readonly RaycastHit2D[] Hits = new RaycastHit2D[128];
    private static readonly Collider2D[] OverlapHits = new Collider2D[128];
    
    private readonly ICollisionRegistry _collisionRegistry;
    private readonly Collider[] _results = new Collider[100];

    public PhysicsService(ICollisionRegistry collisionRegistry)
    {
      _collisionRegistry = collisionRegistry;
    }

    public IEnumerable<GameEntity> RaycastAll(Vector2 worldPosition, Vector2 direction, int layerMask)
    {
      var contactFilter2D = new ContactFilter2D();
      contactFilter2D.SetLayerMask(layerMask);
      
      int hitCount = Physics2D.Raycast(worldPosition, direction, contactFilter2D, Hits);

      for (int i = 0; i < hitCount; i++)
      {
        RaycastHit2D hit = Hits[i];
        if (hit.collider == null)
          continue;

        GameEntity entity = _collisionRegistry.Get<GameEntity>(hit.collider.GetInstanceID());
        if (entity == null)
          continue;

        yield return entity;
      }
    }

    public GameEntity Raycast(Vector2 worldPosition, Vector2 direction, int layerMask)
    {
      var contactFilter2D = new ContactFilter2D();
      contactFilter2D.SetLayerMask(layerMask);
      
      int hitCount = Physics2D.Raycast(worldPosition, direction, contactFilter2D, Hits);

      for (int i = 0; i < hitCount; i++)
      {
        RaycastHit2D hit = Hits[i];
        if (hit.collider == null)
          continue;

        GameEntity entity = _collisionRegistry.Get<GameEntity>(hit.collider.GetInstanceID());
        if (entity == null)
          continue;

        return entity;
      }

      return null;
    }

    public GameEntity LineCast(Vector2 start, Vector2 end, int layerMask)
    {
      var contactFilter2D = new ContactFilter2D();
      contactFilter2D.SetLayerMask(layerMask);
      
      int hitCount = Physics2D.Raycast(
        start, end - start, contactFilter2D, Hits, Vector2.Distance(start, end));

      for (int i = 0; i < hitCount; i++)
      {
        RaycastHit2D hit = Hits[i];
        if (hit.collider == null)
          continue;

        GameEntity entity = _collisionRegistry.Get<GameEntity>(hit.collider.GetInstanceID());
        if (entity == null)
          continue;

        return entity;
      }

      return null;
    }
    
    public IEnumerable<GameEntity> CircleCast(Vector3 position, float radius, int layerMask) 
    {
      int hitCount = OverlapCircle(position, radius, OverlapHits, layerMask);

      DrawDebug(position, radius, 1f, Color.red);
      
      for (int i = 0; i < hitCount; i++)
      {
        GameEntity entity = _collisionRegistry.Get<GameEntity>(OverlapHits[i].GetInstanceID());
        if (entity == null)
          continue;

        yield return entity;
      }
    }

    public int CircleCastNonAlloc(Vector3 position, float radius, int layerMask, GameEntity[] hitBuffer) 
    {
      int hitCount = OverlapCircle(position, radius, OverlapHits, layerMask);

      DrawDebug(position, radius, 1f, Color.green);
      
      for (int i = 0; i < hitCount; i++)
      {
        GameEntity entity = _collisionRegistry.Get<GameEntity>(OverlapHits[i].GetInstanceID());
        if (entity == null)
          continue;

        if (i < hitBuffer.Length)
          hitBuffer[i] = entity;
      }

      return hitCount;
    }

    public TEntity OverlapPoint<TEntity>(Vector2 worldPosition, int layerMask) where TEntity : class
    {
      var contactFilter2D = new ContactFilter2D();
      contactFilter2D.SetLayerMask(layerMask);
      
      int hitCount = Physics2D.OverlapPoint(worldPosition, contactFilter2D, OverlapHits);

      for (int i = 0; i < hitCount; i++)
      {
        Collider2D hit = OverlapHits[i];
        if (hit == null)
          continue;

        TEntity entity = _collisionRegistry.Get<TEntity>(hit.GetInstanceID());
        if (entity == null)
          continue;

        return entity;
      }

      return null;
    }

    public int OverlapCircle(Vector3 worldPos, float radius, Collider2D[] hits, int layerMask)
    {
      var contactFilter2D = new ContactFilter2D();
      contactFilter2D.SetLayerMask(layerMask);
      
      return Physics2D.OverlapCircle(worldPos, radius, contactFilter2D, hits);
    }

    public Vector3 CalculatePosition
      (GameEntity entity, Vector3 worldPositionA, Collider collider)
    {
      var colliderA = _collisionRegistry.Get(entity);
      if (colliderA == null)
        return worldPositionA;
      
      var startColliderPosition = colliderA.transform.position;
      var colliderPosition = colliderA.transform.position;
      
      colliderPosition = WorldPositionA(
        colliderPosition, colliderA.transform.rotation, colliderA,
        collider.transform.position, collider.transform.rotation, collider);

      return worldPositionA + (colliderPosition-startColliderPosition);
    }
    
    public Vector3 CalculatePosition
      (GameEntity entity, Vector3 worldPositionA, GameEntity near)
    {
      var colliderA = _collisionRegistry.Get(entity);
      if (colliderA == null)
        return worldPositionA;
      
      var colliderB = _collisionRegistry.Get(near);
      if (colliderB == null)
        return worldPositionA;
      
      var startColliderPosition = worldPositionA;
      var colliderPosition = worldPositionA;

      colliderPosition = WorldPositionA(
        colliderPosition, colliderA.transform.rotation, colliderA,
        near.WorldPosition, colliderB.transform.rotation, colliderB);
      
      return worldPositionA + (colliderPosition-startColliderPosition);
    }

    public Collider[] FindNonEntityColliders(GameEntity entity, Vector3 position, float radius)
    {
      var colliderA = _collisionRegistry.Get(entity);
      if (colliderA == null)
        return Array.Empty<Collider>();
      
      var size = UnityEngine.Physics.OverlapSphereNonAlloc(position, radius, _results);

      var colliders = new Collider[size];
      var count = 0;
      for (var index = 0; index < size; index++)
      {
        var nearCollider = _results[index];
        if (nearCollider == null || nearCollider == colliderA)
          continue;
        
        if (_collisionRegistry.Has(nearCollider))
          continue;
        
        colliders[index] = nearCollider;
        count++;
      }
      
      var colliders1 = new Collider[count];
      for (var index = size-1; index >= 0; index--)
      {
        if(colliders[index] == null)
          continue;
        
        colliders1[count-1] = colliders[index];
        count--;
      }

      return colliders1;
    }

    public GameEntity[] FindCollidedEntities(GameEntity entity, Vector3 position, float radius)
    {
      var colliderA = _collisionRegistry.Get(entity);
      if (colliderA == null)
        return Array.Empty<GameEntity>();

      var size = UnityEngine.Physics.OverlapSphereNonAlloc(position, radius, _results);

      var entities = new List<GameEntity>();
      for (var index = 0; index < size; index++)
      {
        var nearCollider = _results[index];
        if (nearCollider == null || nearCollider == colliderA)
          continue;

        if (_collisionRegistry.Has(nearCollider) == false)
          continue;

        entities.Add(_collisionRegistry.Get<GameEntity>(nearCollider.GetInstanceID()));
      }

      return entities.ToArray();
    }

    public (bool, Vector3, float) GetPenetration(
      GameEntity entity, Collider colliderB
    )
    {
      var colliderA = _collisionRegistry.Get(entity);
      if (colliderA == null)
        return (false, Vector3.zero, 0);
      
      var isPenetrating = UnityEngine.Physics.ComputePenetration(
        colliderA, colliderA.transform.position, colliderA.transform.rotation,
        colliderB, colliderB.transform.position, colliderB.transform.rotation,
        out var direction, out var distance
      );

      return (isPenetrating, direction, distance);
    }
    
    public (bool, Vector3, float) GetPenetration(
      GameEntity entity, GameEntity nearEntity
    )
    {
      var colliderA = _collisionRegistry.Get(entity);
      var colliderB = _collisionRegistry.Get(nearEntity);
      if (colliderA == null)
        return (false, Vector3.zero, 0);
      
      var isPenetrating = UnityEngine.Physics.ComputePenetration(
        colliderA, colliderA.transform.position, colliderA.transform.rotation,
        colliderB, colliderB.transform.position, colliderB.transform.rotation,
        out var direction, out var distance
      );

      return (isPenetrating, direction, distance);
    }

    public static Vector3 WorldPositionA(
      Vector3 worldPositionA, Quaternion rotationA, Collider colliderA,
      Vector3 worldPositionB, Quaternion rotationB, Collider colliderB
      )
    {
      var isPenetrating = UnityEngine.Physics.ComputePenetration(
        colliderA, worldPositionA, rotationA,
        colliderB, worldPositionB, rotationB,
        out var direction, out var distance
      );
      
      if (isPenetrating)
      {
        worldPositionA += direction * distance;
      }

      return worldPositionA;
    }

    private static void DrawDebug(Vector2 worldPos, float radius, float seconds, Color color)
    {
      Debug.DrawRay(worldPos, radius * Vector3.up, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.down, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.left, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.right, color, seconds);
    }
  }
}