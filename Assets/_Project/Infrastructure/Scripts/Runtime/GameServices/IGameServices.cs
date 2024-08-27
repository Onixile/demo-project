using _Project.Infrastructure.Scripts.Runtime.GameServices.List;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices
{
  public interface IGameServices
  {
    TService Get<TService>() where TService : class, IGameService;
  }
}