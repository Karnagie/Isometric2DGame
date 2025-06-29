using Entitas;

namespace Code.Core.Features.Players.Systems
{
    public class SetDirectionByInputSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _players;
        private readonly IGroup<InputEntity> _inputs;

        public SetDirectionByInputSystem(GameContext game, InputContext input)
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
            foreach (var input in _inputs)
            foreach (var player in _players)
            {
                player.ReplaceDirection(input.MoveInput);
            }
        }
    }
}