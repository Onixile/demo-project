using _Project.Scripts.Runtime.Game;
using _Project.Scripts.Runtime.Infrastructure.GameServices;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider.List;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.PlayerProgress;
using _Project.Scripts.Runtime.UI;
using UnityEngine;

namespace _Project.Scripts.Runtime.Infrastructure.GameStates.List.PlaygroundState
{
  public class PlaygroundState : GameState
  {
    private const int ScoreReward = 100;
    private const string SceneName = "Playground";
    private const string UIAssetsGroupLabel = "ui_Playground";
    private const string LoadingWindowDescriptionText = "Loading Playground State";
    private const string ResultWindowTitleWin = "Win";
    private readonly string ResultWindowDescriptionWin = $"You have just earned {ScoreReward} score!";
    private const string ResultWindowTitleLose = "Lose";
    private const string ResultWindowDescriptionLose = "You will win in next time!";
    private const string ResultWindowTitleNoWinners = "No Winners";
    private const string ResultWindowDescriptionNoWinners = "...and no loosers!";

    private PlaygroundWindow _playgroundWindow;
    private GameResultWindow _gameResultWindow;

    public PlaygroundState(GameStatesMachine gameStatesMachine, IGameServices gameServices) : base(gameStatesMachine, gameServices)
    {
    }

    public override void Enter()
    {
      base.Enter();
      LoadScene(SceneName, Initialization, true, LoadingWindowDescriptionText, false);
    }

    protected async void Initialization()
    {
      UIFactory uiFactory = GetUIFactory();

      await uiFactory.LoadAddressableAssetsGroupAsync(UIAssetsGroupLabel);

      Transform uiParent = InitializeUIStatePanel(nameof(PlaygroundState));

      _playgroundWindow = uiFactory.CreatePlaygroundWindow(uiParent);
      _playgroundWindow.Initialization(Back);
      
      _gameResultWindow = uiFactory.CreateGameResultWindow(uiParent);
      _gameResultWindow.Initialization(Back);

      // TODO: For Tests
      Object.FindObjectOfType<TicTacToe>().Initialization(PlayerWinHandle, BotWinHandle, NoWinnersHandle);

      uiFactory.CleanupAddressableGroup();
      uiFactory.LoadingWindow.Hide();
    }

    private void Back() => 
      GoToState<MainMenuState.MainMenuState>();

    private void PlayerWinHandle()
    {
      AddScore();

      _gameResultWindow.SetupWindow(ResultWindowTitleWin, ResultWindowDescriptionWin);
      ShowResultWindow();
    }

    private void BotWinHandle()
    {
      RemoveScore();
      
      _gameResultWindow.SetupWindow(ResultWindowTitleLose, ResultWindowDescriptionLose);
      ShowResultWindow();
    }

    private void NoWinnersHandle()
    {
      _gameResultWindow.SetupWindow(ResultWindowTitleNoWinners, ResultWindowDescriptionNoWinners);
      ShowResultWindow();
    }

    private void ShowResultWindow() => 
      _playgroundWindow.Hide(delegate { _gameResultWindow.Show(); });

    private void AddScore()
    {
      IPlayerProgress playerProgress = _gameServices.Get<IPlayerProgress>();
      playerProgress.Data.Score += ScoreReward;
      playerProgress.Save();
    }
    
    private void RemoveScore()
    {
      IPlayerProgress playerProgress = _gameServices.Get<IPlayerProgress>();
      playerProgress.Data.Score = Mathf.Clamp(playerProgress.Data.Score - ScoreReward, 0, int.MaxValue);
      playerProgress.Save();
    }
  }
}