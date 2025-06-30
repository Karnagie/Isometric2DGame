using UnityEngine;

namespace Code.Core.Features.Ui.Factories
{
    public interface IHealthBarFactory
    {
        GameEntity Create(Vector3 at, int targetId);
    }
}