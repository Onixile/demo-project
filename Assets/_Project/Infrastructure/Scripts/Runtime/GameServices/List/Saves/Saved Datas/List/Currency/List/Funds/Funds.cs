using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Base;
using UnityEngine;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Currency.List.Funds
{
  public class Funds : SavedData, IFunds
  {
    private const int StartValue = 30;

    public uint Amount;

    public Funds() =>
      Add(StartValue);

    public uint Get() =>
      Amount;

    public void Add(uint value)
    {
      Amount += value;
      NotifyUpdate();
    }

    public void Subtract(uint value)
    {
      Amount = (uint)Mathf.Clamp(Amount - value, 0, uint.MaxValue);
      NotifyUpdate();
    }
  }
}