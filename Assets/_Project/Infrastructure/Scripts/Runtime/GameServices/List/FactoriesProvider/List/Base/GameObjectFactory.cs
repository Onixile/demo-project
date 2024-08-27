using System.Linq;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AssetsProvider;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List.Base
{
  public abstract class GameObjectFactory : Factory<GameObject>
  {
    protected readonly IAssetsProvider AssetsProvider;

    protected GameObjectFactory(IAssetsProvider assetsProvider) =>
      AssetsProvider = assetsProvider;

    public async UniTask LoadAddressableAssetsAsync(string labelName) =>
      _assetHandles = await AssetsProvider.GetAssetsByLabel(labelName);

    protected GameObject Instantiate(GameObject obj) =>
      Object.Instantiate(obj).RemoveCloneFromName();

    protected GameObject Instantiate(GameObject obj, Vector3 position, Quaternion rotation) =>
      Object.Instantiate(obj, position, rotation).RemoveCloneFromName();

    protected GameObject Instantiate(GameObject obj, Transform parent) =>
      Object.Instantiate(obj, parent).RemoveCloneFromName();

    protected GameObject InstantiateFromResources(string path) =>
      Instantiate(AssetsProvider.GetResource<GameObject>(path)).RemoveCloneFromName();

    protected GameObject InstantiateFromResources(string path, Vector3 position, Quaternion rotation) =>
      Instantiate(AssetsProvider.GetResource<GameObject>(path), position, rotation);

    protected GameObject InstantiateFromResources(string path, Transform parent) =>
      Instantiate(AssetsProvider.GetResource<GameObject>(path), parent);

    protected GameObject[] GetFromResources(string path) =>
      AssetsProvider.GetResources<GameObject>(path);

    protected GameObject InstantiateFromAssetsHandles(string path) =>
      Instantiate(_assetHandles.FirstOrDefault(g => g.Result.name == path).Result);

    protected GameObject InstantiateFromAssetsHandles(string path, Vector3 position, Quaternion rotation) =>
      Instantiate(_assetHandles.FirstOrDefault(g => g.Result.name == path).Result, position, rotation);

    protected GameObject InstantiateFromAssetsHandles(string path, Transform parent) =>
      Instantiate(_assetHandles.FirstOrDefault(g => g.Result.name == path).Result, parent);

    protected GameObject InstantiateFromAssetsHandles<T>(Vector3 position, Quaternion rotation) where T : Component =>
      Instantiate(_assetHandles.FirstOrDefault(g =>
        g.Result.GetComponent<T>() && g.Result.GetComponent<T>().GetType() == typeof(T)).Result, position, rotation);

    protected GameObject InstantiateFromAssetsHandles<T>(Transform parent) where T : Component =>
      Instantiate(_assetHandles.FirstOrDefault(g =>
        g.Result.GetComponent<T>() && g.Result.GetComponent<T>().GetType() == typeof(T)).Result, parent);

    protected GameObject[] GetFromAssetsHandles<T>() where T : Component =>
      _assetHandles
        .Where(g => g.Result.GetComponent<T>() && g.Result.GetComponent<T>().GetType() == typeof(T))
        .Select(g => g.Result)
        .ToArray();
  }
}