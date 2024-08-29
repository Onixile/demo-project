using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Progress.List.Items;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Progress.List.Levels;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Progress
{
  public class Progress : IProgress
  {
    public IItemsProgress PlayerItems { get; }
    public ILevelProgress Level       { get; }

    public Progress()
    {
      PlayerItems = new ItemsProgress();
      Level = new LevelProgress();
    }
  }
}