using UnityEngine;

namespace Code.Core.Input.Services
{
  public interface IInputService
  {
    Vector2 GetMove();
    bool HasMove();
  }
}