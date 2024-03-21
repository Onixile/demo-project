using System;
using System.Globalization;
using _Project.Scripts.Runtime.UI.ShopData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Runtime.UI
{
  public class ShopContent : MonoBehaviour
  {
    private const string BoughtStatusText = "bought";
    public string Key { get; private set; }

    [SerializeField] private Button _button;
    
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _infoText;

    private Action<string> _onClick;
    private bool _bought;
    
    public void Initialization(ShopItem shopItem)
    {
      Key = shopItem.Key;

      _button.onClick.AddListener(OnClick);
      
      _titleText.text = shopItem.Key;
      _priceText.text = $"{shopItem.Price} {shopItem.Currency}";

      if (shopItem.Items == null || shopItem.Items.Length == 0)
        _infoText.text = shopItem.Amount.ToString();
      else
      {
        _infoText.text = string.Empty;
        _infoText.alignment = TextAlignmentOptions.Left;
        
        int i = 0;
        foreach (Item item in shopItem.Items)
        {
          if (i != 0)
            _infoText.text += "\n\r";
          _infoText.text += $"{item.Key}: " +
                           $"{GetStringValue(item.Damage, nameof(item.Damage).ToLower())} " +
                           $"{GetStringValue(item.Amount, nameof(item.Amount).ToLower())}";
          i++;
        }
      }

      string GetStringValue(float value, string prefix) => 
        value == 0 ? string.Empty : $"{prefix} {value.ToString(CultureInfo.InvariantCulture)}";
    }

    public void SetBought()
    {
      _bought = true;
      _priceText.text = BoughtStatusText;
    }

    public void AddListner(Action<string> onClick) => 
      _onClick += onClick;

    private void OnClick()
    {
      if(_bought)
        return;
      
      _onClick?.Invoke(Key);
    }
  }
}