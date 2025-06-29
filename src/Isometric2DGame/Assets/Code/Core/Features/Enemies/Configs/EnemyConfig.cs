using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.Features.Enemies.Configs
{
    [CreateAssetMenu(fileName = "enemiesConfig", menuName = "Configs/Enemies Config", order = 0)]
    public class EnemyConfig : ScriptableObject
    {
        public List<EnemySetup> Enemies;
    }
}