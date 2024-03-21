using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices.List.AssetsProvider
{
  public interface IAssetsProvider : IGameService
  {
    T GetResource<T>(string path) where T : Object;
    T[] GetResources<T>(string path) where T : Object;

    UniTask<GameObject[]> LoadAddressableAssetsGroupAsync(string label);
  }
}