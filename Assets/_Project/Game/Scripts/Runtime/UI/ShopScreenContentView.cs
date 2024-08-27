using System;
using _Project.Game.Scripts.Runtime.Configs;
using _Project.Game.Scripts.Runtime.UI.View;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Game.Scripts.Runtime.UI
{
  public class ShopScreenContentView : ScreenView
  {
    public string Id { get; private set; }

    [SerializeField] private Button          _mainButton;
    [SerializeField] private Image           _icon;
    [SerializeField] private Image           _pricePanel;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Image           _status;
    [SerializeField] private Color           _notChoosenColor;
    [SerializeField] private Color           _choosenColor;
    [SerializeField] private Color           _lockedColor;
    [SerializeField] private Color           _possibleColor;

    public void Initialization(PlayerItemConfig itemConfig, UnityAction onClick, Action<AudioItemType> onPlayAudio)
    {
      _mainButton.onClick.AddListener(onClick);
      _mainButton.AddClickSound(onPlayAudio);
      _mainButton.AddClickAnimation();

      Id = itemConfig.Id;
      
      _icon.sprite = itemConfig.Sprite;
      _icon.SetNativeSize();
      
      _priceText.text = itemConfig.Price.ToString();
    }

    public void UpdateStatus(bool isLocked, bool isBought, bool isCurrent)
    {
      if (isCurrent)
      {
        SetBought();
        SetChoosen();
        return;
      }

      if (!isBought)
        SetLocked(isLocked);
      else
        SetBought();
    }

    private void SetLocked(bool isLocked) =>
      _status.color = isLocked ? _lockedColor : _possibleColor;

    private void SetBought()
    {
      _status.color = _notChoosenColor;
      _pricePanel.gameObject.SetActive(false);
    }

    private void SetChoosen() => 
      _status.color = _choosenColor;
  }
}