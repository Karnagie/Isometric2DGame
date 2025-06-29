using System;
using Code.Infrastructure.View;

namespace Code.Core.Features.Enemies.Configs
{
    [Serializable]
    public struct EnemySetup
    {
        public EnemyId Id;
        
        public float Speed;
        public float Health;
        public EntityBehaviour Prefab;
    }
}