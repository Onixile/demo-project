using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.ConfigProvider
{
  public interface IConfigsProvider : IGameService
  {
    T GetResource<T>(string path) where T : ScriptableObject;
    T[] GetResources<T>(string path) where T : ScriptableObject;

    UniTask<AsyncOperationHandle<ScriptableObject>[]> GetAssetsByLabel(string label);
  }
}