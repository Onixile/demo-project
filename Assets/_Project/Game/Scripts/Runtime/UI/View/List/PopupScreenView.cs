using System;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Game.Scripts.Runtime.UI.View.List
{
  public class PopupScreenView : ScreenView
  {
    [Serializable]
    private struct PricedItem
    {
      public GameObject      RootObject;
      public TextMeshProUGUI PriceText;
      public Image           Icon;
    }

    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button          _okButton;
    [SerializeField] private Button          _yesButton;
    [SerializeField] private Button          _noButton;
    [SerializeField] private PricedItem      _pricedItem;

    public void Initialization(string title, string description,
      UnityAction onClickOk, UnityAction onClickYes, UnityAction onClickNo, Action<AudioItemType> onPlayAudio)
    {
      BaseInitialization(title, description, onClickOk, onClickYes, onClickNo, onPlayAudio);
      _pricedItem.RootObject.SetActive(false);
    }

    public void Initialization(string title, string description, int price, Sprite icon,
      UnityAction onClickOk, UnityAction onClickYes, UnityAction onClickNo, Action<AudioItemType> onPlayAudio)
    {
      BaseInitialization(title, description, onClickOk, onClickYes, onClickNo, onPlayAudio);
      _pricedItem.RootObject.SetActive(true);
      _pricedItem.PriceText.text = price.ToString();
      _pricedItem.Icon.sprite = icon;
      _pricedItem.Icon.SetNativeSize();
    }

    private void BaseInitialization(string title, string description, UnityAction onClickOk, UnityAction onClickYes, UnityAction onClickNo, Action<AudioItemType> onPlayAudio)
    {
      _titleText.text = title;
      _descriptionText.text = description;

      _okButton.onClick.RemoveAllListeners();
      _yesButton.onClick.RemoveAllListeners();
      _noButton.onClick.RemoveAllListeners();

      _okButton.onClick.AddListener(onClickOk);
      _yesButton.onClick.AddListener(onClickYes);
      _noButton.onClick.AddListener(onClickNo);

      _okButton.AddClickSound(onPlayAudio);
      _yesButton.AddClickSound(onPlayAudio);
      _noButton.AddClickSound(onPlayAudio);

      _okButton.gameObject.SetActive(onClickOk != null);
      _yesButton.gameObject.SetActive(onClickYes != null);
      _noButton.gameObject.SetActive(onClickNo != null);

      _okButton.AddClickAnimation();
      _yesButton.AddClickAnimation();
      _noButton.AddClickAnimation();
    }
  }
}