using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Currency.List.Funds;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Currency
{
  public interface ICurrency
  {
    IFunds Soft { get; }
  }
}