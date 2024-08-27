using System.Collections.Generic;
using System.Linq;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AssetsProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.ConfigProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List.Base;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider
{
  public class FactoriesProvider : IFactoriesProvider
  {
    private readonly List<BaseFactory> _factories;

    public FactoriesProvider(IAssetsProvider assetsProvider, IConfigsProvider configsProvider)
    {
      _factories = new List<BaseFactory>();
      
      _factories.Add(new UIFactory(assetsProvider));
      _factories.Add(new PlaygroundFactory(assetsProvider));
      _factories.Add(new AllConfigsFactory(configsProvider));
    }

    public T GetFactory<T>() where T : BaseFactory =>
      (T)_factories.FirstOrDefault(f => f is T);
  }
}