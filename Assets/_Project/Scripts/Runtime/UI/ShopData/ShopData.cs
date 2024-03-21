using Newtonsoft.Json;

namespace _Project.Scripts.Runtime.UI.ShopData
{
  [System.Serializable]
  public class ShopData
  {
    [JsonProperty("shopItems")] 
    public ShopItem[] ShopItems;
  }
}