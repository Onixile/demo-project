using System.Collections.Generic;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Base;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Progress.List.Items
{
  public class ItemsProgress : SavedData, IItemsProgress
  {
    public string       CurrentItem = string.Empty;
    public List<string> BoughtItems;

    public ItemsProgress() => 
      BoughtItems = new List<string>();

    public void SetCurrent(string key)
    {
      CurrentItem = key;
      NotifyUpdate();
    }

    public string GetCurrent() =>
      CurrentItem;

    public void SetBought(string key)
    {
      if (BoughtItems.Contains(key) == false)
        BoughtItems.Add(key);
      NotifyUpdate();
    }

    public bool IsBought(string key) =>
      BoughtItems.Contains(key);
  }
}