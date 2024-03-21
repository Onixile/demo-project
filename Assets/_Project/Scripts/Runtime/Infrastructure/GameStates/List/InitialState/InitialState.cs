using _Project.Scripts.Runtime.Infrastructure.GameServices;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider.List;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Runtime.Infrastructure.GameStates.List.InitialState
{
  public class InitialState : GameState
  {
    private const string SceneName = "Initial";
    private const string UIAssetsGroupLabel = "ui_Initial";

    public InitialState(GameStatesMachine gameStatesMachine, IGameServices gameServices)
      : base(gameStatesMachine, gameServices)
    {
    }

    public override async void Enter()
    {
      base.Enter();
      await InitializationAsync();
      LoadScene(SceneName, GoToState<MainMenuState.MainMenuState>);
    }

    private async UniTask InitializationAsync()
    {
      UIFactory uiFactory = GetUIFactory();

      await uiFactory.LoadAddressableAssetsGroupAsync(UIAssetsGroupLabel);

      uiFactory.CreateCanvas();
      uiFactory.CreateLoadingWindow();
      uiFactory.CreatePopupWindow();

      uiFactory.CleanupAddressableGroup();
    }
  }
}