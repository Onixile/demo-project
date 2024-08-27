using System.Linq;
using _Project.Infrastructure.Scripts.Runtime.Configs;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Currency.List.Funds;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Progress.List.Items;

namespace _Project.Infrastructure.Scripts.Runtime.ContentManager
{
  public class ShopContentManager<T> where T : ConfigShop
  {
    private readonly T[] _configs;

    private readonly IFunds         _funds;
    private readonly IItemsProgress _itemsProgress;
    private readonly ISaves         _saves;

    public ShopContentManager(T[] configs, IFunds funds, IItemsProgress itemsProgress, ISaves saves)
    {
      _configs = configs;

      _funds = funds;
      _itemsProgress = itemsProgress;
      _saves = saves;
      
      if (string.IsNullOrEmpty(itemsProgress.GetCurrent()))
      {
        string id = _configs[0].Id;
        Buy(id);
        SetCurrent(id);
      }
    }

    public T GetConfig(string id) =>
      _configs.FirstOrDefault(x => x.Id == id);

    public T[] GetConfigs() =>
      _configs;

    public bool IsBought(string id) => 
      _itemsProgress.IsBought(id);
    
    public bool IsLocked(string id) => 
      _funds.Get() < GetConfig(id).Price;

    public string GetCurrent() =>
      _itemsProgress.GetCurrent();

    public void SetCurrent(string id)
    {
      _itemsProgress.SetCurrent(id);
      _saves.Update();
    }

    public void Buy(string id)
    {
      T config = _configs.First(x => x.Id == id);

      _funds.Subtract((uint)config.Price);
      _itemsProgress.SetBought(id);
      _saves.Update();
    }
  }
}