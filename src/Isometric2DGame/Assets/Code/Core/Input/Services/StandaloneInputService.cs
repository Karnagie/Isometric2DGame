using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Core.Input.Services
{
  public class StandaloneInputService : IInputService
  {
    private readonly InputAction _move = InputSystem.actions.FindAction("Move");
    private readonly InputAction _attack = InputSystem.actions.FindAction("Attack");

    public Vector2 GetMove() => _move.ReadValue<Vector2>();

    public bool HasMove() => GetMove() != Vector2.zero;
    public bool IsAttacking() => _attack.WasPressedThisFrame();
  }
}