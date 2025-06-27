using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Windows;
using Code.Common.Windows.Configs;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Loading;
using UnityEngine;

namespace Code.Common.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private readonly IAssetProvider _assetProvider;
    
    private Dictionary<WindowId, GameObject> _windowPrefabsById;
    private LoadingScreen _loadingScreenPrefab;

    public StaticDataService(IAssetProvider assetProvider)
    {
      _assetProvider = assetProvider;
    }
    
    public void LoadAll()
    {
      LoadWindows();
      LoadScreens();
    }

    public GameObject GetWindowPrefab(WindowId id) =>
      _windowPrefabsById.TryGetValue(id, out GameObject prefab)
        ? prefab
        : throw new Exception($"Prefab config for window {id} was not found");

    public LoadingScreen GetLoadingScreenPrefab()
      => _loadingScreenPrefab;

    private void LoadScreens() => 
      _loadingScreenPrefab = _assetProvider.LoadAsset<LoadingScreen>("Loading/loadingScreen");

    private void LoadWindows()
    {
      _windowPrefabsById = _assetProvider
        .LoadAsset<WindowsConfig>("Configs/Windows/windowsConfig")
        .WindowConfigs
        .ToDictionary(x => x.Id, x => x.Prefab);
    }
  }
}