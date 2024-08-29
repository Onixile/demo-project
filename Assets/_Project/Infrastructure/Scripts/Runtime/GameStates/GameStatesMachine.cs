using System;
using System.Collections.Generic;
using _Project.Infrastructure.Scripts.Runtime.GameServices;
using _Project.Infrastructure.Scripts.Runtime.GameStates.List;
using _Project.Infrastructure.Scripts.Runtime.GameStates.List.InitialState;
using _Project.Infrastructure.Scripts.Runtime.GameStates.List.MainMenuState;
using _Project.Infrastructure.Scripts.Runtime.GameStates.List.PlaygroundState;

namespace _Project.Infrastructure.Scripts.Runtime.GameStates
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