using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Object = UnityEngine.Object;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.AssetsProvider
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

    public async UniTask<AsyncOperationHandle<GameObject>[]> GetAssetsByLabel(string label) => 
      await GetAssetsByAssetReferences(await GetAssetReferencesByLabel(label));

    private async UniTask<List<AssetReference>> GetAssetReferencesByLabel(string label)
    {
      IList<IResourceLocation> locations = await Addressables.LoadResourceLocationsAsync(label).Task;
      return locations.Select(location => new AssetReference(location.PrimaryKey)).ToList();
    }

    private async UniTask<AsyncOperationHandle<GameObject>[]> GetAssetsByAssetReferences(List<AssetReference> assetReferences)
    {
      List<AsyncOperationHandle<GameObject>> handles = new List<AsyncOperationHandle<GameObject>>();

      foreach (var assetReference in assetReferences)
      {
        AsyncOperationHandle<GameObject> handle = assetReference.LoadAssetAsync<GameObject>();
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