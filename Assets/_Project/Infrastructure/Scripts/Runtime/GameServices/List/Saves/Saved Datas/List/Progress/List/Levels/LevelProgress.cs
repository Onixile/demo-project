using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Base;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Progress.List.Levels
{
  public class LevelProgress : SavedData, ILevelProgress
  {
    public uint Level;
    
    public void Set(uint value)
    {
      Level = value;
      NotifyUpdate();
    }

    public uint Get() => 
      Level;
  }
}
