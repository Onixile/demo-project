using System;
using UnityEngine;

namespace _Project.Infrastructure.Scripts.Runtime.Configs
{
  public class ConfigShop : ConfigId, IComparable<ConfigShop>
  {
    public Sprite Sprite => _sprite;
    public int    Price  => _price;

    [SerializeField] private Sprite _sprite;
    [SerializeField] [Range(0, 1000)] private int _price;
    
    public int CompareTo(ConfigShop config)
    {
      if (Price > config.Price)
        return 1;

      if (Price < config.Price)
        return -1;

      return 0;
    }
  }
}
