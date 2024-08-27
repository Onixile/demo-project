using System.Linq;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.ConfigProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List.Base
{
  public abstract class ConfigFactory : Factory<ScriptableObject>
  {
    protected readonly IConfigsProvider ConfigsProvider;

    protected ConfigFactory(IConfigsProvider assetsProvider) =>
      ConfigsProvider = assetsProvider;

    public async UniTask LoadAddressableAssetsAsync(string labelName) =>
      _assetHandles = await ConfigsProvider.GetAssetsByLabel(labelName);

    protected T[] GetFromResources<T>(string path) where T : ScriptableObject =>
      ConfigsProvider.GetResources<T>(path);

    protected T[] GetFromAssetsHandles<T>() where T : ScriptableObject =>
      _assetHandles
        .Where(g => g.Result is T)
        .Select(g => g.Result as T)
        .ToArray();
  }
}