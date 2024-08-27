using System;
using _Project.Game.Scripts.Runtime.UI.View.List;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.AudioPlayer;
using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves;

namespace _Project.Game.Scripts.Runtime.UI.Controller.List
{
  public class SettingsScreenController : ScreenController<SettingsScreenView>
  {
    private readonly CurrencyScreenController _currencyScreen;

    private readonly ISaves _saves;

    public SettingsScreenController(SettingsScreenView view, CurrencyScreenController currencyScreen, ISaves saves, IAudioPlayer audio) : base(view)
    {
      _currencyScreen = currencyScreen;
      _saves = saves;
      _view.Initialization(Apply, ChangeMusicVolume, ChangeSoundsVolume, audio.Play,
        _saves.Datas.Settings.Audio.GetMusicVolume(), _saves.Datas.Settings.Audio.GetSoundsVolume());
    }

    public override void Show()
    {
      _currencyScreen.Hide();
      base.Show();
    }

    public override void Show(Action onComplete = null)
    {
      _currencyScreen.Hide();
      base.Show(onComplete);
    }

    public override void Hide()
    {
      _currencyScreen.Show();
      base.Hide();
    }

    public override void Hide(Action onComplete = null)
    {
      _currencyScreen.Show();
      base.Hide(onComplete);
    }

    private void Apply()
    {
      _saves.Update();
      Hide();
    }

    private void ChangeMusicVolume(float value) =>
      _saves.Datas.Settings.Audio.SetMusicVolume(value);

    private void ChangeSoundsVolume(float value) =>
      _saves.Datas.Settings.Audio.SetSoundsVolume(value);
  }
}