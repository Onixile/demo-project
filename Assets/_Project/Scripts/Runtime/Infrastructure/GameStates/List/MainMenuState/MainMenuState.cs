using _Project.Scripts.Runtime.Infrastructure.GameServices;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.AssetsProvider;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider.List;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.PlayerProgress;
using _Project.Scripts.Runtime.UI;
using _Project.Scripts.Runtime.UI.ShopData;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Runtime.Infrastructure.GameStates.List.MainMenuState
{
  public class MainMenuState : GameState
  {
    private const string SceneName = "MainMenu";
    private const string UIAssetsGroupLabel = "ui_MainMenu";
    private const string LoadingWindowDescriptionText = "Loading Main Menu State";
    private const string ShopItemsJsonPath = "Temp/shop_items";
    private const string PopupDescriptionShop = "Do you want to buy this item?";
    private const string PopupTitleEscape = "Escape";
    private const string PopupDescriptionEscape = "Do you want to close the application?";
    
    private MainMenuWindow _mainMenuWindow;
    private ShopWindow _shopWindow;
    private PopupWindow _popupWindow;
    private GameEscaper _gameEscaper;

    public MainMenuState(GameStatesMachine gameStatesMachine, IGameServices gameServices) : base(gameStatesMachine, gameServices)
    {
    }

    public override void Enter()
    {
      base.Enter();
      LoadScene(SceneName, Initialization, true, LoadingWindowDescriptionText, false);
    }

    protected async void Initialization()
    {
      IPlayerProgress playerProgress = _gameServices.Get<IPlayerProgress>();
      IAssetsProvider assetsProvider = _gameServices.Get<IAssetsProvider>();

      // TODO: For Tests
      ShopData shopData = JsonConvert.DeserializeObject<ShopData>(assetsProvider.GetResource<TextAsset>(ShopItemsJsonPath).text);

      UIFactory uiFactory = GetFactory<UIFactory>();

      await uiFactory.LoadAddressableGroupAsync(UIAssetsGroupLabel);

      Transform uiParent = InitializeUIStatePanel(nameof(MainMenuState));
      
      _popupWindow = uiFactory.PopupWindow;

      _gameEscaper = uiParent.gameObject.AddComponent<GameEscaper>();
      _gameEscaper.Initialization(OnEscape);

      _mainMenuWindow = uiFactory.CreateMainMenuWindow(uiParent);
      _shopWindow = uiFactory.CreateShopWindow(uiParent, shopData, out ShopContent[] shopContents);
      
      _mainMenuWindow.Initialization(OpenPlayground, OpenShop, playerProgress.Data.GetScore().ToString());
      _shopWindow.Initialization(CloseShop, ClickShopContent, shopContents);

      uiFactory.CleanupAddressableGroup();
      uiFactory.LoadingWindow.Hide();
    }

    protected override void CleanupUIStatePanel()
    {
      base.CleanupUIStatePanel();
      _gameEscaper.Sleep = true;
    }

    private void OpenPlayground() =>
      GoToState<PlaygroundState.PlaygroundState>();

    private void OpenShop() =>
      _shopWindow.Show();

    private void CloseShop() =>
      _shopWindow.Hide();

    private void ClickShopContent(string key)
    {
      _gameEscaper.Sleep = true;
      _popupWindow.SetupWindow(key, PopupDescriptionShop, null,
        delegate
        {
          _shopWindow.SetBought(key);
          _popupWindow.Hide();
          _gameEscaper.Sleep = false;
        },
        delegate
        {
          _popupWindow.Hide();
          _gameEscaper.Sleep = false;
        });

      _popupWindow.Show();
    }

    private void OnEscape()
    {
      _popupWindow.SetupWindow(PopupTitleEscape, PopupDescriptionEscape, null,
        delegate
        {
#if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        },
        delegate { _popupWindow.Hide(); });

      _popupWindow.Show();
    }
  }
}