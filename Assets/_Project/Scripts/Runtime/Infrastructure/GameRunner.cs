using _Project.Scripts.Runtime.Infrastructure.GameServices;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Runtime.Infrastructure
{
  public class GameRunner : MonoBehaviour
  {
    public GameManager GameManagerPrefab;

    [Inject]
    public void Initialization(IGameServices gameServices)
    {
      GameManager gameManager = FindObjectOfType<GameManager>();

      if (!gameManager)
      {
        if (!GameManagerPrefab)
        {
          Debug.LogError($"'{nameof(GameManagerPrefab)}' is null");
          return;
        }

        gameManager = Instantiate(GameManagerPrefab);
        gameManager.Initialization(gameServices);
      }

      Destroy(gameObject);
    }
  }
}