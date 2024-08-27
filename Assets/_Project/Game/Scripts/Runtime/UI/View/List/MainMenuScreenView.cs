using System;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Game.Scripts.Runtime.UI.View.List
{
  public class MainMenuScreenView : ScreenView
  {
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _shopButton;

    public void Initialization(UnityAction onClickPlay, UnityAction onClickSettings, UnityAction onClickShop,
      Action<AudioItemType> onPlayAudio)
    {
      _playButton.onClick.AddListener(onClickPlay);
      _settingsButton.onClick.AddListener(onClickSettings);
      _shopButton.onClick.AddListener(onClickShop);

      _playButton.AddClickSound(onPlayAudio);
      _settingsButton.AddClickSound(onPlayAudio);
      _shopButton.AddClickSound(onPlayAudio);

      _playButton.AddClickAnimation();
      _settingsButton.AddClickAnimation();
      _shopButton.AddClickAnimation();
    }
  }
}