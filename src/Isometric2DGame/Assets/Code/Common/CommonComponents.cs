using Code.Infrastructure.View;
using Entitas;
using UnityEngine;

namespace Code.Common
{
  [Game, Meta] public class Destructed : IComponent { }
  [Game] public class ViewPath : IComponent { public string Value; }
  [Game] public class ViewParent : IComponent { public Transform Value; }
  [Game] public class SelfDestructTimer : IComponent { public float Value; }
  [Game] public class Radius : IComponent { public float Value; }
}