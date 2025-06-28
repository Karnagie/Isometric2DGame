using UnityEngine;

namespace Code.Core.Features.Players.Factories
{
    public interface IPlayerFactory
    {
        GameEntity Create(Vector3 at);
    }
}