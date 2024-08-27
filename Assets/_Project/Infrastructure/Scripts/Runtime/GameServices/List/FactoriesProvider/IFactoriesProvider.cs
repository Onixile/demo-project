using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List.Base;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider
{
  public interface IFactoriesProvider : IGameService
  {
    T GetFactory<T>() where T : BaseFactory;
  }
}