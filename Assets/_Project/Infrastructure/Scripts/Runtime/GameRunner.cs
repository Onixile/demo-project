using _Project.Infrastructure.Scripts.Runtime.GameServices;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AssetsProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Infrastructure.Scripts.Runtime
{
  public class GameRunner : MonoBehaviour
  {
    private const string GameManagerPath = "Game Manager";
    
    [Inject]
    public async void Initialization(IGameServices gameServices)
    {
      await UniTask.DelayFrame(1);
      
      GameManager gameManager = FindObjectOfType<GameManager>();

      if (!gameManager)
      {
        GameObject prefab = gameServices.Get<IAssetsProvider>().GetResource<GameObject>(GameManagerPath);

        gameManager = Instantiate(prefab).GetComponent<GameManager>();
        gameManager.Initialization(gameServices);
      }

      Destroy(gameObject);
    }
  }
}