using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using AudioType = _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;

namespace _Project.Infrastructure.Scripts.Runtime.Utility.Extensions
{
  public static class ButtonExtensions
  {
    private const float AnimationDuration = 0.5f;
    private const float TargetScale       = 0.1f;

    public static void AddClickSound(this Button button, Action<AudioType.AudioItemType> _onPlayAudio) =>
      button.onClick.AddListener(() => _onPlayAudio?.Invoke(AudioType.AudioItemType.DefaultButton));

    public static void AddClickAnimation(this Button button)
    {
      button.transition = Selectable.Transition.None;
      button.onClick.AddListener(() => PunchAnimation(button));
    }

    private static void PunchAnimation(Button button)
    {
      button.transform.DOKill();
      button.transform.localScale = Vector3.one;
      button.transform.DOPunchScale(Vector3.one * TargetScale, AnimationDuration);
    }
  }
}