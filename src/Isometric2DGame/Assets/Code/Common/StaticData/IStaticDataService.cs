using System.Collections.Generic;
using Code.Common.Windows;
using Code.Core.Features.Enemies;
using Code.Core.Features.Stats;
using Code.Infrastructure.Loading;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Common.StaticData
{
  public interface IStaticDataService
  {
    void LoadAll();
    GameObject GetWindowPrefab(WindowId id);
    LoadingScreen GetLoadingScreenPrefab();
    
    EntityBehaviour GetPlayerPrefab();
    Dictionary<StatId, float> GetPlayerStats();
    
    EntityBehaviour GetEnemyPrefab(EnemyId enemyId);
    Dictionary<StatId, float> GetEnemyStats(EnemyId enemyId);
  }
}