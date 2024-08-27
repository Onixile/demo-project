using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves
{
  public class Saves : ISaves
  {
    private const string SaveKey = "SavedData";
    
    public ISavedDatas Datas { get; set; }

    public Saves() => 
      Initialization();

    public void Update()
    {
      string data = JsonConvert.SerializeObject(Datas);
      PlayerPrefs.SetString(SaveKey, data);
    }

    public void Reset()
    {
      PlayerPrefs.DeleteKey(SaveKey);
      Initialization();
    }

    private void Initialization()
    {
      Datas = Load();
      Update();
    }

    private SavedDatas Load()
    {
      if (PlayerPrefs.HasKey(SaveKey))
      {
        string data = PlayerPrefs.GetString(SaveKey);
        return JsonConvert.DeserializeObject<SavedDatas>(data);
      }

      return new SavedDatas();
    }
  }
}