using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Code.Core.Common.Collisions
{
  public class CollisionRegistry : ICollisionRegistry
  {
      private readonly Dictionary<int, IEntity> _entityByInstanceId = new();
      private readonly Dictionary<IEntity, Collider> _colliderByInstanceId = new();

      public void Register(int instanceId, IEntity entity, Collider collider)
      {
        _entityByInstanceId[instanceId] = entity;
        _colliderByInstanceId[entity] = collider;
      }

      public void Unregister(int instanceId)
      {
        if (_entityByInstanceId.ContainsKey(instanceId))
          _entityByInstanceId.Remove(instanceId);
        
        if (_entityByInstanceId.ContainsKey(instanceId))
          _entityByInstanceId.Remove(instanceId);
      }

      public TEntity Get<TEntity>(int instanceId) where TEntity : class
      {
        return _entityByInstanceId.TryGetValue(instanceId, out IEntity entity) 
          ? entity as TEntity
          : null;
      }

      public Collider Get<TEntity>(TEntity entity) where TEntity : class, IEntity => 
        _colliderByInstanceId.GetValueOrDefault(entity);

      public bool Has(Collider collider) => 
        _colliderByInstanceId.ContainsValue(collider);
  }
}