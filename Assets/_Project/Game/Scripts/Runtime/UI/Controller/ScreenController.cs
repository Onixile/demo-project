using System;
using _Project.Game.Scripts.Runtime.UI.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Game.Scripts.Runtime.UI.Controller
{
  public abstract class ScreenController<T> where T : ScreenView
  {
    protected readonly T _view;

    protected ScreenController(T view) =>
      _view = view;

    public Transform GetViewRoot() =>
      _view.transform;

    public virtual void Cleanup(Action onComplete = null)
    {
      Object.Destroy(_view.gameObject);
      onComplete?.Invoke();
    }
    
    public virtual void Show() =>
      _view.Show();

    public virtual void Show(Action onComplete = null) =>
      _view.Show(onComplete);

    public virtual void Hide() =>
      _view.Hide();

    public virtual void Hide(Action onComplete = null) =>
      _view.Hide(onComplete);
  }
}