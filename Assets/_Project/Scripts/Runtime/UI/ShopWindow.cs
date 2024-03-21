using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.Runtime.UI
{
  public class ShopWindow : Window
  {
    public Transform Content => _content;

    [SerializeField] private Button _hideButton;
    [SerializeField] private Transform _content;

    private ShopContent[] _shopContents;

    public void Initialization(UnityAction onHideShop, Action<string> onClickShopContent, ShopContent[] shopContents)
    {
      _shopContents = shopContents;

      foreach (ShopContent shopContent in _shopContents) 
        shopContent.AddListner(onClickShopContent);

      _hideButton.onClick.AddListener(onHideShop);
    }
    
    public void SetBought(string key) => 
      _shopContents.FirstOrDefault(x => x.Key == key)?.SetBought();
  }
}