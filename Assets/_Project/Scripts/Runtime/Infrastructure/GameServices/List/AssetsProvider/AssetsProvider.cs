using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices.List.AssetsProvider
{
  public class AssetsProvider : IAssetsProvider
  {
    public T GetResource<T>(string path) where T : Object
    {
      T asset = Resources.Load<T>(path);

      if (!asset)
        Debug.LogError($"Can't find asset: {path}");

      return asset;
    }

    public T[] GetResources<T>(string path) where T : Object
    {
      T[] assets = Resources.LoadAll<T>(path);

      if (assets == null || assets.Length == 0)
        Debug.LogError($"Can't find assets: {path}");

      return assets;
    }

    public async UniTask<T[]> LoadAddressableAssetsGroupAsync<T>(string label)
    {
      T[] loadedAssetsGroup = null;
      AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(label, null);

      await handle.Task;

      if (handle.Status == AsyncOperationStatus.Succeeded)
        loadedAssetsGroup = handle.Result.ToArray();
      else
      {
        loadedAssetsGroup = Array.Empty<T>();
        Debug.LogError($"Can't load addressable group: {label}");
      }

      Addressables.Release(handle);
      return loadedAssetsGroup;
    }
  }
}