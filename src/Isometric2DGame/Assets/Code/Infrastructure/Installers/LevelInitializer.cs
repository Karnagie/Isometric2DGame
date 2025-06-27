using System.Collections.Generic;
using Code.Core.Levels;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class LevelInitializer : MonoBehaviour, IInitializable
  {
    public Transform StartPoint;
    
    private ILevelDataProvider _levelDataProvider;

    [Inject]
    private void Construct(
      ILevelDataProvider levelDataProvider
      )
    {
      _levelDataProvider = levelDataProvider;
    }
    
    public void Initialize()
    {
      _levelDataProvider.SetStartPoint(StartPoint.position);
    }
  }
}