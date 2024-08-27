using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Base;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Settings.List
{
  public interface IAudioSettings : ISavedData
  {
    float GetMasterVolume();
    void SetMasterVolume(float value);

    float GetMusicVolume();
    void SetMusicVolume(float value);

    float GetSoundsVolume();
    void SetSoundsVolume(float value);
  }
}