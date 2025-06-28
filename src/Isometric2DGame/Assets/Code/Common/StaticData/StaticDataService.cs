using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Common.Windows;
using Code.Common.Windows.Configs;
using Code.Core.Features.Players.Configs;
using Code.Core.Features.Stats;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Loading;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Common.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private readonly IAssetProvider _assetProvider;
    
    private Dictionary<WindowId, GameObject> _windowPrefabsById;
    private LoadingScreen _loadingScreenPrefab;
    private PlayerConfig _playerConfig;

    public StaticDataService(IAssetProvider assetProvider)
    {
      _assetProvider = assetProvider;
    }
    
    public void LoadAll()
    {
      LoadWindows();
      LoadScreens();
      LoadPrefabConfig();
    }

    public GameObject GetWindowPrefab(WindowId id) =>
      _windowPrefabsById.TryGetValue(id, out GameObject prefab)
        ? prefab
        : throw new Exception($"Prefab config for window {id} was not found");

    public LoadingScreen GetLoadingScreenPrefab()
      => _loadingScreenPrefab;

    public EntityBehaviour GetPlayerPrefab() => _playerConfig.Prefab;

    public Dictionary<StatId, float> GetPlayerStats() => 
      InitStats.EmptyStatDictionary()
        .With(x => x[StatId.Speed] = _playerConfig.Speed);

    private void LoadScreens() => 
      _loadingScreenPrefab = _assetProvider.LoadAsset<LoadingScreen>("Loading/loadingScreen");

    private void LoadWindows()
    {
      _windowPrefabsById = _assetProvider
        .LoadAsset<WindowsConfig>("Configs/Windows/windowsConfig")
        .WindowConfigs
        .ToDictionary(x => x.Id, x => x.Prefab);
    }
    
    private void LoadPrefabConfig() => 
      _playerConfig = _assetProvider.LoadAsset<PlayerConfig>("Configs/Players/playerConfig");
  }
}