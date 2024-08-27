using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Base;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Progress.List.Items
{
  public interface IItemsProgress : ISavedData
  {
    public void SetCurrent(string key);
    public string GetCurrent();
    public void SetBought(string key);
    public bool IsBought(string key);
  }
}