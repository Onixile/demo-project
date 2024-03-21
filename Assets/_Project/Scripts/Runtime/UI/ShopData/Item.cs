using Newtonsoft.Json;

namespace _Project.Scripts.Runtime.UI.ShopData
{
  [System.Serializable]
  public struct Item
  {
    [JsonProperty("key")]
    public string Key;
    
    [JsonProperty("damage")]
    public float Damage;
    
    [JsonProperty("amount")]
    public int Amount;
  }
}