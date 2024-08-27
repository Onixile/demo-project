using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Game.Scripts.Runtime.UI.View.List
{
  public class LoadingScreenView : ScreenView
  {
    [SerializeField] [Range(0, 5)] private float  _screenHideAnimationDelay     = 0.05f;
    [SerializeField] [Range(0, 5)] private float  _progressBarAnimationDuration = 0.7f;
    [SerializeField]               private Slider _progressBar;

    public void Initialization() =>
      _progressBar.value = 0;

    public override void Show(Action onComplete = null)
    {
      _progressBar.value = 0;
      base.Show(() => onComplete?.Invoke());
    }

    public override void Hide(Action onComplete = null)
    {
      PrepareToAnimation(false, false, true, 1);

      _screenAnimationSequence
        .Append(_screenCanvasGroup.DOFade(0, _screenAnimationDuration).SetEase(Ease.InQuart))
        .SetDelay(_screenHideAnimationDelay)
        .OnComplete(() =>
        {
          gameObject.SetActive(false);
          onComplete?.Invoke();
        });

      _screenAnimationSequence.Play();
    }

    public void SetProgress(float value) =>
      _progressBar.DOValue(value, _progressBarAnimationDuration);
  }
}