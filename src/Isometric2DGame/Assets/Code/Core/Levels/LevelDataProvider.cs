using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.Levels
{
  public class LevelDataProvider : ILevelDataProvider
  {
    private readonly List<Vector2> _routePoints = new();

    public Vector3 StartPoint { get; private set; }
    public IReadOnlyList<Vector2> RoutePoints => _routePoints;

    public void SetStartPoint(Vector3 startPoint)
    {
      StartPoint = startPoint;
    }

    public void AddRoutePoint(Vector3 point)
    {
      _routePoints.Add(point);
    }
  }
}