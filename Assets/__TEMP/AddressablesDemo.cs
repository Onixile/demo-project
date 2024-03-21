using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Serialization;

namespace _TEMP_
{
  public class AddressablesDemo : MonoBehaviour
  {
    public string addressableLabelName;

    private async void Start()
    {
      await LoadAddressableGroupAsync();
    }

    private async UniTask LoadAddressableGroupAsync()
    {
      AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>(addressableLabelName, null);

      await handle.Task;

      if (handle.Status == AsyncOperationStatus.Succeeded)
      {
        GameObject[] loadedAssets = handle.Result.ToArray();

        foreach (GameObject asset in loadedAssets)
          Instantiate(asset, Vector3.zero, Quaternion.identity);
      }
      else
        Debug.LogError("Failed to load Addressable group: " + addressableLabelName);

      Addressables.Release(handle);
    }
  }
}