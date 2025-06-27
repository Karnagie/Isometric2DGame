using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.StateMachines
{
  public interface IGameStateMachine 
  {
    void Enter<TState>() where TState : class, IState;
    void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
  }
}