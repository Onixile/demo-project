using System.Linq;
using _Project.Game.Scripts.Runtime.Configs;
using _Project.Game.Scripts.Runtime.Playground;
using _Project.Game.Scripts.Runtime.UI.Controller.List;
using _Project.Game.Scripts.Runtime.UI.View.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves;
using UnityEngine;

namespace _Project.Infrastructure.Scripts.Runtime.GameStates.List.PlaygroundState
{
  public class PlaygroundState : GameState
  {
    private readonly ISaves       _saves;
    private readonly IAudioPlayer _audio;

    private readonly UIFactory         _uiFactory;
    private readonly PlaygroundFactory _playgroundFactory;

    private PlaygroundScreenController    _playgroundScreen;
    private LevelCompleteScreenController _levelCompleteScreen;

    private PlaygroundController _playgroundController;

    public PlaygroundState(GameStatesMachine gameStatesMachine, IGameServices gameServices)
      : base(gameStatesMachine, gameServices)
    {
      _saves = GetService<ISaves>();
      _audio = GetService<IAudioPlayer>();

      _uiFactory = GetFactory<UIFactory>();
      _playgroundFactory = GetFactory<PlaygroundFactory>();
    }

    public override void Enter()
    {
      base.Enter();
      LoadScene(_names.Scene, Initialization, true, false);
    }

    public override void Exit()
    {
      base.Exit();

      _uiFactory.ReleaseAddressableAssets();
      _playgroundFactory.ReleaseAddressableAssets();
    }

    private async void Initialization()
    {
      await _configsFactory.LoadAddressableAssetsAsync(_names.ConfigsGroupLabel);

      PlayerItemConfig playerConfig = _configsFactory.GetPlayerItemConfigs().First(x => x.Id == _saves.Datas.Progress.PlayerItems.GetCurrent());
      PlaygroundConfig playgroundConfig = _configsFactory.GetPlaygroundConfig();

      await _uiFactory.LoadAddressableAssetsAsync(_names.UIGroupLabel);
      await _playgroundFactory.LoadAddressableAssetsAsync(_names.ObjectsGroupLabel);

      int numberOfLevel = (int)Mathf.Clamp(_saves.Datas.Progress.Level.Get(), 0, playgroundConfig.LevelConfigs.Length - 1);

      CreateGameStateScreen(out Transform gameStateScreenRoot, nameof(PlaygroundState));

      _playgroundScreen = new PlaygroundScreenController(_uiFactory.CreateScreen<PlaygroundScreenView>(gameStateScreenRoot), numberOfLevel, GoToMainMenu, _audio);
      _levelCompleteScreen = new LevelCompleteScreenController(_uiFactory.CreateScreen<LevelCompleteScreenView>(gameStateScreenRoot), GoToMainMenu, _audio);

      _playgroundController = Object.FindObjectOfType<PlaygroundController>();
      _playgroundController.Initialization(_playgroundFactory, playgroundConfig, playerConfig, _playgroundScreen.SetHealth,
        delegate(bool isWin) { CompleteLevel(isWin, numberOfLevel, playgroundConfig.LevelConfigs[numberOfLevel].LevelDatas.Reward); }, numberOfLevel);

      _playgroundScreen.Show();
      _loadingScreen.Hide();
    }

    private void GoToMainMenu() =>
      GoToState<MainMenuState.MainMenuState>();

    private void CompleteLevel(bool isWin, int numberOfLevel, uint reward)
    {
      if (isWin)
      {
        _saves.Datas.Progress.Level.Set((uint)(numberOfLevel + 1));
        _saves.Datas.Currency.Soft.Add(reward);
        _saves.Update();
      }

      _levelCompleteScreen.Show(isWin);
    }
  }
}