using System.Linq;
using _Project.Game.Scripts.Runtime.Configs;
using _Project.Infrastructure.Scripts.Runtime.Configs;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.ConfigProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List.Base;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List
{
  public class AllConfigsFactory : ConfigFactory
  {
    public AllConfigsFactory(IConfigsProvider assetsProvider) : base(assetsProvider)
    {
    }

    public T Get<T>() where T : Config =>
      GetAll<T>().First();

    public T[] GetAll<T>() where T : Config =>
      GetFromAssetsHandles<T>();
  }
}