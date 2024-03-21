using _Project.Scripts.Runtime.Infrastructure.GameServices;
using _Project.Scripts.Runtime.Infrastructure.GameStates;
using _Project.Scripts.Runtime.Infrastructure.GameStates.List.InitialState;
using _Project.Scripts.Runtime.Utility;
using UnityEngine;

namespace _Project.Scripts.Runtime.Infrastructure
{
  public class GameManager : MonoBehaviour
  {
    private GameStatesMachine _gameStatesMachine;

    public void Initialization(IGameServices gameServices)
    {
      DontDestroyOnLoad(gameObject);

      _gameStatesMachine = new GameStatesMachine(gameServices);
      _gameStatesMachine.Enter<InitialState>();

      gameObject.name = nameof(GameManager).AddSpaceAfterCapital();
    }
  }
}