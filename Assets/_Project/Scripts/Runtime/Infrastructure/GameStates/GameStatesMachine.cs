using System;
using System.Collections.Generic;
using _Project.Scripts.Runtime.Infrastructure.GameServices;
using _Project.Scripts.Runtime.Infrastructure.GameStates.List;
using _Project.Scripts.Runtime.Infrastructure.GameStates.List.InitialState;
using _Project.Scripts.Runtime.Infrastructure.GameStates.List.MainMenuState;
using _Project.Scripts.Runtime.Infrastructure.GameStates.List.PlaygroundState;

namespace _Project.Scripts.Runtime.Infrastructure.GameStates
{
  public class GameStatesMachine
  {
    private readonly Dictionary<Type, IGameStateBase> _gameStates;
    private IGameStateBase _activeState;

    public GameStatesMachine(IGameServices gameServices)
    {
      _gameStates = new Dictionary<Type, IGameStateBase>();

      _gameStates[typeof(InitialState)] = new InitialState(this, gameServices);
      _gameStates[typeof(MainMenuState)] = new MainMenuState(this, gameServices);
      _gameStates[typeof(PlaygroundState)] = new PlaygroundState(this, gameServices);
    }

    public void Enter<TState>() where TState : class, IGameState =>
      ChangeState<TState>().Enter();

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedGameState<TPayload> =>
      ChangeState<TState>().Enter(payload);

    private TState ChangeState<TState>() where TState : class, IGameStateBase
    {
      _activeState?.Exit();

      TState state = GetState<TState>();
      _activeState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IGameStateBase =>
      _gameStates[typeof(TState)] as TState;
  }
}