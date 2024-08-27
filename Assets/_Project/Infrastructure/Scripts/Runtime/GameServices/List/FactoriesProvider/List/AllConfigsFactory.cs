using System.Linq;
using _Project.Game.Scripts.Runtime.Configs;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.ConfigProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List.Base;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List
{
  public class AllConfigsFactory : ConfigFactory
  {
    public AllConfigsFactory(IConfigsProvider assetsProvider) : base(assetsProvider)
    {
    }

    public PlayerItemConfig[] GetPlayerItemConfigs() => 
      GetFromAssetsHandles<PlayerItemConfig>();

    public PlaygroundConfig GetPlaygroundConfig() => 
      GetFromAssetsHandles<PlaygroundConfig>().First();
  }
}