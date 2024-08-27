using _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Base;

namespace _Project.Infrastructure.Scripts.Runtime.GameServices.List.Saves.Saved_Datas.List.Settings.List
{
  public class AudioSettings : SavedData, IAudioSettings
  {
    public float MasterVolume;
    public float MusicVolume;
    public float SoundsVolume;
    
    public AudioSettings()
    {
      SetMasterVolume(1);
      SetMusicVolume(1);
      SetSoundsVolume(1);
    }

    public float GetMasterVolume() =>
      MasterVolume;

    public void SetMasterVolume(float value)
    {
      MasterVolume = value;
      NotifyUpdate();
    }

    public float GetMusicVolume() =>
      MusicVolume;

    public void SetMusicVolume(float value)
    {
      MusicVolume = value;
      NotifyUpdate();
    }

    public float GetSoundsVolume() =>
      SoundsVolume;

    public void SetSoundsVolume(float value)
    {
      SoundsVolume = value;
      NotifyUpdate();
    }
  }
}
