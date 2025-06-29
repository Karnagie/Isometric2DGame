using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Core.Features.Routing
{
    [Game] public class RoutePoints : IComponent { public List<Vector2> Value; }
    [Game] public class CurrentRoutePoint : IComponent { public Vector2 Value; }
    [Game] public class Routing : IComponent { }
}