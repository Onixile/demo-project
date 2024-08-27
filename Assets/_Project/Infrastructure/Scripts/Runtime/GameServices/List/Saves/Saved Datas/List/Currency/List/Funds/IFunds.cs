using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Base;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Currency.List.Funds
{
  public interface IFunds : ISavedData
  {
    uint Get();
    void Add(uint value);
    void Subtract(uint value);
  }
}
