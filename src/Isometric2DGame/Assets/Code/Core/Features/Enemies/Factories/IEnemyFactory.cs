using UnityEngine;

namespace Code.Core.Features.Enemies.Factories
{
    public interface IEnemyFactory
    {
        GameEntity Create(Vector3 at, EnemyId id);
    }
}