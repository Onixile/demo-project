using _Project.Scripts.Runtime.Game;
using _Project.Scripts.Runtime.Infrastructure.GameServices;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider.List;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.PlayerProgress;
using _Project.Scripts.Runtime.UI;
using UnityEngine;
using PopupWindow = _Project.Scripts.Runtime.UI.PopupWindow;

namespace _Project.Scripts.Runtime.Infrastructure.GameStates.List.PlaygroundState
{
  public class PlaygroundState : GameState
  {
    private const int ScoreReward = 100;
    private const string SceneName = "Playground";
    private const string UIAssetsGroupLabel = "ui_Playground";
    private const string GameAssetsGroupLabel = "game_TicTacToe";
    private const string LoadingWindowDescriptionText = "Loading Playground State";
    private const string ResultWindowTitleWin = "Win";
    private readonly string ResultWindowDescriptionWin = $"You have just earned {ScoreReward} score!";
    private const string ResultWindowTitleLose = "Lose";
    private const string ResultWindowDescriptionLose = "You will win in next time!";
    private const string ResultWindowTitleNoWinners = "No Winners";
    private const string ResultWindowDescriptionNoWinners = "...and no loosers!";
    private const string PopupTitlePause = "Pause";
    private const string PopupDescriptionPause = "Do you want to continue?";

    private IPlayerProgress _playerProgress;
    private PopupWindow _popupWindow;
    private PlaygroundWindow _playgroundWindow;
    private GameResultWindow _gameResultWindow;
    
    private TicTacToe _ticTacToe;

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
      _playerProgress = _gameServices.Get<IPlayerProgress>();

      UIFactory uiFactory = GetFactory<UIFactory>();
      GameFactory gameFactory = GetFactory<GameFactory>();

      await uiFactory.LoadAddressableGroupAsync(UIAssetsGroupLabel);
      await gameFactory.LoadAddressableGroupAsync(GameAssetsGroupLabel);

      Transform uiParent = InitializeUIStatePanel(nameof(PlaygroundState));

      _popupWindow = uiFactory.PopupWindow;
      
      _playgroundWindow = uiFactory.CreatePlaygroundWindow(uiParent);
      _gameResultWindow = uiFactory.CreateGameResultWindow(uiParent);
      
      _playgroundWindow.Initialization(Pause);
      _gameResultWindow.Initialization(Back);

      TicTacToeConfig ticTacToeConfig = gameFactory.GetTicTacToeConfig();
       _ticTacToe = gameFactory.CreateTicTacToe(null);
       _ticTacToe.Initialization(ticTacToeConfig,
        gameFactory.CreateCellPanel, PlayerWinHandler, BotWinHandler, NoWinnersHandler);

      uiFactory.CleanupAddressableGroup();
      gameFactory.CleanupAddressableGroup();
      
      uiFactory.LoadingWindow.Hide();
    }

    private void Pause()
    {
      _popupWindow.SetupWindow(PopupTitlePause, PopupDescriptionPause, Continue, Abort);
      _popupWindow.Show();

      _ticTacToe.PauseGame(true);
    }

    private void Continue() => 
      _popupWindow.Hide(delegate {  _ticTacToe.PauseGame(false); });

    private void Abort() => 
      _popupWindow.Hide(GoToState<MainMenuState.MainMenuState>);
    
    private void Back() => 
      GoToState<MainMenuState.MainMenuState>();

    private void PlayerWinHandler()
    {
      AddScore();

      _gameResultWindow.SetupWindow(ResultWindowTitleWin, ResultWindowDescriptionWin);
      ShowResultWindow();
    }

    private void BotWinHandler()
    {
      SubtractScore();

      _gameResultWindow.SetupWindow(ResultWindowTitleLose, ResultWindowDescriptionLose);
      ShowResultWindow();
    }

    private void NoWinnersHandler()
    {
      _gameResultWindow.SetupWindow(ResultWindowTitleNoWinners, ResultWindowDescriptionNoWinners);
      ShowResultWindow();
    }

    private void ShowResultWindow() =>
      _playgroundWindow.Hide(delegate { _gameResultWindow.Show(); });

    private void AddScore()
    {
      _playerProgress.Data.AddScore(ScoreReward);
      _playerProgress.Save();
    }

    private void SubtractScore()
    {
      _playerProgress.Data.SubtractScore(ScoreReward);
      _playerProgress.Save();
    }
  }
}