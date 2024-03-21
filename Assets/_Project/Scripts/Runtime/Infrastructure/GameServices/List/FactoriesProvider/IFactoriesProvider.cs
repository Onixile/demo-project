using _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider.List;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider
{
  public interface IFactoriesProvider : IGameService
  {
    TFactory GetFactory<TFactory>() where TFactory : Factory;
  }
}