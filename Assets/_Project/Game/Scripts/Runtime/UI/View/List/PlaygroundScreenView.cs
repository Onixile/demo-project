using System;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using _Project.Infrastructure.Scripts.Runtime.Utility.Extensions;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Game.Scripts.Runtime.UI.View.List
{
  public class PlaygroundScreenView : ScreenView
  {
    private const float FillDuration = 1;
    
    [SerializeField] private Button          _backButton;
    [SerializeField] private Image           _healthFill;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _health;

    public virtual void Initialization(string levelText, UnityAction onClickBack, Action<AudioItemType> onPlayAudio)
    {
      _level.text = levelText;
      
      _backButton.onClick.AddListener(onClickBack);
      _backButton.AddClickSound(onPlayAudio);
      _backButton.AddClickAnimation();
    }

    public void SetHealth(int value, int max)
    {
      _health.text = value.ToString();
      _healthFill.DOFillAmount((float)value / max, FillDuration);
    }

    private void OnDestroy() => 
      _healthFill.DOKill();
  }
}