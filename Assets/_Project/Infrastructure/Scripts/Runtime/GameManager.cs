using _Project.Infrastructure.Scripts.Runtime.GameServices;
using _Project.Infrastructure.Scripts.Runtime.GameStates;
using _Project.Infrastructure.Scripts.Runtime.GameStates.List.InitialState;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using UnityEngine;

namespace _Project.Infrastructure.Scripts.Runtime
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