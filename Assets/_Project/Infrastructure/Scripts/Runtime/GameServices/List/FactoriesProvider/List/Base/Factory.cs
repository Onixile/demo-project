using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List.Base
{
  public abstract class Factory<T> : BaseFactory where T : Object
  {
    protected AsyncOperationHandle<T>[] _assetHandles;

    public void ReleaseAddressableAssets()
    {
      if (_assetHandles != null)
      {
        _assetHandles.ToList().ForEach(Addressables.Release);
        _assetHandles = null;
      }
    }
  }
}