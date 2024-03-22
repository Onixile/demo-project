using System;
using _Project.Scripts.Runtime.Infrastructure.GameServices;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider.List;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.SceneLoader;
using _Project.Scripts.Runtime.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Runtime.Infrastructure.GameStates.List
{
  public abstract class GameState : IGameState
  {
    protected readonly IGameServices _gameServices;

    protected GameStateWindow _uiStatePanel;

    private readonly GameStatesMachine _gameStatesMachine;
    private LoadingWindow _loadingWindow;

    protected GameState(GameStatesMachine gameStatesMachine, IGameServices gameServices)
    {
      _gameStatesMachine = gameStatesMachine;
      _gameServices = gameServices;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit() =>
      CleanupUIStatePanel();

    protected void GoToState<TState>() where TState : GameState =>
      _gameStatesMachine.Enter<TState>();

    protected void LoadScene(string sceneName, Action onComplete,
      bool useLoadingWindow = false, string loadingWindowDescriptionText = "", bool autoHideWindow = true)
    {
      if (useLoadingWindow)
      {
        _loadingWindow ??= GetFactory<UIFactory>().LoadingWindow;
        _loadingWindow.SetupWindow(loadingWindowDescriptionText);

        if (autoHideWindow)
          onComplete += delegate { _loadingWindow.Hide(); };

        _loadingWindow.Show(delegate { Load(_loadingWindow.SetProgress); });
      }
      else
        Load();

      void Load(Action<float> onSetProgress = null) =>
        _gameServices.Get<ISceneLoader>().Load(sceneName, onComplete, onSetProgress);
    }

    protected TFactory GetFactory<TFactory>() where TFactory : Factory
      => _gameServices.Get<IFactoriesProvider>().GetFactory<TFactory>();

    protected Transform InitializeUIStatePanel(string gameObjectName)
    {
      _uiStatePanel = GetFactory<UIFactory>().CreateGameStateWindow(gameObjectName);
      _uiStatePanel.Initialization();

      return _uiStatePanel.transform;
    }

    protected virtual void CleanupUIStatePanel()
    {
      if (_uiStatePanel != null)
        _uiStatePanel.Hide(delegate { Object.Destroy(_uiStatePanel.gameObject); });
    }
  }
}