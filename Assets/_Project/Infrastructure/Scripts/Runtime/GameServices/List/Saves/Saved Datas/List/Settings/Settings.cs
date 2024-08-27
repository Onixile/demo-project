using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Settings.List;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Settings
{
  public class Settings : ISettings
  {
    public IAudioSettings Audio { get; }

    public Settings() => 
      Audio = new AudioSettings();
  }
}