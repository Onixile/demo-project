namespace _Project.Scripts.Runtime.Infrastructure.GameStates.List
{
  public interface IGameStateBase
  {
    void Exit();
  }

  public interface IGameState : IGameStateBase
  {
    void Enter();
  }

  public interface IPayloadedGameState<TPayload> : IGameStateBase
  {
    void Enter(TPayload payload);
  }
}