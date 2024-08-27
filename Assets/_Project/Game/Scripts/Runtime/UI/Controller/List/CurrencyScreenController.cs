using System;
using _Project.Game.Scripts.Runtime.UI.View.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Currency.List.Funds;

namespace _Project.Game.Scripts.Runtime.UI.Controller.List
{
  public class CurrencyScreenController : ScreenController<CurrencyScreenView>
  {
    private readonly IFunds _softFunds;

    public CurrencyScreenController(CurrencyScreenView view, ISaves saves) : base(view)
    {
      _softFunds = saves.Datas.Currency.Soft;
      _softFunds.RegisterUpdateListener(UpdateCurrency);
      
      _view.Initialization();
      
      UpdateCurrency();
    }

    ~CurrencyScreenController() => 
      _softFunds.UnregisterUpdateListener(UpdateCurrency);

    public void SwitchOverlay(bool toTop) => 
      _view.SwitchOverlay(toTop);

    private void UpdateCurrency() => 
      _view.SetValue(_softFunds.Get());

    public override void Cleanup(Action onComplete = null)
    {
      _softFunds.UnregisterUpdateListener(UpdateCurrency);
      base.Cleanup(onComplete);
    }
  }
}