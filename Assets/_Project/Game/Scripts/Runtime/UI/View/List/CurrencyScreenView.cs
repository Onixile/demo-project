using TMPro;
using UnityEngine;

namespace _Project.Game.Scripts.Runtime.UI.View.List
{
  public class CurrencyScreenView : ScreenView
  {
    private const int OverlaySortingOrder = 1000;
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _valueText;

    private int _defaultSortingOrder;
    
    public void Initialization() => 
      _defaultSortingOrder = _canvas.sortingOrder;

    public void SetValue(uint value) => 
      _valueText.text = value.ToString();

    public void SwitchOverlay(bool toTop) => 
      _canvas.sortingOrder = toTop ? OverlaySortingOrder : _defaultSortingOrder;
  }
}