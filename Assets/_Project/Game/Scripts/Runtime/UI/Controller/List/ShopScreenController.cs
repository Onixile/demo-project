using System;
using _Project.Game.Scripts.Runtime.Configs;
using _Project.Game.Scripts.Runtime.Playground;
using _Project.Game.Scripts.Runtime.UI.View.List;
using _Project.Infrastructure.Scripts.Runtime.ContentManager;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using UnityEngine;

namespace _Project.Game.Scripts.Runtime.UI.Controller.List
{
  public class ShopScreenController : ScreenController<ShopScreenView>
  {
    private const string BuyingTitleText            = "Confirmation";
    private const string BuyingDescriptionText      = "Do you want to buy this item?";
    private const string RejectBuyingTitleText      = "Insufficient funds";
    private const string RejecBuyingDescriptionText = "You can't to buy this item";

    private readonly ShopContentManager<PlayerItemConfig> _contentManager;

    private readonly CurrencyScreenController _currencyScreen;
    private readonly PopupScreenController    _popupScreen;

    private readonly IAudioPlayer _audio;

    public ShopScreenController(ShopScreenView view, ShopScreenContentView[] contents, ShopContentManager<PlayerItemConfig> contentManager,
      CurrencyScreenController currencyScreen, PopupScreenController popupScreen, IAudioPlayer audio) : base(view)
    {
      _contentManager = contentManager;

      _currencyScreen = currencyScreen;
      _popupScreen = popupScreen;

      _audio = audio;

      _view.Initialization(contents, Hide, _audio.Play);
      InitializeContent(contents, _contentManager.GetConfigs());
    }

    private void InitializeContent(ShopScreenContentView[] contentViews, PlayerItemConfig[] playerItems)
    {
      Array.Sort(playerItems);

      if (contentViews.Length != playerItems.Length)
        throw new UnityException("Number of UI content panels doesn't equal the number of configuration files");

      int i = 0;
      foreach (var content in contentViews)
      {
        PlayerItemConfig config = playerItems[i];

        content.Initialization(config, () => ChooseContent(config.Id), _audio.Play);
        content.transform.SetParent(_view.ContentRoot);
        content.Show();

        i++;
      }

      ChooseContent(_contentManager.GetCurrent());
    }

    private void BuyAndChooseContent(string id, Action onComplete = null)
    {
      BuyContent(id);
      ChooseContent(id);

      onComplete?.Invoke();
    }

    private void BuyContent(string id)
    {
      _contentManager.Buy(id);
      UpdateContentStatus();
    }

    private void ChooseContent(string id)
    {
      if (_contentManager.IsBought(id))
        SetCurrentContent(id);
      else
        ShopPopup(id);
    }

    private void SetCurrentContent(string id)
    {
      _contentManager.SetCurrent(id);
      UpdateContentStatus();
    }

    private void ShopPopup(string id)
    {
      PlayerItemConfig config = _contentManager.GetConfig(id);

      if (_contentManager.IsLocked(id))
        _popupScreen.Initialization(RejectBuyingTitleText, RejecBuyingDescriptionText, HidePopup, null, null);
      else
      {
        _popupScreen.Initialization(BuyingTitleText, BuyingDescriptionText, config.Price, config.Sprite,
          null, () => BuyAndChooseContent(id, HidePopup), HidePopup);
      }

      _currencyScreen.SwitchOverlay(true);
      _popupScreen.Show();
    }

    private void HidePopup() =>
      _popupScreen.Hide(() => _currencyScreen.SwitchOverlay(false));

    private void UpdateContentStatus()
    {
      foreach (var item in _contentManager.GetConfigs())
      {
        string id = item.Id;
        _view.UpdateContentStatus(id, _contentManager.IsLocked(id), _contentManager.IsBought(id), _contentManager.GetCurrent() == id);
      }
    }
  }
}