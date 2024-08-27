using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Base;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Progress.List.Levels
{
  public interface ILevelProgress : ISavedData
  {
    void Set(uint value);
    uint Get();
  }
}
