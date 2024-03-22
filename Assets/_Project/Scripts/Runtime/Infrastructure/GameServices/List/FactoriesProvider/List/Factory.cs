using System.Linq;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.AssetsProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider.List
{
  public abstract class Factory
  {
    protected readonly IAssetsProvider AssetsProvider;

    protected GameObject[] LoadedGameObjectGroup;

    protected Factory(IAssetsProvider assetsProvider) =>
      AssetsProvider = assetsProvider;

    public async UniTask LoadAddressableGroupAsync(string labelName) => 
      LoadedGameObjectGroup = await AssetsProvider.LoadAddressableAssetsGroupAsync<GameObject>(labelName);

    public void CleanupAddressableGroup() =>
      LoadedGameObjectGroup = null;

    protected T LoadFromResources<T>(string path) where T : Object =>
      AssetsProvider.GetResource<T>(path);
    
    protected T InstantiateFromResources<T>(string path) where T : Object =>
      Object.Instantiate(AssetsProvider.GetResource<T>(path));

    protected T InstantiateFromResources<T>(string path, Vector3 position, Quaternion rotation) where T : Object =>
      Object.Instantiate(AssetsProvider.GetResource<T>(path), position, rotation);

    protected T InstantiateFromResources<T>(string path, Transform parent) where T : Object =>
      Object.Instantiate(AssetsProvider.GetResource<T>(path), parent);

    protected GameObject InstantiateFromAssetsGroup(string path) =>
      Object.Instantiate(LoadedGameObjectGroup.FirstOrDefault(g => g.name == path));

    protected GameObject InstantiateFromAssetsGroup(string path, Vector3 position, Quaternion rotation) =>
      Object.Instantiate(LoadedGameObjectGroup.FirstOrDefault(g => g.name == path), position, rotation);

    protected GameObject InstantiateFromAssetsGroup(string path, Transform parent) =>
      Object.Instantiate(LoadedGameObjectGroup.FirstOrDefault(g => g.name == path), parent);
  }
}