using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Game.Scripts.Runtime.UI.View
{
  [RequireComponent(typeof(CanvasGroup))]
  public abstract class ScreenView : MonoBehaviour
  {
    [SerializeField] [Range(0, 5)] protected float       _screenAnimationDuration = 0.3f;
    [SerializeField]               protected CanvasGroup _screenCanvasGroup;

    protected Sequence _screenAnimationSequence;

    private void OnValidate() =>
      _screenCanvasGroup = GetComponent<CanvasGroup>();

    public void Awake() =>
      PrepareToAnimation(false, false, false, 0);

    public virtual void Show(Action onComplete = null)
    {
      PrepareToAnimation(false, true, true, 0);

      _screenAnimationSequence
        .Append(_screenCanvasGroup.DOFade(1, _screenAnimationDuration).SetEase(Ease.OutQuart))
        .OnComplete(() =>
        {
          _screenCanvasGroup.interactable = true;
          onComplete?.Invoke();
        });
    }

    public virtual void Hide(Action onComplete = null)
    {
      PrepareToAnimation(false, false, true, 1);

      _screenAnimationSequence
        .Append(_screenCanvasGroup.DOFade(0, _screenAnimationDuration).SetEase(Ease.OutQuart))
        .OnComplete(() =>
        {
          gameObject.SetActive(false);
          onComplete?.Invoke();
        });
    }

    protected void PrepareToAnimation(bool interactable, bool blocksRaycasts, bool isActive, float alpha)
    {
      ResetSequence();

      gameObject.SetActive(isActive);

      _screenCanvasGroup.alpha = alpha;
      _screenCanvasGroup.interactable = interactable;
      _screenCanvasGroup.blocksRaycasts = blocksRaycasts;
    }

    protected void ResetSequence()
    {
      _screenAnimationSequence?.Pause();
      _screenAnimationSequence?.Kill();

      _screenAnimationSequence = DOTween.Sequence();
    }

    private void OnDestroy() => 
      _screenAnimationSequence?.Kill();
  }
}