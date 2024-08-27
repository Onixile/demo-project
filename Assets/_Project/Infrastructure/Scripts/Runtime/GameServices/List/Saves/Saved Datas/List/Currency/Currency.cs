using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Currency.List.Funds;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Currency
{
  public class Currency : ICurrency
  {
    public IFunds Soft { get; }

    public Currency() => 
      Soft = new Funds();
  }
}