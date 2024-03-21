using Newtonsoft.Json;

namespace _Project.Scripts.Runtime.UI.ShopData
{
  [System.Serializable]
  public struct ShopItem
  {
    [JsonProperty("key")] 
    public string Key;

    [JsonProperty("amount")]
    public uint Amount;

    [JsonProperty("price")] 
    public string Price;

    [JsonProperty("currency")] 
    public string Currency;

    [JsonProperty("items")]
    public Item[] Items;
  }
}