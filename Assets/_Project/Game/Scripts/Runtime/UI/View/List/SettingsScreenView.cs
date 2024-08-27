using System;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Game.Scripts.Runtime.UI.View.List
{
  public class SettingsScreenView : ScreenView
  {
    [SerializeField] private Button _applyButton;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _soundsVolumeSlider;

    public void Initialization(UnityAction onClickApply, UnityAction<float> onChangeMusicSlider, UnityAction<float> onChangeSoundsSlider,
      Action<AudioItemType> onPlayAudio, float musicVolume, float soundsVolume)
    {
      _applyButton.onClick.AddListener(onClickApply);
      _applyButton.AddClickSound(onPlayAudio);
      _applyButton.AddClickAnimation();

      _musicVolumeSlider.onValueChanged.AddListener(onChangeMusicSlider);
      _musicVolumeSlider.value = musicVolume;

      _soundsVolumeSlider.onValueChanged.AddListener(onChangeSoundsSlider);
      _soundsVolumeSlider.value = soundsVolume;
    }
  }
}