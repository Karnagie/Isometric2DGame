using Code.Core.Common.Layers;
using Entitas;
using UnityEngine;

namespace Code.Core.Common
{
  [Game] public class WorldPosition : IComponent { public Vector3 Value; }
  [Game] public class LocalPosition : IComponent { public Vector3 Value; }
  [Game] public class UsePrefabLocalPosition : IComponent { }
 
  [Game] public class TransformComponent : IComponent { public Transform Value; }
  [Game] public class LayerComponent : IComponent { public Layer Value; }
  [Game] public class SpriteRendererComponent : IComponent { public SpriteRenderer Value; }
}