using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.AssetsProvider;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider.List;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider
{
  public class FactoriesProvider : IFactoriesProvider
  {
    private readonly List<Factory> _factories;

    public FactoriesProvider(IAssetsProvider assetsProvider)
    {
      _factories = new List<Factory>();
      
      _factories.Add(new UIFactory(assetsProvider));
      _factories.Add(new GameFactory(assetsProvider));
    }

    public TFactory GetFactory<TFactory>() where TFactory : Factory =>
      (TFactory)_factories.FirstOrDefault(f => f is TFactory);
  }
}