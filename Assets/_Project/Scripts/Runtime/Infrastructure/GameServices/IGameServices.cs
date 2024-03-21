using _Project.Scripts.Runtime.Infrastructure.GameServices.List;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices
{
  public interface IGameServices
  {
    TService Get<TService>() where TService : class, IGameService;
  }
}