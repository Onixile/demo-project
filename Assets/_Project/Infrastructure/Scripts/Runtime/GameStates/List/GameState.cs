using System;
using _Project.Game.Scripts.Runtime.UI.Controller.List;
using _Project.Game.Scripts.Runtime.UI.View.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List.Base;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.SceneLoader;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using UnityEngine;

namespace _Project.Infrastructure.Scripts.Runtime.GameStates.List
{
  public abstract class GameState : IGameState
  {
    protected readonly GameStateNames _names;

    protected static LoadingScreenController  _loadingScreen;
    protected static PopupScreenController    _popupScreen;
    protected static CurrencyScreenController _currencyScreen;

    private readonly GameStatesMachine _gameStatesMachine;
    private readonly IGameServices     _gameServices;
    protected static AllConfigsFactory _configsFactory;

    private GameStateScreenController _gameStateScreen;

    protected GameState(GameStatesMachine gameStatesMachine, IGameServices gameServices)
    {
      _names = new GameStateNames(GetType().Name.Remove("State"));

      _gameStatesMachine = gameStatesMachine;
      _gameServices = gameServices;
      _configsFactory ??= GetFactory<AllConfigsFactory>();
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
      CleanupGameStateScreen();
      _configsFactory.ReleaseAddressableAssets();
    }

    protected void GoToState<TState>() where TState : GameState =>
      _loadingScreen.Show(_gameStatesMachine.Enter<TState>);

    protected void LoadScene(string sceneName, Action onComplete,
      bool useLoadingWindow = false, bool autoHideWindow = true)
    {
      if (useLoadingWindow)
      {
        if (autoHideWindow)
          onComplete += _loadingScreen.Hide;

        Load(_loadingScreen.SetProgress);
      }
      else
        Load();

      void Load(Action<float> onSetProgress = null) =>
        _gameServices.Get<ISceneLoader>().Load(sceneName, onComplete, onSetProgress);
    }

    protected T GetFactory<T>() where T : BaseFactory =>
      _gameServices.Get<IFactoriesProvider>().GetFactory<T>();

    protected T GetService<T>() where T : class, IGameService =>
      _gameServices.Get<T>();

    protected void CreateGameStateScreen(out Transform root, string objName)
    {
      _gameStateScreen = new GameStateScreenController(GetFactory<UIFactory>().CreateScreen<GameStateScreenView>(objName));
      root = _gameStateScreen.GetViewRoot();
    }

    protected void CleanupGameStateScreen() =>
      _gameStateScreen?.Cleanup();
  }
}