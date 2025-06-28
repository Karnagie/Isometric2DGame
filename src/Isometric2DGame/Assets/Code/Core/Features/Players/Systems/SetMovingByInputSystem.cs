using Entitas;

namespace Code.Core.Features.Players.Systems
{
    public class SetMovingByInputSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _players;
        private readonly IGroup<InputEntity> _inputs;

        public SetMovingByInputSystem(GameContext game, InputContext input)
        {
            _players = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Player));

            _inputs = input.GetGroup(InputMatcher
                .AllOf(
                    InputMatcher.Input,
                    InputMatcher.MoveInput));
        }

        public void Execute()
        {
            var hasInput = _inputs.count > 0;
            foreach (var player in _players)
            {
                player.isMoving = hasInput;
            }
        }
    }
}