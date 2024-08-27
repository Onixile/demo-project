using System;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Currency;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.General;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Progress;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Settings;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas
{
  [Serializable]
  public class SavedDatas : ISavedDatas
  {
    public IGeneral General { get; private set; }
    public ICurrency Currency { get; private set; }
    public ISettings Settings { get; private set; }
    public IProgress Progress { get; private set; }

    public SavedDatas()
    {
      General = new General();
      Currency = new Currency();
      Settings = new Settings();
      Progress = new Progress();
    }
  }
}