using System;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Game.Scripts.Runtime.UI.View.List
{
  public class LevelCompleteScreenView : ScreenView
  {
    [SerializeField] private Button          _nextButton;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    public void Initialization(UnityAction onClickNext, Action<AudioItemType> onPlayAudio)
    {
      _nextButton.onClick.AddListener(onClickNext);
      _nextButton.AddClickSound(onPlayAudio);
      _nextButton.AddClickAnimation();
    }

    public void SetDescription(string text) => 
      _descriptionText.text = text;
  }
}