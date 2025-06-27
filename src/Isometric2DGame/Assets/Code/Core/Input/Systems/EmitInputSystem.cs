using Code.Core.Input.Services;
using Entitas;

namespace Code.Core.Input.Systems
{
  public class EmitInputSystem : IExecuteSystem
  {
    private readonly IInputService _inputService;
    private readonly IGroup<InputEntity> _inputs;

    public EmitInputSystem(InputContext input, IInputService inputService)
    {
      _inputService = inputService;
      _inputs = input.GetGroup(InputMatcher.Input);
    }
    
    public void Execute()
    {
      foreach (InputEntity input in _inputs)
      {
        if (_inputService.HasMove())
          input.ReplaceMoveInput(_inputService.GetMove());
        else if (input.hasMoveInput)
          input.RemoveMoveInput();
      }
    }
  }
}