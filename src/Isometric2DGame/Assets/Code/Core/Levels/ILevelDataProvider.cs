using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.Levels
{
  public interface ILevelDataProvider
  {
    Vector3 StartPoint { get; }

    void SetStartPoint(Vector3 startPoint);
  }
}