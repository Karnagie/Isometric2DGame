using Entitas;
using UnityEngine;

namespace Code.Core.Common.Collisions
{
  public interface ICollisionRegistry
  {
    void Register(int instanceId, IEntity entity, Collider collider);
    void Unregister(int instanceId);
    TEntity Get<TEntity>(int instanceId) where TEntity : class;
    Collider Get<TEntity>(TEntity entity) where TEntity : class, IEntity;
    bool Has(Collider collider);
  }
}