using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Runtime.Infrastructure.GameServices.List.PlayerProgress
{
  public class PlayerProgress : IPlayerProgress
  {
    private const string SaveKey = "PlayerProgressData";
    
    public IPlayerProgressData Data { get; }

    public PlayerProgress() =>
      Data = Load();

    public void Save()
    {
      string playerProgressData = JsonConvert.SerializeObject(Data);
      PlayerPrefs.SetString(SaveKey, playerProgressData);
    }

    private PlayerProgressData Load()
    {
      if (PlayerPrefs.HasKey(SaveKey))
      {
        string playerProgressData = PlayerPrefs.GetString(SaveKey);
        return JsonConvert.DeserializeObject<PlayerProgressData>(playerProgressData);
      }

      return new PlayerProgressData();
    }
  }
}