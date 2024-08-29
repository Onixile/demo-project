using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Currency;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.General;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Progress;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Settings;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas
{
  public interface ISavedDatas
  {
    IGeneral  General  { get; }
    ICurrency Currency { get; }
    ISettings Settings { get; }
    IProgress Progress { get; }
  }
}