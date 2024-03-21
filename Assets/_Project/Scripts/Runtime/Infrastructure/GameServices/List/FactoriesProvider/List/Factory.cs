using System.Linq;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.AssetsProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider.List
{
  public abstract class Factory
  {
    protected readonly IAssetsProvider AssetsProvider;

    protected GameObject[] LoadedAssetsGroup;

    protected Factory(IAssetsProvider assetsProvider) =>
      AssetsProvider = assetsProvider;

    public async UniTask LoadAddressableAssetsGroupAsync(string labelName) => 
      LoadedAssetsGroup = await AssetsProvider.LoadAddressableAssetsGroupAsync(labelName);

    public void CleanupAddressableGroup() =>
      LoadedAssetsGroup = null;

    protected T InstantiateFromResources<T>(string path) where T : Object =>
      Object.Instantiate(AssetsProvider.GetResource<T>(path));

    protected T InstantiateFromResources<T>(string path, Vector3 position, Quaternion rotation) where T : Object =>
      Object.Instantiate(AssetsProvider.GetResource<T>(path), position, rotation);

    protected T InstantiateFromResources<T>(string path, Transform parent) where T : Object =>
      Object.Instantiate(AssetsProvider.GetResource<T>(path), parent);

    protected GameObject InstantiateFromAssetsGroup(string path) =>
      Object.Instantiate(LoadedAssetsGroup.FirstOrDefault(g => g.name == path));

    protected GameObject InstantiateFromAssetsGroup(string path, Vector3 position, Quaternion rotation) =>
      Object.Instantiate(LoadedAssetsGroup.FirstOrDefault(g => g.name == path), position, rotation);

    protected GameObject InstantiateFromAssetsGroup(string path, Transform parent) =>
      Object.Instantiate(LoadedAssetsGroup.FirstOrDefault(g => g.name == path), parent);
  }
}