using Code.Core.Input.Services;
using Entitas;

namespace Code.Core.Input.Systems
{
    public class EmitAttackInputSystem : IExecuteSystem
    {
        private readonly IInputService _inputService;
        private readonly IGroup<InputEntity> _inputs;

        public EmitAttackInputSystem(InputContext input, IInputService inputService)
        {
            _inputService = inputService;
            _inputs = input.GetGroup(InputMatcher.Input);
        }
    
        public void Execute()
        {
            foreach (InputEntity input in _inputs)
            {
                input.isAttacking = _inputService.IsAttacking();
            }
        }
    }
}