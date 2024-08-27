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
      UIFactory uiFactory = GetFactory<UIFactory>();
      await uiFactory.LoadAddressableAssetsAsync(_names.UIGroupLabel);

      uiFactory.CreateCanvas();

      _loadingScreen = new LoadingScreenController(uiFactory.CreateScreen<LoadingScreenView>());
      _popupScreen = new PopupScreenController(uiFactory.CreateScreen<PopupScreenView>(), GetService<IAudioPlayer>());
      _currencyScreen = new CurrencyScreenController(uiFactory.CreateScreen<CurrencyScreenView>(), GetService<ISaves>());
    }
  }
}