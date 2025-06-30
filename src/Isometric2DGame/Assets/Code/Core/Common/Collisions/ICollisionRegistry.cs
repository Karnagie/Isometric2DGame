using Entitas;
using UnityEngine;

namespace Code.Core.Common.Collisions
{
  public interface ICollisionRegistry
  {
    void Register(int instanceId, IEntity entity, Collider2D collider);
    void Unregister(int instanceId);
    TEntity Get<TEntity>(int instanceId) where TEntity : class;
    Collider2D Get<TEntity>(TEntity entity) where TEntity : class, IEntity;
    bool Has(Collider2D collider);
  }
}