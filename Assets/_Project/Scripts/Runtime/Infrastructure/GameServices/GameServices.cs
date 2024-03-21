using System;
using System.Collections.Generic;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.AssetsProvider;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.PlayerProgress;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.SceneLoader;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices
{
  public class GameServices : IGameServices
  {
    private readonly Dictionary<Type, IGameService> _gameServices;
    
    public GameServices(IAssetsProvider assetsProvider, IFactoriesProvider factoriesProvider, IPlayerProgress playerProgress, ISceneLoader sceneLoader)
    {
      _gameServices = new Dictionary<Type, IGameService>();

      _gameServices.Add(typeof(IAssetsProvider), assetsProvider);
      _gameServices.Add(typeof(IFactoriesProvider), factoriesProvider);
      _gameServices.Add(typeof(IPlayerProgress), playerProgress);
      _gameServices.Add(typeof(ISceneLoader), sceneLoader);
    }

    public TService Get<TService>() where TService : class, IGameService =>
      _gameServices[typeof(TService)] as TService;
  }
}