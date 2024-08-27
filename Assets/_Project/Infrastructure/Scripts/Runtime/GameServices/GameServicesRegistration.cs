using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AssetsProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.ConfigProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.SceneLoader;
using Zenject;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices
{
  public class GameServicesRegistration : MonoInstaller
  {
    public override void InstallBindings() =>
      BindServices();

    private void BindServices()
    {
      Container.Bind<IGameServices>().To<GameServices>().AsSingle();

      Container.Bind<IAssetsProvider>().To<AssetsProvider>().AsSingle();
      Container.Bind<IConfigsProvider>().To<ConfigsProvider>().AsSingle();
      Container.Bind<IFactoriesProvider>().To<FactoriesProvider>().AsSingle();
      Container.Bind<ISaves>().To<Saves>().AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
      Container.Bind<IAudioPlayer>().To<AudioPlayer>().AsSingle();
    }
  }
}