using System.Linq;
using _Project.Scripts.Runtime.Infrastructure.GameServices.List.AssetsProvider;
using _Project.Scripts.Runtime.UI;
using _Project.Scripts.Runtime.UI.ShopData;
using _Project.Scripts.Runtime.Utility;
using UnityEngine;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices.List.FactoriesProvider.List
{
  public class UIFactory : Factory
  {
    private const string UIPath = "UI";
    private const string LoadingWindowPath = "Loading Window";
    private const string PopupWindowPath = "Popup Window";
    private const string GameStateWindowPath = "Game State Window";
    private const string MainMenuWindowPath = "Main Menu Window";
    private const string ShopWindowPath = "Shop Window";
    private const string ShopContentPath = "Shop Content";
    private const string PlaygroundWindowPath = "Playground Window";

    public LoadingWindow LoadingWindow { get; private set; }
    public PopupWindow PopupWindow { get; private set; }

    private Canvas _canvas;

    public UIFactory(IAssetsProvider assetsProvider) : base(assetsProvider)
    {
    }

    public void CreateCanvas()
    {
      GameObject uiObj = InstantiateFromAssetsGroup(UIPath);
      uiObj.gameObject.name = "UI";

      _canvas = uiObj.GetComponentInChildren<Canvas>();

      Object.DontDestroyOnLoad(uiObj);
    }

    public void CreateLoadingWindow() =>
      LoadingWindow = CreateWindow<LoadingWindow>(LoadingWindowPath, _canvas.transform, nameof(UI.LoadingWindow).AddSpaceAfterCapital());

    public void CreatePopupWindow() =>
      PopupWindow = CreateWindow<PopupWindow>(PopupWindowPath, _canvas.transform, nameof(UI.PopupWindow).AddSpaceAfterCapital());

    public GameStateWindow CreateGameStateWindow(string objName) =>
      CreateWindow<GameStateWindow>(GameStateWindowPath, _canvas.transform, objName.AddSpaceAfterCapital());

    public MainMenuWindow CreateMainMenuWindow(Transform parent) =>
      CreateWindow<MainMenuWindow>(MainMenuWindowPath, parent, nameof(MainMenuWindow).AddSpaceAfterCapital());

    public ShopWindow CreateShopWindow(Transform parent, ShopData shopData, out ShopContent[] shopContents)
    {
      ShopWindow window = CreateWindow<ShopWindow>(ShopWindowPath, parent, nameof(ShopWindow).AddSpaceAfterCapital());

      shopContents = shopData.ShopItems.Select(shopItem => CreateShopContent(window.Content, shopItem)).ToArray();
      return window;
    }

    public ShopContent CreateShopContent(Transform parent, ShopItem shopItem)
    {
      ShopContent content = InstantiateFromAssetsGroup(ShopContentPath, parent).GetComponent<ShopContent>();
      content.gameObject.name = $"{nameof(ShopContent).AddSpaceAfterCapital()} [{shopItem.Key}]";
      content.Initialization(shopItem);

      return content;
    }

    public PlaygroundWindow CreatePlaygroundWindow(Transform parent) =>
      CreateWindow<PlaygroundWindow>(PlaygroundWindowPath, parent, nameof(PlaygroundWindow).AddSpaceAfterCapital());

    private T CreateWindow<T>(string path, Transform parent, string name) where T : Window
    {
      T window = InstantiateFromAssetsGroup(path, parent).GetComponent<T>();
      window.gameObject.name = name;

      return window;
    }
  }
}