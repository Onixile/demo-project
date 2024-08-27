using _Project.Game.Scripts.Runtime.Configs;
using _Project.Game.Scripts.Runtime.UI;
using _Project.Game.Scripts.Runtime.UI.Controller.List;
using _Project.Game.Scripts.Runtime.UI.View.List;
using _Project.Infrastructure.Scripts.Runtime.ContentManager;
using UnityEngine;
using _Project.Infrastructure.Scripts.Runtime.GameServices;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves;

namespace _Project.Infrastructure.Scripts.Runtime.GameStates.List.MainMenuState
{
  public class MainMenuState : GameState
  {
    private readonly ISaves       _saves;
    private readonly IAudioPlayer _audio;

    private readonly UIFactory _uiFactory;

    private MainMenuScreenController _mainMenuScreen;
    private SettingsScreenController _settingsScreen;
    private ShopScreenController     _shopScreen;

    private ShopContentManager<PlayerItemConfig> _shopContentManager;

    public MainMenuState(GameStatesMachine gameStatesMachine, IGameServices gameServices) : base(gameStatesMachine, gameServices)
    {
      _saves = GetService<ISaves>();
      _audio = GetService<IAudioPlayer>();

      _uiFactory = GetFactory<UIFactory>();
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
      _currencyScreen.Hide();
    }

    private async void Initialization()
    {
      await _configsFactory.LoadAddressableAssetsAsync(_names.ConfigsGroupLabel);
      PlayerItemConfig[] itemConfigs = _configsFactory.GetPlayerItemConfigs();
      _shopContentManager = new ShopContentManager<PlayerItemConfig>(itemConfigs, _saves.Datas.Currency.Soft, _saves.Datas.Progress.PlayerItems, _saves);

      await _uiFactory.LoadAddressableAssetsAsync(_names.UIGroupLabel);

      CreateGameStateScreen(out Transform gameStateScreenRoot, nameof(MainMenuState));

      _mainMenuScreen = new MainMenuScreenController(_uiFactory.CreateScreen<MainMenuScreenView>(gameStateScreenRoot), GoToPlayground, () => _settingsScreen.Show(), () => _shopScreen.Show(), _audio);
      _settingsScreen = new SettingsScreenController(_uiFactory.CreateScreen<SettingsScreenView>(gameStateScreenRoot), _currencyScreen, _saves, _audio);
      _shopScreen = new ShopScreenController(_uiFactory.CreateScreen<ShopScreenView>(gameStateScreenRoot), _uiFactory.CreateScreenContent<ShopScreenContentView>(gameStateScreenRoot, itemConfigs.Length), _shopContentManager, _currencyScreen, _popupScreen, _audio);

      _currencyScreen.Show();
      _mainMenuScreen.Show();
      _loadingScreen.Hide();

      _audio.Play(AudioItemType.Music);
    }

    private void GoToPlayground() =>
      GoToState<PlaygroundState.PlaygroundState>();
  }
}