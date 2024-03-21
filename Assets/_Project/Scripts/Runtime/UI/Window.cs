using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Runtime.UI
{
  [RequireComponent(typeof(CanvasGroup))]
  public abstract class Window : MonoBehaviour
  {
    [SerializeField] [Range(0, 5)] protected float _windowAnimationDuration = 0.5f;
    [SerializeField] protected CanvasGroup _windowCanvasGroup;

    protected Sequence _windowAnimationSequence;

    public void Awake()
    {
      _windowCanvasGroup = GetComponent<CanvasGroup>();
      SetupWindow(false, false, false, 0);
    }

    public virtual void Show(Action onComplete = null)
    {
      SetupWindow(false, true, true, 0);

      gameObject.SetActive(true);

      _windowAnimationSequence
        .Append(_windowCanvasGroup.DOFade(1, _windowAnimationDuration).SetEase(Ease.OutQuart))
        .OnComplete(delegate
        {
          _windowCanvasGroup.interactable = true;
          onComplete?.Invoke();
        });
    }

    public virtual void Hide(Action onComplete = null)
    {
      SetupWindow(false, false, true, 1);

      _windowAnimationSequence
        .Append(_windowCanvasGroup.DOFade(0, _windowAnimationDuration).SetEase(Ease.OutQuart))
        .OnComplete(delegate
        {
          gameObject.SetActive(false);
          onComplete?.Invoke();
        });
    }

    protected void SetupWindow(bool interactable, bool blocksRaycasts, bool isActive, float alpha)
    {
      ResetSequence();

      gameObject.SetActive(isActive);

      _windowCanvasGroup.alpha = alpha;
      _windowCanvasGroup.interactable = interactable;
      _windowCanvasGroup.blocksRaycasts = blocksRaycasts;
    }

    private void ResetSequence()
    {
      _windowAnimationSequence?.Pause();
      _windowAnimationSequence?.Kill();

      _windowAnimationSequence = DOTween.Sequence();
    }
  }
}