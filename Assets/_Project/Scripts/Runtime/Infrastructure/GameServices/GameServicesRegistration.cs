using _Project.Scripts.Runtime.Infrastructure.GameServices.List.AssetsProvider;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.PlayerProgress;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.SceneLoader;
using Zenject;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices
{
  public class GameServicesRegistration : MonoInstaller
  {
    public override void InstallBindings() =>
      BindServices();

    private void BindServices()
    {
      Container.Bind<IGameServices>().To<GameServices>().AsSingle();

      Container.Bind<IAssetsProvider>().To<AssetsProvider>().AsSingle();
      Container.Bind<IFactoriesProvider>().To<FactoriesProvider>().AsSingle();
      Container.Bind<IPlayerProgress>().To<PlayerProgress>().AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
    }
  }
}