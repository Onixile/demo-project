using _Project.Game.Scripts.Runtime.UI.Controller.List;
using _Project.Game.Scripts.Runtime.UI.View.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.FactoriesProvider.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves;
using Cysharp.Threading.Tasks;

namespace _Project.Infrastructure.Scripts.Runtime.GameStates.List.InitialState
{
  public class InitialState : GameState
  {
    public InitialState(GameStatesMachine gameStatesMachine, IGameServices gameServices)
      : base(gameStatesMachine, gameServices)
    {
    }

    public override async void Enter()
    {
      base.Enter();
      await InitializationAsync();
      LoadScene(_names.Scene, GoToState<MainMenuState.MainMenuState>);
    }

    private async UniTask InitializationAsync()
    {
      IAudioPlayer audio = GetService<IAudioPlayer>();
      ISaves saves = GetService<ISaves>();

      UIFactory uiFactory = GetFactory<UIFactory>();
      
      await uiFactory.LoadAddressableAssetsAsync(_names.UIGroupLabel);

      uiFactory.CreateCanvas();

      LoadingScreenView loadingScreenView = uiFactory.CreateScreen<LoadingScreenView>();
      PopupScreenView popupScreenView = uiFactory.CreateScreen<PopupScreenView>();
      CurrencyScreenView currencyScreenView = uiFactory.CreateScreen<CurrencyScreenView>();

      _loadingScreen = new LoadingScreenController(loadingScreenView);
      _popupScreen = new PopupScreenController(popupScreenView, audio);
      _currencyScreen = new CurrencyScreenController(currencyScreenView, saves);
    }
  }
}