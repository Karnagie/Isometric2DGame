using Entitas;
using UnityEngine;

namespace Code.Core.Input
{
  [Input] public class Input : IComponent { }
  [Input] public class MoveInput : IComponent { public Vector2 Value; }
}