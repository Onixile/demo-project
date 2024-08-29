using System.Collections.Generic;
using System.Linq;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.ConfigProvider
{
  public class ConfigsProvider : IConfigsProvider
  {
    public T GetResource<T>(string path) where T : ScriptableObject
    {
      T asset = Resources.Load<T>(path);

      if (!asset)
        Debug.LogError($"Can't find asset: {path}");

      return asset;
    }

    public T[] GetResources<T>(string path) where T : ScriptableObject
    {
      T[] assets = Resources.LoadAll<T>(path);

      if (assets.IsNullOrEmpty())
        Debug.LogError($"Can't find assets: {path}");

      return assets;
    }

    public async UniTask<AsyncOperationHandle<ScriptableObject>[]> GetAssetsByLabel(string label) =>
      await GetAssetsByAssetReferences(await GetAssetReferencesByLabel(label));

    private async UniTask<List<AssetReference>> GetAssetReferencesByLabel(string label)
    {
      IList<IResourceLocation> locations = await Addressables.LoadResourceLocationsAsync(label).Task;
      return locations.Select(location => new AssetReference(location.PrimaryKey)).ToList();
    }

    private async UniTask<AsyncOperationHandle<ScriptableObject>[]> GetAssetsByAssetReferences(List<AssetReference> assetReferences)
    {
      List<AsyncOperationHandle<ScriptableObject>> handles = new List<AsyncOperationHandle<ScriptableObject>>();

      foreach (var assetReference in assetReferences)
      {
        AsyncOperationHandle<ScriptableObject> handle = assetReference.LoadAssetAsync<ScriptableObject>();
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
          handles.Add(handle);
        else
          Debug.Log($"Can't load {assetReference.AssetGUID}");
      }

      return handles.ToArray();
    }
  }
}