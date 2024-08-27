using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.AssetsProvider
{
  public interface IAssetsProvider : IGameService
  {
    T GetResource<T>(string path) where T : Object;
    T[] GetResources<T>(string path) where T : Object;

    UniTask<AsyncOperationHandle<GameObject>[]> GetAssetsByLabel(string label);
  }
}