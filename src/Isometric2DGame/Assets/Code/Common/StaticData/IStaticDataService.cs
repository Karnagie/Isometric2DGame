using Code.Common.Windows;
using Code.Infrastructure.Loading;
using UnityEngine;

namespace Code.Common.StaticData
{
  public interface IStaticDataService
  {
    void LoadAll();
    GameObject GetWindowPrefab(WindowId id);
    LoadingScreen GetLoadingScreenPrefab();
  }
}