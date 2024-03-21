using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Runtime.Game
{
  public class Cell : MonoBehaviour, IPointerClickHandler
  {
    public FillType FillType { get; private set; }

    [SerializeField] private SpriteRenderer _icon;

    private Action _onPointerClick;

    public void Initialization(Action onPointerClick)
    {
      _onPointerClick = onPointerClick;
      FillType = FillType.Empty;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (IsValid())
        return;

      _onPointerClick?.Invoke();
    }

    public void Fill(FillType fillType, Sprite icon)
    {
      if (IsValid())
        return;

      FillType = fillType;
      _icon.sprite = icon;
      _icon.gameObject.SetActive(true);
    }

    private bool IsValid() =>
      FillType != FillType.Empty;
  }
}