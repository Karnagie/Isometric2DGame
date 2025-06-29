using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.Levels
{
  public interface ILevelDataProvider
  {
    Vector3 StartPoint { get; }
    IReadOnlyList<Vector2> RoutePoints { get; }

    void SetStartPoint(Vector3 startPoint);
    void AddRoutePoint(Vector3 point);
  }
}