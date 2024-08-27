using System;
using System.Collections.Generic;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AssetsProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.ConfigProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.SceneLoader;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices
{
  public class GameServices : IGameServices
  {
    private readonly Dictionary<Type, IGameService> _gameServices;

    public GameServices(IAssetsProvider assetsProvider, IConfigsProvider configsProvider, IFactoriesProvider factoriesProvider,
      ISaves saves, ISceneLoader sceneLoader, IAudioPlayer audioPlayer)
    {
      _gameServices = new Dictionary<Type, IGameService>();

      _gameServices.Add(typeof(IAssetsProvider), assetsProvider);
      _gameServices.Add(typeof(IConfigsProvider), configsProvider);
      _gameServices.Add(typeof(IFactoriesProvider), factoriesProvider);
      _gameServices.Add(typeof(ISaves), saves);
      _gameServices.Add(typeof(ISceneLoader), sceneLoader);
      _gameServices.Add(typeof(IAudioPlayer), audioPlayer);
    }

    public TService Get<TService>() where TService : class, IGameService =>
      _gameServices[typeof(TService)] as TService;
  }
}