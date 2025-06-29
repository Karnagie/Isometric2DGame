using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Core.Features.Players.Configs
{
    [CreateAssetMenu(fileName = "playerConfig", menuName = "Configs/Player Config", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        public float Speed;
        public float Health;
        public EntityBehaviour Prefab;
    }
}