using Entitas;
using UnityEngine;

namespace Code.Core.Features.Movement
{
  [Game] public class Speed : IComponent { public float Value; }
  [Game] public class Offset : IComponent { public Vector3 Value; }
  
  [Game] public class TargetId : IComponent { public int Value; }
  [Game] public class Direction : IComponent { public Vector2 Value; }
  [Game] public class Moving : IComponent { }
  [Game] public class TurnedAlongDirection : IComponent { }
  [Game] public class RotationAlignedAlongDirection : IComponent { }
  [Game] public class MovementAvailable : IComponent { }
  [Game] public class MovementPoint : IComponent { public Vector2 Value; }
  [Game] public class SyncWorldPositionWithTransform : IComponent { }
}